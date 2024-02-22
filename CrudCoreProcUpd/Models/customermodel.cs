using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace CrudCoreProcUpd.Models
{
    public class customermodel
    {

         

        [Display(Name = "Customer ID")]

        public int id { get; set; }

        [Display(Name = "Name")]
        //[Required(ErrorMessage = "Please! Enter Your Name")]
        //[StringLength(20, MinimumLength = 4, ErrorMessage = "Name should be more than or equal to four characters.")]
        public string name { get; set; }


        [Required(ErrorMessage = "You must provide a Salary")]
        [Display(Name = "Salary")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Salary must be a numeric value.")]
        public string salary { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Enter Your EmailID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string email { get; set; }


        [Required(ErrorMessage = "You must provide a PhoneNumber")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        // [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [RegularExpression(@"^(\+\d{1,4})?[-.\s]?(\d{4}[-.\s]?\d{7}|\d{10})$", ErrorMessage = "Not a valid phone number")]
        public string phoneno { get; set; }

        [Display(Name = "Home Address")]
        [Required(ErrorMessage = "Enter Your Home Address")]
        public string address { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please! Enter Your Password")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[.,])[a-zA-Z\d.,]{6}$",
        ErrorMessage = "Password must be include Alphabets , Numeric values dot or comma ")]
        public string password { get; set; }

        public List<SelectListItem> UsernameList { get; set; }
        public int nmtoid { get; set; }

    }
}
