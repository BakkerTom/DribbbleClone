using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dribbble.Models;
using Dribbble.Models.Repositories;
using System.Web.Script.Serialization;

namespace Dribbble.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository accountRepo = new AccountRepository();

        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Models.Account account)
        {
            //Signup flow
            if (ModelState.IsValid)
            {
                accountRepo.Insert(account);
                RedirectToAction("Login");
            }
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
                //Wanneer de account/password combinatie klopt
                if (accountRepo.isValid(account.AccountName, account.Password))
                {
                    //Haal de volledige account data op
                    Account fullAccount = accountRepo.getByAccountName(account.AccountName);
                    FormsAuthentication.SetAuthCookie(fullAccount.ID.ToString(), account.RememberMe);

                    return RedirectToAction("Index", "Shot");
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
            return RedirectToAction("Index", "Shot");
        }
    }
}