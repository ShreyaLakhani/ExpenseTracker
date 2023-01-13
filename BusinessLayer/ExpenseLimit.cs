using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ExpenseLimit
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Expense Limit")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Expense Limit")]
        [RegularExpression("([0-9])[0-9]*[.]?[0-9]*", ErrorMessage = "Expense Amount must be a number")]
        public float ExpLimit { get; set; }

        public int UserId { get; set; }
    }
}
