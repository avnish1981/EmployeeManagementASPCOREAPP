using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementASPCOREAPP.Web.Models;
using EmployeeManagementASPCOREAPP.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementASPCOREAPP.Web.Controllers
{
    [Authorize(Roles ="admin")]
    //[Authorize(Roles = "users")]

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
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
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

        [HttpGet ]
        public async Task<IActionResult> EditUsersInRole(string roleid)
        {
            ViewBag.roleId = roleid;
           var role = await roleManaer.FindByIdAsync(roleid);
            if(role==null)
            {
                ViewBag.ErrorMessage = $"Role with {roleid } cannot be found";
                return Redirect("NotFound");
            }

            var model = new  List<UserRoleViewModel>();
            foreach(var user in userManager.Users )
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if(await userManager.IsInRoleAsync(user,role.Name ))
                {
                    userRoleViewModel.IsSelected = true;

                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }


            return View(model );
        }

        [HttpPost ]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model,string roleId)
        {
            var role = await roleManaer.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with {roleId } cannot be found";
                return Redirect("NotFound");
            }
            for(int i=0;i<model .Count;i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if(model[i].IsSelected && !(await userManager.IsInRoleAsync(user,role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!(model[i].IsSelected) && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync (user, role.Name);
                }
                else
                {
                    continue;
                }
                if(result.Succeeded )
                {
                    if(i<model.Count-1)
                    {
                        continue;
                    }
                    
                }
            }

            return RedirectToAction("EditRole", new { roleid = roleId });
        }

        


       
    }
}