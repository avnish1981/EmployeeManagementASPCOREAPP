using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementASPCOREAPP.Web.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

        public string roleid { get; set; }
        [Required(ErrorMessage ="Role Name Required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
