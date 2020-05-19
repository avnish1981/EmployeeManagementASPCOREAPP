using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementASPCOREAPP.Web.Models;
using EmployeeManagementASPCOREAPP.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementASPCOREAPP.Web.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManaer;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministratorController(RoleManager<IdentityRole> roleManaer,UserManager<ApplicationUser> userManager )
        {
            this.roleManaer = roleManaer;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManaer.Roles;

            return View(roles);
        }
        [HttpGet ]
        public IActionResult CreateRole()
        {

            return View();
        }

        [HttpPost ]
        public async  Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManaer.CreateAsync(identityRole);
                if(result.Succeeded )
                {
                    return RedirectToAction("ListRoles", "Administrator");
                }
                else
                {
                    foreach(var error in result.Errors )
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
           
         

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string roleid)
        {
            var role = await roleManaer.FindByIdAsync(roleid);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with {roleid } cannot be found";
                return Redirect("NotFound");
            }

            var model = new EditRoleViewModel
            {
                roleid  = role.Id,
                RoleName = role.Name
            };
            //Feaching all the users from DB.
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManaer.FindByIdAsync(model.roleid );
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with {model.roleid } cannot be found";
                return Redirect("NotFound");
            }
            else
            {
                role.Name = model.RoleName;


                var result = await roleManaer.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View(model);
            }

        }
        //[HttpGet ]
        //public async  Task<IActionResult> Delete(string id)
        //{
        //    var role = await roleManaer.FindByIdAsync(id);
        //    if(role ==null )
        //    {
        //        ViewBag.ErrorMessage = $"Role with {role.Id  } cannot be found";
        //        return View("NotFound");

        //    }
        //    var model = new DeleteViewModel 
        //    {
        //        Id = role.Id ,
        //        RoleName = role.Name 
        //    };
            

        //    return View(model);
        //}
        [HttpPost ]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            var role = await roleManaer.FindByIdAsync(model.Id );
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with {model.Id  } cannot be found";
                return View("NotFound");

            }
            var result = await roleManaer.DeleteAsync(role);
            if(result.Succeeded )
            {
                 return RedirectToAction("ListRoles");
            }
            else
            {
                foreach(var error in result.Errors )
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


       
    }
}