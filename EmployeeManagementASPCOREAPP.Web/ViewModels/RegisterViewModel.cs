using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementASPCOREAPP.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress )]
        public string  Email { get; set; }
        [Required ]
        [DataType(DataType.Password )]
        public string Password { get; set; }
        [Required ]
        [DataType(DataType.Password )]
        [Compare("Password",ErrorMessage ="Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }
        [Required ]
        public string City { get; set; }
    }
}
