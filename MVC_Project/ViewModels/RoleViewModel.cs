using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project_PL.ViewModels
{
    public class RoleViewModel
    {
        public string id { get; set; }
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }

        public RoleViewModel()
        {
            id = Guid.NewGuid().ToString();
        }

    }
}
