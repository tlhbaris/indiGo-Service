using indiGo.Business.Repositories.Abstract;
using indiGo.Core.Entities;
using indiGo.Core.Identity;
using indiGo.Core.ViewModels;
using indiGo.Core.ViewModels.PageViewModels;
using indiGo.Data.Identity;
using indiGo.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace indiGo.Web.Controllers;

[Authorize(Roles = "ELECTRICALSERVICE,GASSERVICE,PLUMBINGSERVICE")]
public class ServiceController : Controller
{
    private readonly IRepository<ServiceDemand, int> _serviceDemandRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository<Address, int> _addressRepository;
    private readonly IRepository<Product,int> _productRepository;
    private readonly IRepository<Receipt,int> _receiptRepository;
    private readonly IRepository<ReceiptDetail,int> _receiptDetailRepository;

    public ServiceController(IRepository<ServiceDemand, int> serviceDemandRepository, UserManager<ApplicationUser> userManager, IRepository<Address, int> addressRepository, IRepository<Product, int> productRepository, IRepository<Receipt, int> receiptRepository, IRepository<ReceiptDetail, int> receiptDetailRepository)
    {
        _serviceDemandRepository = serviceDemandRepository;
        _userManager = userManager;
        _addressRepository = addressRepository;
        _productRepository = productRepository;
        _receiptRepository = receiptRepository;
        _receiptDetailRepository = receiptDetailRepository;
    }

    [HttpGet]
    public IActionResult WaitingDemands()
    {
        var demands = _serviceDemandRepository.Get(x => x.ServiceId == HttpContext.GetUserId()).ToList();

        var model = new List<ServiceDemandViewModel>();
        demands.ForEach(x =>
        {
            if (x.Accepted)
            {
                return;
            }
            var address = _addressRepository.GetById(x.AddressId);
            model.Add(new ServiceDemandViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = address,
                AddressId = x.AddressId,
                Category = x.Category,
                CreatedAt = x.CreatedAt,
                PhoneNumber = x.PhoneNumber,
                TCKN = x.TCKN,
                Problem = x.Problem
            });
        });

        return View(model);
    }

    [HttpPost]
    public IActionResult TakeJob(int serviceId)
    {
        var demand = _serviceDemandRepository.GetById(serviceId);
        if (demand == null)
        {
            TempData["status"] = "fail";
        }
        else
        {
            demand.Accepted = true;
            _serviceDemandRepository.Save();
            TempData["status"] = "success";
        }
        return RedirectToAction("WaitingDemands");
    }

    [HttpGet]
    public IActionResult AcceptedDemands()
    {
        var demands = _serviceDemandRepository.Get(x => x.ServiceId == HttpContext.GetUserId() && !x.Receipted).ToList();
        var products = _productRepository.Get().ToList();

        var model = new AcceptedDemandsPageViewModel();
        
        demands.ForEach(x =>
        {
            if (!x.Accepted)
            {
                return;
            }
            var address = _addressRepository.GetById(x.AddressId);
            model.ServiceDemands.Add(new ServiceDemandViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = address,
                AddressId = x.AddressId,
                Category = x.Category,
                CreatedAt = x.CreatedAt,
                PhoneNumber = x.PhoneNumber,
                TCKN = x.TCKN,
                Problem = x.Problem
            });
        });

        products.ForEach(x =>
        {
            model.Products.Add(new ProductViewModel()
            {
                Id = x.Id,
                Category = x.Category,
                Name = x.Name,
                Price = x.Price
            });
        });

        return View(model);
    }

    [HttpPost]
    public IActionResult AcceptedDemands(DemandProductReceiptViewModel model)
    {
        var demand = _serviceDemandRepository.GetById(model.DemandId);
        var receipt = new Receipt()
        {
            DemandId = model.DemandId
        };

        _receiptRepository.Insert(receipt);
        _receiptRepository.Save();

        model.Products.ForEach(x =>
        {
            _receiptDetailRepository.Insert(new ReceiptDetail()
            {
                ProductId = x.Id, 
                ReceiptId = receipt.Id,
                Quantity = x.Quantity
            });
        });
        demand.Receipted = true;
        _receiptDetailRepository.Save();
        _serviceDemandRepository.Save();

        TempData["ReceiptCreated"] = "success";
        
        return RedirectToAction("AcceptedDemands");
    }

    [HttpGet]
    public IActionResult CompletedDemands()
    {
        var demands = _serviceDemandRepository.Get(x => x.ServiceId == HttpContext.GetUserId() && x.Receipted).ToList();
        
        var model = new List<CompletedDemandsPageViewModel>();

        demands.ForEach(x =>
        {
            var receipt = _receiptRepository.Get(y => y.DemandId == x.Id).FirstOrDefault();
            var details = _receiptDetailRepository.Get(y => y.ReceiptId == receipt.Id).ToList();
            model.Add(new CompletedDemandsPageViewModel()
            {
                ServiceDemand = new ServiceDemandViewModel()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    AddressId = x.AddressId,
                    Accepted = x.Accepted,
                    Category = x.Category,
                    Completed = x.Completed,
                    CreatedAt = x.CreatedAt,
                    Id = x.Id,
                    PhoneNumber = x.PhoneNumber,
                    Paid = x.Paid,
                    Problem = x.Problem,
                    ServiceId = x.ServiceId,
                    Receipted = x.Receipted,
                    TCKN = x.TCKN
                },
            });
            details.ForEach(y =>
            {
                var product = _productRepository.GetById(y.ProductId);
                model.Last().Products.Add(new ReceiptProductViewModel()
                {
                    Id = product.Id,
                    Price = product.Price,
                    Category = product.Category,
                    Name = product.Name,
                    Quantity = y.Quantity
                });
                model.Last().TotalPrice += Math.Round(y.Quantity * product.Price,2);
            });
            

        });

      
        return View(model);
    }

}