using DivineShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> GetUsers { get; }
        User GetById(String id);
        void AddUser(User user);
        void Update(User user);
        
    }
}
