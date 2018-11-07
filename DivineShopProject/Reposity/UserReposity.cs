using DivineShopProject.Interfaces;
using DivineShopProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DivineShopProject.Reposity
{
    public class UserReposity : IUser
    {
        private DbConnection _connection;

        public UserReposity(DbConnection Connection)
        {
            _connection = Connection;
        }
        public IEnumerable<User> GetUsers => _connection.Users;

        public void AddUser(User user)
        {
            _connection.Add(user);
            _connection.SaveChanges();
        }

        public User GetById(string id)
        {
            return _connection.Users.Find(id);
        }

        

        public void Update(User user)
        {
            _connection.Entry(user).State = EntityState.Modified;
            _connection.SaveChanges();
        }
       
   
      

     
    }
}
