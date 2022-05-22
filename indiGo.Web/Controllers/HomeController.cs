using System.Text;
using System.Text.Encodings.Web;
using indiGo.Core.Emails;
using indiGo.Core.Identity;
using indiGo.Core.Services;
using indiGo.Core.ViewModels;
using indiGo.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace indiGo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            
        }

        public async void addRoles()
        {
            foreach (var role in Roles.RoleList)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
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

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action("ConfirmEmail", "Home", new {userId = user.Id, code = code},
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

            return RedirectToAction("Login");

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
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
                return RedirectToAction("Index","Home");
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

    }
}
