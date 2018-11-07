using System;
using System.ComponentModel.DataAnnotations;

namespace DivineShopProject.Models
{
    public class User
    {
        [Key]
        public String Username { get; set; }
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="Email must be right format")]
        public String Email { get; set; }
        public String Fullname { get; set; }
        public bool Admin { get; set; }
        public double Cash { get; set; }

        public User()
        {
        }

        public User(string username, string password, string email, string fullname, bool admin)
        {
            Username = username;
            Password = password;
            Email = email;
            Fullname = fullname;
            Admin = admin;
        }
    }
}
