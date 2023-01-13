using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ExpenseCategory
    {
        [DisplayName("Category ID")]
        public int CategoryId { get; set; }

        [DisplayName("Expense Category Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Expense Category Name")]
        public string CategoryName { get; set; }

        [DisplayName("Expense Category Limit")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Expense Category Limit")]
        [RegularExpression("([0-9])[0-9]*[.]?[0-9]*", ErrorMessage = "Expense Amount must be a number")]
        public float CategoryLimit { get; set; }
    }
}
