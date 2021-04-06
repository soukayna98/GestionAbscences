using GestionAbscences.Areas.Admin.Models;
using GestionAbscences.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GestionAbscences.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginInfo)
        {
           var adminService = new AdminService();
           var isLoggedIn =  adminService.Login(loginInfo.Id, loginInfo.Password);
            if (isLoggedIn)
            {
                return RedirectToAction("Index", "Default");
            }
            else
            {
                loginInfo.Message = "email or pass incorrect";
                return View(loginInfo);
            }

        }
    }
}