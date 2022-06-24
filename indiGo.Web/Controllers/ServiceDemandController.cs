using indiGo.Business.Repositories.Abstract;
using indiGo.Core.Categories;
using indiGo.Core.Entities;
using indiGo.Core.ViewModels;
using indiGo.Data.Identity;
using indiGo.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace indiGo.Web.Controllers;

public class ServiceDemandController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository<Address,int> _addressRepository;
    private readonly IRepository<ServiceDemand, int> _serviceDemandRepository;

    public ServiceDemandController(IRepository<Address, int> addressRepository, UserManager<ApplicationUser> userManager, IRepository<ServiceDemand, int> serviceDemandRepository)
    {
        _addressRepository = addressRepository;
        _userManager = userManager;
        _serviceDemandRepository = serviceDemandRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ElectricalServiceDemand()
    {
        var userId = HttpContext.GetUserId();
        var addresses = _addressRepository.Get(x=>x.UserId == userId).ToList();
       
        var model = new ServiceDemandPageViewModel();

        addresses.ForEach(x =>
        {
            model.Addresses.Add(new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.AddressName

            });
        });

        return View(model);
    }

    [HttpPost]
    public IActionResult ElectricalServiceDemand(ServiceDemandPageViewModel model)
    {
        var userId = HttpContext.GetUserId();
        var adresses = _addressRepository.Get(x => x.UserId == userId).ToList();

        adresses.ForEach(x =>
        {
            model.Addresses.Add(new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.AddressName
            });
        });

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var serviceDemand = new ServiceDemand()
        {
            FirstName = model.ServiceDemand.FirstName,
            LastName = model.ServiceDemand.LastName,
            PhoneNumber = model.ServiceDemand.PhoneNumber,
            Problem = model.ServiceDemand.Problem,
            TCKN = model.ServiceDemand.TCKN,
            AddressId = model.ServiceDemand.AddressId,
            Category = Categories.Electric,
            UserId = userId
        };

        _serviceDemandRepository.Insert(serviceDemand);
        _serviceDemandRepository.Save();

        TempData["status"] = "success";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult GasServiceDemand()
    {
        var userId = HttpContext.GetUserId();
        var adresses = _addressRepository.Get(x => x.UserId == userId).ToList();
        var model = new ServiceDemandPageViewModel();

        adresses.ForEach(x =>
        {
            model.Addresses.Add(new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.AddressName

            });
        });

        return View(model);
    }

    [HttpPost]
    public IActionResult GasServiceDemand(ServiceDemandPageViewModel model)
    {
        var userId = HttpContext.GetUserId();
        var adresses = _addressRepository.Get(x => x.UserId == userId).ToList();

        adresses.ForEach(x =>
        {
            model.Addresses.Add(new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.AddressName
            });
        });

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var serviceDemand = new ServiceDemand()
        {
            FirstName = model.ServiceDemand.FirstName,
            LastName = model.ServiceDemand.LastName,
            PhoneNumber = model.ServiceDemand.PhoneNumber,
            Problem = model.ServiceDemand.Problem,
            TCKN = model.ServiceDemand.TCKN,
            AddressId = model.ServiceDemand.AddressId,
            Category = Categories.Gas,
            UserId = userId
        };

        _serviceDemandRepository.Insert(serviceDemand);
        _serviceDemandRepository.Save();

        TempData["status"] = "success";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult PlumbingServiceDemand()
    {
        var userId = HttpContext.GetUserId();
        var adresses = _addressRepository.Get(x => x.UserId == userId).ToList();
        var model = new ServiceDemandPageViewModel();

        adresses.ForEach(x =>
        {
            model.Addresses.Add(new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.AddressName

            });
        });

        return View(model);
    }

    [HttpPost]
    public IActionResult PlumbingServiceDemand(ServiceDemandPageViewModel model)
    {
        var userId = HttpContext.GetUserId();
        var adresses = _addressRepository.Get(x => x.UserId == userId).ToList();

        adresses.ForEach(x =>
        {
            model.Addresses.Add(new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.AddressName
            });
        });

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var serviceDemand = new ServiceDemand()
        {
            FirstName = model.ServiceDemand.FirstName,
            LastName = model.ServiceDemand.LastName,
            PhoneNumber = model.ServiceDemand.PhoneNumber,
            Problem = model.ServiceDemand.Problem,
            TCKN = model.ServiceDemand.TCKN,
            AddressId = model.ServiceDemand.AddressId,
            Category = Categories.Plumbing,
            UserId = userId
        };

        _serviceDemandRepository.Insert(serviceDemand);
        _serviceDemandRepository.Save();

        TempData["status"] = "success";
        return RedirectToAction("Index");
    }
}