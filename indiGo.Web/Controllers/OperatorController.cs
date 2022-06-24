using System.Text;
using System.Text.Encodings.Web;
using indiGo.Business.Repositories.Abstract;
using indiGo.Core.Emails;
using indiGo.Core.Entities;
using indiGo.Core.Identity;
using indiGo.Core.Services;
using indiGo.Core.ViewModels;
using indiGo.Core.ViewModels.PageViewModels;
using indiGo.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace indiGo.Web.Controllers;

[Authorize(Roles = "OPERATOR")]
public class OperatorController : Controller
{
    private readonly IRepository<ServiceDemand, int> _serviceDemandRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository<Address, int> _addressRepository;
    private readonly IEmailService _emailService;


    public OperatorController(IRepository<ServiceDemand, int> serviceDemandRepository, UserManager<ApplicationUser> userManager, IRepository<Address, int> addressRepository, IEmailService emailService)
    {
        _serviceDemandRepository = serviceDemandRepository;
        _userManager = userManager;
        _addressRepository = addressRepository;
        _emailService = emailService;
    }

    [HttpGet]
    public async Task<IActionResult> ServiceDemands()
    {
        var model = new AdminWaitingDemandsPageViewModel();

        var electricalTechnicsA = await _userManager.GetUsersInRoleAsync(Roles.ElectricalService);
        var gasTechnicsA = await _userManager.GetUsersInRoleAsync(Roles.GasService);
        var plumbingTechnicsA = await _userManager.GetUsersInRoleAsync(Roles.PlumbingService);

        var electricalTechnics = new List<ApplicationUser>(electricalTechnicsA);
        var gasTechnics = new List<ApplicationUser>(gasTechnicsA);
        var plumbingTechnics = new List<ApplicationUser>(plumbingTechnicsA);


        var serviceDemands = _serviceDemandRepository.Get().ToList();
        serviceDemands.ForEach(x =>
        {
            if (x.ServiceId != null)
            {
                return;
            }
            var address = _addressRepository.GetById(x.AddressId);
            model.ServiceDemands.Add(new ServiceDemandViewModel()
            {
                Id = x.Id,
                AddressId = x.AddressId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Problem = x.Problem,
                TCKN = x.TCKN,
                Category = x.Category,
                Address = address
            });
        });

        electricalTechnics.ForEach(x =>
        {
            model.ElectricalTechnics.Add(new UserViewModel()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                RegisterDate = x.RegisterDate,
                Id = x.Id
            });
        });


        gasTechnics.ForEach(x =>
        {
            model.GasTechnics.Add(new UserViewModel()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                RegisterDate = x.RegisterDate,
                Id = x.Id
            });
        });

        plumbingTechnics.ForEach(x =>
        {
            model.PlumbingTechnics.Add(new UserViewModel()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                RegisterDate = x.RegisterDate,
                Id = x.Id
            });
        });

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ServiceDemands(int demandId, string serviceId)
    {
        var user = await _userManager.FindByIdAsync(serviceId);
        var demand = _serviceDemandRepository.GetById(demandId);
        if (user == null || demand == null)
        {
            return View();
        }
        demand.ServiceId = serviceId;
        _serviceDemandRepository.Save();



        var emailMessage = new MailModel()
        {
            To = new List<EmailModel>
            {
                new EmailModel
                {
                    Email = user.Email,
                    Name = user.FirstName
                }
            },
            Body = $"Servis talebiniz onaylanmıştır. En kısa zamanda sizinle iletişime geçilecektir. İyi günler.",
            Subject = "Servis Talebi Onaylandı"
        };
        await _emailService.SendEmailAsync(emailMessage);



        TempData["status"] = "success";
        return RedirectToAction("ServiceDemands");
    }

    [HttpGet]
    public async Task<IActionResult> AssignedDemands()
    {
        var model = new AdminWaitingDemandsPageViewModel();

        var electricalTechnicsA = await _userManager.GetUsersInRoleAsync(Roles.ElectricalService);
        var gasTechnicsA = await _userManager.GetUsersInRoleAsync(Roles.GasService);
        var plumbingTechnicsA = await _userManager.GetUsersInRoleAsync(Roles.PlumbingService);

        var electricalTechnics = new List<ApplicationUser>(electricalTechnicsA);
        var gasTechnics = new List<ApplicationUser>(gasTechnicsA);
        var plumbingTechnics = new List<ApplicationUser>(plumbingTechnicsA);


        var serviceDemands = _serviceDemandRepository.Get().ToList();
        serviceDemands.ForEach(async x =>
        {
            if (x.ServiceId == null)
            {
                return;
            }
            var address = _addressRepository.GetById(x.AddressId);
            var service = await _userManager.FindByIdAsync(x.ServiceId);
            model.ServiceDemands.Add(new ServiceDemandViewModel()
            {
                Id = x.Id,
                AddressId = x.AddressId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Problem = x.Problem,
                TCKN = x.TCKN,
                Category = x.Category,
                Address = address,
                Service = new UserViewModel()
                {
                    FirstName = service.FirstName,
                    LastName = service.LastName,
                    Id = service.Id,
                    Email = service.Email,
                    PhoneNumber = service.PhoneNumber,
                    RegisterDate = service.RegisterDate
                }
            });
        });

        electricalTechnics.ForEach(x =>
        {
            model.ElectricalTechnics.Add(new UserViewModel()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                RegisterDate = x.RegisterDate,
                Id = x.Id
            });
        });


        gasTechnics.ForEach(x =>
        {
            model.GasTechnics.Add(new UserViewModel()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                RegisterDate = x.RegisterDate,
                Id = x.Id
            });
        });

        plumbingTechnics.ForEach(x =>
        {
            model.PlumbingTechnics.Add(new UserViewModel()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                RegisterDate = x.RegisterDate,
                Id = x.Id
            });
        });

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Services()
    {
        var electicalServices = new List<ApplicationUser>(await _userManager.GetUsersInRoleAsync(Roles.ElectricalService));
        var gasServices = new List<ApplicationUser>(await _userManager.GetUsersInRoleAsync(Roles.GasService));
        var plumbingServices = new List<ApplicationUser>(await _userManager.GetUsersInRoleAsync(Roles.PlumbingService));

        var model = new ServicesViewModel();

        electicalServices.ForEach(x =>
        {
            model.ElectricalServices.Add(new UserViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                RegisterDate = x.RegisterDate
            });
        });
        gasServices.ForEach(x =>
        {
            model.GasServices.Add(new UserViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                RegisterDate = x.RegisterDate
            });
        });
        plumbingServices.ForEach(x =>
        {
            model.PlumbingServices.Add(new UserViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                RegisterDate = x.RegisterDate,
                Email = x.Email
            });
        });


        return View(model);
    }
}
