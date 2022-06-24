using System.Text;
using System.Text.Encodings.Web;
using indiGo.Business.Repositories.Abstract;
using indiGo.Core.Emails;
using indiGo.Core.Entities;
using indiGo.Core.Identity;
using indiGo.Core.Services;
using indiGo.Core.ViewModels;
using indiGo.Data.Identity;
using indiGo.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace indiGo.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IRepository<Address, int> _addressRepository;
    private readonly IRepository<ServiceDemand, int> _serviceDemandRepository;
    private readonly IEmailService _emailService;

    public async Task addRoles()
    {
        foreach (string role in Roles.RoleList)
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = role });
        }
    }

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IRepository<Address, int> addressRepository, RoleManager<IdentityRole> roleManager, IRepository<ServiceDemand, int> serviceDemandRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _addressRepository = addressRepository;
        _roleManager = roleManager;
        _serviceDemandRepository = serviceDemandRepository;
        //addRoles();
    }


    [HttpGet]
    public IActionResult Register()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Profile");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Profile");
        }

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Bir hata oluştu!");
            return View(model);
        }

        var user = new ApplicationUser()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Username
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Bir hata oluştu!");
            return View(model);
        }


        var count = _userManager.Users.Count();
        await _userManager.AddToRoleAsync(user, count == 1 ? Roles.Admin : Roles.Passive);
        //await _userManager.AddToRoleAsync(user, Roles.Operator);


        if (await _userManager.IsInRoleAsync(user, Roles.Passive))
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                protocol: Request.Scheme);

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
                Body = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Clicking here</a>.",
                Subject = "Confirm your email"
            };
            await _emailService.SendEmailAsync(emailMessage);
        }


        return RedirectToAction("Login");

    }

    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Profile");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Profile");
        }

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
            return View(model);
        }

        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Böyle bir kullanıcı yok.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

        if (result.Succeeded)
        {
            HttpContext.Session.SetString("User", System.Text.Json.JsonSerializer.Serialize<UserViewModel>(new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RegisterDate = user.RegisterDate
            }));

            if (await _userManager.IsInRoleAsync(user,Roles.Operator))
            {
                return RedirectToAction("ServiceDemands", "Operator");
            }

            if (await _userManager.IsInRoleAsync(user, Roles.Admin))
            {
                return RedirectToAction("Products", "Admin");
            }
            if (await _userManager.IsInRoleAsync(user, Roles.Customer) ||
                await _userManager.IsInRoleAsync(user, Roles.Passive))
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("WaitingDemands", "Service");
        }
        else if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "Çok fazla yanlış giriş denemesi yaptınız.");
            return View(model);
        }

        ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre yanlış.");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return View(0);
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return View(0);
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded)
        {
            return View(0);
        }

        await _userManager.RemoveFromRoleAsync(user, Roles.Passive);
        await _userManager.AddToRoleAsync(user, Roles.Customer);

        return View(1);
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var name = HttpContext.User.Identity.Name;
        var user = await _userManager.FindByNameAsync(name);

        var uservm = new UserViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            RegisterDate = user.RegisterDate,
            PhoneNumber = user.PhoneNumber
        };

        return View(uservm);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Profile(UserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var name = HttpContext.User.Identity.Name;
        var user = await _userManager.FindByNameAsync(name);

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.PhoneNumber = model.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            HttpContext.Session.SetString("User", System.Text.Json.JsonSerializer.Serialize<UserViewModel>(new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RegisterDate = user.RegisterDate
            }));
            model.RegisterDate = user.RegisterDate;

            TempData["status"] = "success";

            return View(model);

        }

        var message = string.Join("<br>", result.Errors.Select(x => x.Description));
        ViewBag.Message = message;

        return View(model);
    }

    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var name = HttpContext.User.Identity.Name;
        var user = await _userManager.FindByNameAsync(name);
        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (result.Succeeded)
        {
            TempData["status"] = "success";
            return View();
        }

        TempData["status"] = "error";
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "CUSTOMER,PASSIVE")]
    public async Task<IActionResult> MyDemands()
    {
        var demands = _serviceDemandRepository.Get(x => x.UserId == HttpContext.GetUserId()).ToList();
        return View(demands);
    }

    [HttpGet]
    [Authorize(Roles = "CUSTOMER,PASSIVE")]
    public async Task<IActionResult> Addresses()
    {
        var name = HttpContext.User.Identity.Name;
        var user = await _userManager.FindByNameAsync(name);
        var addresses = _addressRepository.Get(x => x.UserId == user.Id).ToList();
        var model = new AddressPageViewModel
        {
            Addresses = addresses
        };
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "CUSTOMER,PASSIVE")]
    public async Task<IActionResult> Addresses(AddressPageViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var name = HttpContext.User.Identity.Name;
        var user = await _userManager.FindByNameAsync(name);

        if (model.AddressViewModel != null)
        {
            var address = new Address
            {
                AddressName = model.AddressViewModel.AddressName,
                City = model.AddressViewModel.City,
                District = model.AddressViewModel.District,
                Neighborhood = model.AddressViewModel.Neighborhood,
                Street = model.AddressViewModel.Street,
                ApartmentNo = model.AddressViewModel.ApartmentNo,
                FlatNo = model.AddressViewModel.FlatNo,
                AddressInfo = model.AddressViewModel.AddressInfo,
                UserId = user.Id
            };

            _addressRepository.Insert(address);
            _addressRepository.Save();
        }
        else if (model.EditAddressViewModel != null)
        {
            var address = _addressRepository.GetById(model.EditAddressViewModel.Id);
            address.City = model.EditAddressViewModel.City;
            address.District = model.EditAddressViewModel.District;
            address.Neighborhood = model.EditAddressViewModel.Neighborhood;
            address.Street = model.EditAddressViewModel.Street;
            address.ApartmentNo = model.EditAddressViewModel.ApartmentNo;
            address.FlatNo = model.EditAddressViewModel.FlatNo;
            address.AddressInfo = model.EditAddressViewModel.AddressInfo;
            address.AddressName = model.EditAddressViewModel.AddressName;
            _addressRepository.Save();
            TempData["status"] = "success";
            return RedirectToAction("Addresses");
        }



        return RedirectToAction("Addresses");
    }

    [HttpPost]
    [Authorize(Roles = "CUSTOMER,PASSIVE")]
    public IActionResult DeleteAddresses(int id)
    {
        var address = _addressRepository.GetById(id);
        if (address == null)
        {
            TempData["status"] = "error";
        }

        _addressRepository.Delete(address);
        _addressRepository.Save();

        return RedirectToAction("Addresses");
    }

    [HttpGet]
    [Authorize]
    public IActionResult AccessDenied()
    {
        return View();
    }



}