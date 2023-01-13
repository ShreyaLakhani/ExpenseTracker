using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        [ActionName("Login")]
        public ActionResult Login_Get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login_Post(FormCollection formCollection)
        {
            AccountBusinessLayer accountBusiness = new AccountBusinessLayer();
            bool accountExist = accountBusiness.getAccountDetail(formCollection["Username"].ToString(), formCollection["Password"].ToString());

            if (accountExist == true)
            {
                Response.Write("<script>alert('Login successful');</script>");
                Session["Username"] = formCollection["Username"].ToString();
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                Response.Write("<script>alert('User Not Found, Please Register');</script>");
                return RedirectToAction("Register");
            }
        }

        [HttpGet]
        [ActionName("Register")]
        public ActionResult Register_Get() 
        {
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult Register_Post(FormCollection formCollection) 
        {
            Account account = new Account();
            account.Email = formCollection["Email"];
            account.Username = formCollection["Username"];
            account.Password = formCollection["Password"];

            AccountBusinessLayer accountBusiness = new AccountBusinessLayer();
            accountBusiness.addAccount(account);
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}