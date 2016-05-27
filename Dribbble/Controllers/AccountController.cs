using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dribbble.Models.Repositories;

namespace Dribbble.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository accountRepo = new AccountRepository();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Account account)
        {
            if (ModelState.IsValid)
            {
                if (accountRepo.isValid(account.AccountName, account.Password))
                {
                    FormsAuthentication.SetAuthCookie(account.AccountName, account.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(account);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}