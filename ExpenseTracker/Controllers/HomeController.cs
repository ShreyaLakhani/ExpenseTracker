using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public void fillDropDown()
        {
            AccountBusinessLayer businessLayer = new AccountBusinessLayer();
            List<ExpenseCategory> categoryNames = businessLayer.GetExpenseCategoryNames().ToList();
            ViewBag.CategoryList = new SelectList(categoryNames, "CategoryId", "CategoryName");
        }

        [HttpGet]
        [ActionName("Dashboard")]
        public ActionResult Dashboard_Get()
        {
            if (Session["Username"] != null)
            {
                fillDropDown();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ActionName("Dashboard")]
        public ActionResult Dashboard_Post(FormCollection formCollection)
        {
            if (Session["Username"] != null)
            {
                AccountBusinessLayer businessLayer = new AccountBusinessLayer();

                int CategoryId = Convert.ToInt32(formCollection["CategoryId"]);
                List<Expense> expenses = businessLayer.GetCategoryWiseExpense(CategoryId, Session["Username"].ToString()).ToList();

                float categoryLimit = businessLayer.getCategoryLimit(CategoryId);
                ViewBag.CategoryLimit = "Category Limit : "+categoryLimit;

                float categoryTotalAmount = businessLayer.getCategoryWiseTotalAmount(CategoryId, Session["Username"].ToString());
                ViewBag.CategoryTotalAmount = "Expense Amount : " + categoryTotalAmount;

                return View("DisplayGrid", expenses);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}