using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Principal;

namespace BusinessLayer
{
    public class AccountBusinessLayer
    {
        public bool getAccountDetail(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllAccounts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                    return true;
                else
                    return false;
            }
        }

        public void addAccount(Account account)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddAccount", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", account.Email);
                cmd.Parameters.AddWithValue("@Username", account.Username);
                cmd.Parameters.AddWithValue("@Password", account.Password);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void addExpenseCategory(ExpenseCategory expenseCategory)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddExpenseCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryName", expenseCategory.CategoryName);
                cmd.Parameters.AddWithValue("@CategoryLimit", expenseCategory.CategoryLimit);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<ExpenseCategory> GetExpenseCategories()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            List<ExpenseCategory> expenseCategories = new List<ExpenseCategory>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetExpenseCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ExpenseCategory expenseCategory = new ExpenseCategory();
                    expenseCategory.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                    expenseCategory.CategoryName = rdr["CategoryName"].ToString();
                    expenseCategory.CategoryLimit = (float)Convert.ToDouble(rdr["CategoryLimit"]);
                    expenseCategories.Add(expenseCategory);
                }
            }
            return expenseCategories;
        }

        public void editExpenseCategory(ExpenseCategory expenseCategory)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spEditExpenseCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryId", expenseCategory.CategoryId);
                cmd.Parameters.AddWithValue("@CategoryName", expenseCategory.CategoryName);
                cmd.Parameters.AddWithValue("@CategoryLimit", expenseCategory.CategoryLimit);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteExpenseCategory(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteExpenseCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryId", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<ExpenseCategory> GetExpenseCategoryNames()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            List<ExpenseCategory> expenseCategories = new List<ExpenseCategory>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetCategoryNames", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ExpenseCategory expenseCategory = new ExpenseCategory();
                    expenseCategory.CategoryId = Convert.ToInt32(rdr["CategoryID"]);
                    expenseCategory.CategoryName = rdr["CategoryName"].ToString();
                    expenseCategories.Add(expenseCategory);
                }
            }
            return expenseCategories;
        }

        public int getUserId(string username)
        {
            int userId = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", username);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        userId = Convert.ToInt32(rdr["Id"]);
                    }
                }
            }
            return userId;
        }

        public float getCategoryLimit(int categoryId)
        {
            float categoryLimit = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetCategoryLimit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        categoryLimit = (float)Convert.ToDouble(rdr["CategoryLimit"]);
                    }
                }
            }

            return categoryLimit;
        }

        public float getCategoryWiseTotalAmount(int categoryId, string username)
        {
            int userId = getUserId(username);
            float totalAmount = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetTotalAmount", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        totalAmount += (float)Convert.ToDouble(rdr["Amount"]);
                    }
                }
            }

            return totalAmount;
        }

        public float getTotalExpenseLimit(string username)
        {
            float totalLimit = 0;
            int userId = getUserId(username);

            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetTotalExpenseLimit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        totalLimit = (float)Convert.ToDouble(rdr["ExpLimit"]);
                    }
                }
            }

            return totalLimit;
        }

        public float getTotalExpenseAmount(string username)
        {
            int userId = getUserId(username);
            float totalAmount = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetTotalExpenseAmount", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        totalAmount += (float)Convert.ToDouble(rdr["Amount"]);
                    }
                }
            }
            return totalAmount;
        }

        public void addExpense(Expense expense)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddExpense", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", expense.Title);
                cmd.Parameters.AddWithValue("@Description", expense.Description);
                cmd.Parameters.AddWithValue("@ExpDate", expense.ExpDate);
                cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                cmd.Parameters.AddWithValue("@CategoryId", expense.CategoryId);
                cmd.Parameters.AddWithValue("@UserId", expense.UserId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Expense> GetExpense(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            List<Expense> expenses = new List<Expense>();

            int userId = getUserId(username);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetExpense", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Expense expense = new Expense();
                    expense.Id = Convert.ToInt32(rdr["Id"]);
                    expense.Title = rdr["Title"].ToString();
                    expense.Description = rdr["Description"].ToString();
                    expense.ExpDate = Convert.ToDateTime(rdr["ExpDate"]);
                    expense.Amount = (float)Convert.ToDouble(rdr["Amount"]);
                    //expense.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                    expense.CategoryName = rdr["CategoryName"].ToString();

                    expenses.Add(expense);
                }
            }
            return expenses;
        }

        public void editExpense(Expense expense, string username)
        {
            int userId = getUserId(username);

            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spEditExpense", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", expense.Id);
                cmd.Parameters.AddWithValue("@Title", expense.Title);
                cmd.Parameters.AddWithValue("@Description", expense.Description);
                cmd.Parameters.AddWithValue("@ExpDate", expense.ExpDate);
                cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                cmd.Parameters.AddWithValue("CategoryId", expense.CategoryId);
                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteExpense(int id, string username)
        {
            int userId = getUserId(username);

            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteExpense", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void addExpenseLimit(ExpenseLimit expenseLimit)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddExpenseLimit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExpLimit", expenseLimit.ExpLimit);
                cmd.Parameters.AddWithValue("@UserId", expenseLimit.UserId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<ExpenseLimit> GetExpenseLimit(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            List<ExpenseLimit> expenseLimits = new List<ExpenseLimit>();

            int userId = getUserId(username);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetExpenseLimit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ExpenseLimit expenseLimit = new ExpenseLimit();
                    expenseLimit.Id = Convert.ToInt32(rdr["Id"]);
                    expenseLimit.ExpLimit = (float)Convert.ToDouble(rdr["ExpLimit"]);

                    expenseLimits.Add(expenseLimit);
                }
            }
            return expenseLimits;
        }

        public void editExpenseLimit(ExpenseLimit expenseLimit, string username)
        {
            int userId = getUserId(username);

            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spEditExpenseLimit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", expenseLimit.Id);
                cmd.Parameters.AddWithValue("@ExpLimit", expenseLimit.ExpLimit);
                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteExpenseLimit(int id, string username)
        {
            int userId = getUserId(username);

            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteExpenseLimit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Expense> GetCategoryWiseExpense(int categoryId, string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            List<Expense> expenses = new List<Expense>();

            int userId = getUserId(username);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetCategoryWiseExpense", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Expense expense = new Expense();
                    expense.Id = Convert.ToInt32(rdr["Id"]);
                    expense.Title = rdr["Title"].ToString();
                    expense.Description = rdr["Description"].ToString();
                    expense.ExpDate = Convert.ToDateTime(rdr["ExpDate"]);
                    expense.Amount = (float)Convert.ToDouble(rdr["Amount"]);
                    //expense.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                    expense.CategoryName = rdr["CategoryName"].ToString();

                    expenses.Add(expense);
                }
            }
            return expenses;
        }

        public float getTotalCategoryAmount()
        {
            float totalCategoryAmount = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetTotalCategoryAmount", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        totalCategoryAmount += (float)Convert.ToDouble(rdr["CategoryLimit"]);
                    }
                }
            }

            return totalCategoryAmount;
        }
    }
}