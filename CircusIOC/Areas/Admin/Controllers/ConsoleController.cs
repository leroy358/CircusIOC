using CircusIOC.Models;
using CircusIOC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircusIOC.Areas.Admin.Controllers
{
    public class ConsoleController : Controller
    {
        //AdminService AdminService { get; set; }
        //public ConsoleController() { }
        //public ConsoleController(AdminService adminService)
        //{
        //    this.AdminService = adminService;
        //}
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Admins admin = Service.AdminService.GetByNamePsw(username, password);
            if (admin != null)
            {
                Session["admin"] = admin.AdminName;
                Session.Timeout = 2 * 60;
                return Json("Success");
            }
            else
            {
                return Json("Error");
            }
            //return Json("Error");
        }
        public ActionResult Main()
        {
            if (Session["admin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
	}
}