using DivineShopProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.View_Model
{
    public class RegisterForm
    {
        public String Username { get; set; }

        [DataType(DataType.Password)]
        public String Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        public String Fullname { get; set; }
        public bool Admin { get; set; }     


     
        [Compare("Password", ErrorMessage = "Confirm must match Password")]
        public String ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        public String NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Confirm must match New Password")]
        public String ConfirmNewPassword { get; set; }


    }
}
