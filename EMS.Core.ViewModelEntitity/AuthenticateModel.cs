using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.ViewModelEntitity
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage ="User name is required !")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required !")]
        public string Password { get; set; }
        public string Message { get; set; }
        public string ReturnUrl { get; set; }
    }
}
