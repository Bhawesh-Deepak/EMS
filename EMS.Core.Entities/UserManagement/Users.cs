using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.UserManagement
{
    [Table("Users", Schema = "UserManagement")]
    public class Users : Base<int>
    {
        [Required(ErrorMessage ="Name is required !")]
        [Display(Prompt ="Please enter Name")]
        [MaxLength(250,ErrorMessage ="Name is too Large")]
        public string Name { get; set; }

        [Required(ErrorMessage = "User Name is required !")]
        [Display(Prompt = "Please enter User Name")]
        [MaxLength(250, ErrorMessage = "User Name is too Large")]
        public string UserName { get; set; }

        [Display(Prompt = "Please enter Email Id")]
        [MaxLength(250, ErrorMessage = "User Name is too Large")]
        public string EmailId { get; set; }

        [Display(Prompt = "Please enter Phone")]
        [MaxLength(100, ErrorMessage = "Phone# is too Large")]
        public string Phone { get; set; }


        [Display(Prompt = "Please enter Password")]
        [Required(ErrorMessage ="Password is required !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Display(Prompt = "Please enter Role Name")]
        [MaxLength(100, ErrorMessage = "User Name is too Large !")]
        public string RoleName { get; set; }


        [NotMapped]
        [Display(Prompt = "Please enter Confirm Pasword")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
