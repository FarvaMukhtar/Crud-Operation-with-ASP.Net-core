using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace CrudCoreProcUpd.Models
{
    public class npmodel
    {
        
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please! Enter Your Name")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Name should be more than or equal to four characters.")]
        public string name { get; set; }
        public List<SelectListItem> UsernameList { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please! Enter Your Password")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[.,])[a-zA-Z\d.,]{6}$",
        ErrorMessage = "Password must be include Alphabets , Numeric values dot or comma ")]
        public string password { get; set; }
    }
}
