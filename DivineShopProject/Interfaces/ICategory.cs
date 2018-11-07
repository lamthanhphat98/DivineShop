using DivineShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Interfaces
{
    public interface ICategory
    {
        IEnumerable<Category> GetCategories { get; }
        Category GetCategoryById(int id);
        Category GetCategoryByName(String name);
        void Create(Category category);
    }
}
