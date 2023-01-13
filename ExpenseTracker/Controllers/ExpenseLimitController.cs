using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpenseLimitController : Controller
    {
        // GET: ExpenseLimit
        public ActionResult ShowLimit()
        {
            if (Session["Username"] != null)
            {
                AccountBusinessLayer accountBusiness = new AccountBusinessLayer();
                List<ExpenseLimit> limits = accountBusiness.GetExpenseLimit(Session["Username"].ToString()).ToList();
                return View(limits);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        [ActionName("AddLimit")]
        public ActionResult AddLimit_Get()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ActionName("AddLimit")]
        public ActionResult AddLimit_Post(FormCollection formCollection)
        {
            if (Session["Username"] != null)
            {
                AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                int userId = businessLayer.getUserId(Session["Username"].ToString());

                ExpenseLimit expenseLimit = new ExpenseLimit();
                expenseLimit.ExpLimit = (float)Convert.ToDouble(formCollection["ExpLimit"]);
                expenseLimit.UserId = userId;

                businessLayer.addExpenseLimit(expenseLimit);

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        [ActionName("EditLimit")]
        public ActionResult EditLimit_Get(int id)
        {
            if (Session["Username"] != null)
            {
                AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                ExpenseLimit limit = new ExpenseLimit();
                IEnumerable<ExpenseLimit> expenseLimits = businessLayer.GetExpenseLimit(Session["Username"].ToString()).ToList();
                limit = expenseLimits.Single(s => s.Id == id);
                return View(limit);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ActionName("EditLimit")]
        public ActionResult EditLimit_Post(ExpenseLimit limit)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                    businessLayer.editExpenseLimit(limit, Session["Username"].ToString());
                    return RedirectToAction("ShowLimit");
                }
                return View(limit);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                    businessLayer.deleteExpenseLimit(id, Session["Username"].ToString());
                    return RedirectToAction("ShowLimit");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}