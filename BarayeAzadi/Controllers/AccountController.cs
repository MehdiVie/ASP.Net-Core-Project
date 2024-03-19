using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using BarayeAzadi.Application.Common.Utility;
using BarayeAzadi.Domain.Entities;
using BarayeAzadi.Infrastructure.Data;
using BarayeAzadi.Web.ViewModels;

namespace BarayeAzadi.Web.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
                                UserManager<ApplicationUser> userManager, 
                                SignInManager<ApplicationUser> signInManager, 
                                RoleManager<IdentityRole> roleManager)
        {
            
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index), "Home");
            
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Login(string returnUrl=null)
        {
            returnUrl ??= Url.Content("~/");

            LoginVM loginVM = new()
            {
                ReturnUrl = returnUrl
            };

            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe,
                                                                      lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user=await _userManager.FindByEmailAsync(loginVM.Email);
                    if(await _userManager.IsInRoleAsync(user,SD.Role_Admin))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    
                    if (!string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return LocalRedirect(loginVM.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt!");
                }
            }

            return View(loginVM);
        }

        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");



            RegisterVM registerVM = new()
            {

                RoleList = _roleManager.Roles.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Name
                }),

                RedirectUrl = returnUrl

            };

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            { 
            
            ApplicationUser user = new()
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                UserName = registerVM.Email,
                NormalizedEmail = registerVM.Email.ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = registerVM.PhoneNumber,
                CreatedAt = DateTime.Now
            };
            

            var result = await  _userManager.CreateAsync(user, registerVM.Password);

            if (result.Succeeded)
            {
                if(!string.IsNullOrEmpty(registerVM.Role))
                {
                    await _userManager.AddToRoleAsync(user, registerVM.Role);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                if(!string.IsNullOrEmpty(registerVM.RedirectUrl))
                {
                    return LocalRedirect(registerVM.RedirectUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            else
            {
                foreach (var error in result.Errors)
                {
                  ModelState.AddModelError("", error.Description);
                }
            }

            }


            registerVM.RoleList = _roleManager.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Name
            });

 
            return View(registerVM);
        }
    }
}
