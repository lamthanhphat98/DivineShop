using DivineShopProject.Interfaces;
using DivineShopProject.Models;
using DivineShopProject.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Reposity
{
    public class LikeReposity : ILike
    {
        private DbConnection _connection;

        public LikeReposity(DbConnection Connection)
        {
            _connection = Connection;
        }

        public void AddLike(Like like)
        {
            _connection.Add(like);
            _connection.SaveChanges();
        }

        public void DisLike(string username, int id)
        {
            var like = _connection.Like.Where(l => l.UserId == username && l.ProductId == id).FirstOrDefault();
            _connection.Remove(like);
            _connection.SaveChanges();
        }

        public IEnumerable<Like> GetAllLike(string username)
        {
            return _connection.Like.Where(l=>l.UserId == username);
        }

        public Like GetLike(string username, int id)
        {
            return _connection.Like.Where(l => l.UserId == username && l.ProductId == id).FirstOrDefault();
        }
    }
}
