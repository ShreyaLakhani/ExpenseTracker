using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: Expense
        public ActionResult ShowExpense()
        {
            if (Session["Username"] != null)
            {
                AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                List<Expense> expenses = businessLayer.GetExpense(Session["Username"].ToString()).ToList();
                return View(expenses);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public void fillDropDown()
        {
            AccountBusinessLayer businessLayer = new AccountBusinessLayer();
            List<ExpenseCategory> categoryNames = businessLayer.GetExpenseCategoryNames().ToList();
            ViewBag.CategoryList = new SelectList(categoryNames, "CategoryId", "CategoryName");
        }

        [HttpGet]
        [ActionName("AddExpense")]
        public ActionResult AddExpense_Get()
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
        [ActionName("AddExpense")]
        public ActionResult AddExpense_Post(FormCollection formCollection)
        {
            if (Session["Username"] != null)
            {
                fillDropDown();

                AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                float categoryLimit = businessLayer.getCategoryLimit(Convert.ToInt32(formCollection["CategoryId"]));
                float totalCategoryExpenseAmount = businessLayer.getCategoryWiseTotalAmount(Convert.ToInt32(formCollection["CategoryId"]), Session["Username"].ToString());
                float totalExpenseLimit = businessLayer.getTotalExpenseLimit(Session["Username"].ToString());
                float totalExpenseAmount = businessLayer.getTotalExpenseAmount(Session["Username"].ToString());

                if (totalExpenseAmount < totalExpenseLimit)
                {
                    float currentExpense = totalExpenseAmount + (float)Convert.ToDouble(formCollection["Amount"]);

                    if (currentExpense <= totalExpenseLimit)
                    {
                        if (totalCategoryExpenseAmount < categoryLimit)
                        {
                            float currentAddedExpense = totalCategoryExpenseAmount + (float)Convert.ToDouble(formCollection["Amount"]);

                            if (currentAddedExpense <= categoryLimit)
                            {
                                int userId = businessLayer.getUserId(Session["Username"].ToString());

                                Expense expense = new Expense();
                                expense.Title = formCollection["Title"];
                                expense.Description = formCollection["Description"];
                                expense.ExpDate = Convert.ToDateTime(formCollection["ExpDate"]);
                                expense.Amount = (float)Convert.ToDouble(formCollection["Amount"]);
                                expense.CategoryId = Convert.ToInt32(formCollection["CategoryId"]);
                                expense.UserId = userId;
                                businessLayer.addExpense(expense);

                                return RedirectToAction("ShowExpense");
                            }
                            else
                            {
                                Response.Write("<script>alert('Expense is exceeding the category limit');</script>");
                                return View();
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Expense is exceeding the category limit');</script>");
                            return View();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Expense is exceeding the total expense limit');</script>");
                        return View();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Expense is exceeding the total expense limit');</script>");
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        [ActionName("EditExpense")]
        public ActionResult EditExpense_Get(int id)
        {
            if (Session["Username"] != null)
            {
                fillDropDown();
                AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                Expense expense = new Expense();
                IEnumerable<Expense> expenses = businessLayer.GetExpense(Session["Username"].ToString()).ToList();
                expense = expenses.Single(s => s.Id == id);
                return View(expense);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ActionName("EditExpense")]
        public ActionResult EditExpense_Post(Expense expense)
        {
            if (Session["Username"] != null)
            {
                fillDropDown();

                if (ModelState.IsValid)
                {
                    AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                    businessLayer.editExpense(expense, Session["Username"].ToString());
                    return RedirectToAction("ShowExpense");
                }
                return View(expense);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult DeleteExpense(int id)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    AccountBusinessLayer businessLayer = new AccountBusinessLayer();
                    businessLayer.deleteExpense(id, Session["Username"].ToString());
                    return RedirectToAction("ShowExpense");
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