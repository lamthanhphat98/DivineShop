using DivineShopProject.Interfaces;
using DivineShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Reposity
{
    public class CategoryReposity : ICategory
    {
        private DbConnection _connection;

        public CategoryReposity(DbConnection Connection)
        {
            _connection = Connection;
        }
        public IEnumerable<Category> GetCategories => _connection.Categories;

        public void Create(Category category)
        {
            _connection.Add(category);
            _connection.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            return _connection.Categories.Find(id);
        }

        public Category GetCategoryByName(string name)
        {
            return _connection.Categories.Where(c => c.CategoryName == name).FirstOrDefault();
        }
    }
}
