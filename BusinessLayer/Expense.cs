using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Expense
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Expense Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Expense Title")]
        public string Title { get; set; }

        [DisplayName("Expense Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Expense Description")]
        public string Description { get; set; }

        [DisplayName("Expense Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Expense Date/ Time")]
        public DateTime ExpDate { get; set; }

        [DisplayName("Expense Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Expense Amount")]
        [RegularExpression("([0-9])[0-9]*[.]?[0-9]*", ErrorMessage = "Expense Amount must be a number")]
        public float Amount { get; set; }

        [DisplayName("Expense Category")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Select Expense Category")]
        public int CategoryId { get; set; }

        public int UserId { get; set; }

        [DisplayName("Expense Category")]
        public string CategoryName { get; set; }

        public IEnumerable<Expense> Expenses { get; set; }
    }
}
