using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        // GET: ExpenseCategory
        public ActionResult ShowCategory()
        {
            if (Session["Username"] != null)
            {
                AccountBusinessLayer accountBusiness = new AccountBusinessLayer();
                List<ExpenseCategory> categories = accountBusiness.GetExpenseCategories().ToList();
                return View(categories);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        [ActionName("AddCategory")]
        public ActionResult AddCategory_Get()
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
        [ActionName("AddCategory")]
        public ActionResult AddCategory_Post(FormCollection formCollection)
        {
            if (Session["Username"] != null)
            {
                AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                float totalExpenseLimit = businessLayer.getTotalExpenseLimit(Session["Username"].ToString());
                float totalCategoryAmount = businessLayer.getTotalCategoryAmount();
                if (totalCategoryAmount < totalExpenseLimit)
                {
                    float currentExpense = totalCategoryAmount + (float)Convert.ToDouble(formCollection["CategoryLimit"]);

                    if (currentExpense <= totalExpenseLimit)
                    {
                        ExpenseCategory expenseCategory = new ExpenseCategory();
                        expenseCategory.CategoryName = formCollection["CategoryName"];
                        expenseCategory.CategoryLimit = (float)Convert.ToDouble(formCollection["CategoryLimit"]);

                        businessLayer.addExpenseCategory(expenseCategory);
                        return RedirectToAction("ShowCategory");
                    }
                    else
                    {
                        Response.Write("<script>alert('Category limit is exceeding the total expense limit');</script>");
                        return View();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Category limit is exceeding the total expense limit');</script>");
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        [ActionName("EditCategory")]
        public ActionResult EditCategory_Get(int id)
        {
            if (Session["Username"] != null)
            {
                AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                ExpenseCategory expenseCategory = new ExpenseCategory();
                IEnumerable<ExpenseCategory> expenseCategories = businessLayer.GetExpenseCategories().ToList();
                expenseCategory = expenseCategories.Single(s => s.CategoryId == id);
                return View(expenseCategory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ActionName("EditCategory")]
        public ActionResult EditCategory_Post(ExpenseCategory expenseCategory)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                    businessLayer.editExpenseCategory(expenseCategory);
                    return RedirectToAction("ShowCategory");
                }
                return View(expenseCategory);
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
                    businessLayer.deleteExpenseCategory(id);
                    return RedirectToAction("ShowCategory");
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