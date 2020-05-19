using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementASPCOREAPP.Web.Models;
using EmployeeManagementASPCOREAPP.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementASPCOREAPP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(ILogger<AccountController> logger,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager )
        {
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet ]
        [AllowAnonymous]
        public IActionResult Register()
        {
            logger.LogWarning("This is only for Get request");
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel register )
        {
            if(ModelState.IsValid )
            {
                var user = new ApplicationUser
                {
                    UserName = register.Email,
                    Email = register.Email,
                    City = register.City

                };
                var result = await userManager.CreateAsync(user, register.Password);
                if (result.Succeeded )
                {
                   await signInManager.SignInAsync(user, isPersistent: false);
                    logger.LogWarning($"Record Created Sucessfully" + register.Email );
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors )
                {
                    ModelState.AddModelError("", error.Description);
                    logger.LogInformation("Error created while creating record {0}",error.Description );
                }
            }
           
            
            return View(register);
        }

        [HttpPost ]
        public async Task<IActionResult > Logout()
        {
            await signInManager.SignOutAsync();
            logger.LogWarning($"User is signoff " +  HttpContext.User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet ]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost ]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
        {
            if(ModelState.IsValid )
            {
                var result = await  signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true );
                if(result.Succeeded )
                {
                    if(!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "Home");
                    }
                  
                }
                if(result.IsLockedOut )
                {
                    return View("Lockout");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempts");
              
                
            }
            return View(model);
        }
        [HttpGet]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email{email } already in use");
            }

        }
    }
}