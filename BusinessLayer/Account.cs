using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Account
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Email ID")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Username")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Password")]
        public string Password { get; set; }
    }
}
