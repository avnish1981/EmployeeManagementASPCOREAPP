using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementASPCOREAPP.Web.Models;
using EmployeeManagementASPCOREAPP.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementASPCOREAPP.Web.Controllers
{
    //[Route("Pragim/Home")]
    public class HomeController : Controller
    //public class AvnishController : Controller
    {
        private readonly IEmployeeRepositary _employeeRepositary;
       // public AvnishController(IEmployeeRepositary employeeRepositary)
        public HomeController(IEmployeeRepositary employeeRepositary)
        {
            this._employeeRepositary = employeeRepositary;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        //[Route("")]
        //[Route("~/")]
        //[Route("Index")]
        //public ViewResult GetEmployeeAllList()
        public ViewResult   Index()
        {
            var model =  _employeeRepositary.GetAllEmployee();
            // return View("/Views/Home/Index.cshtml",model);
            return View( model);
        }

        //public JsonResult Details()
        //{
        //    var model = _employeeRepositary.GetEmployee(1);
        //    return Json(model);
        //}

        //public ObjectResult  Details()
        //{
        //    var model = _employeeRepositary.GetEmployee(1);
        //    return new ObjectResult(model);
        //}
        //[Route("Details/{id}")]
        //[Route("Details")]
       // public ViewResult GetDetailforEmployee(int? id)
        public ViewResult Details(int? id)
        {
            throw new Exception("This is exception Test");
            //var model = _employeeRepositary.GetEmployee(1);
            //ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Information";
            //ViewBag.Employee = model;
            //ViewBag.PageTitle = "Employee Information";
            //return View(model);
            //return View("Test");
            //return View("../Test/Update");
            //return View("../../MyView/Index");
            Employee emp = _employeeRepositary.GetEmployee(id.Value);
            if(emp == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            
            HomeDetailsViewModel model = new HomeDetailsViewModel();
            model.PageTitle = "Employee Information";
            model.Employee = emp;//_employeeRepositary.GetEmployee(id??1);
            //return View("/Views/Home/Details.cshtml",model);
            return View( model);
        }
        [HttpGet]
        public ViewResult Create()
        {
            
            return View();
        }
        [HttpPost ]
        public   ActionResult  Create(Employee emp)
        {
            var model = _employeeRepositary.Add(emp);
            if(model !=null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }
       
        [HttpGet]
        public ViewResult Delete(int id)
        {
            var model = _employeeRepositary.GetEmployee(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult  Delete(Employee emp)
        {
            var model = _employeeRepositary.Delete(emp);
            if(model !=null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet ]
        public ViewResult Update(int id)
        {
            var model = _employeeRepositary.GetEmployee(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult  Update(Employee emp)
        {
            if (ModelState.IsValid)
            {
                var model = _employeeRepositary.Update(emp);
                               
                return RedirectToAction("Details",new { id = emp.Id });
            }
            return View();

        }
    }
}