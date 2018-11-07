using DivineShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Interfaces
{
    public interface IProduct
    {
        IEnumerable<Product> Products { get; set; }
        Product GetById(int id);
        IEnumerable<Product> SearchUser(String value);
        IEnumerable<Product> SortByAsc();
        IEnumerable<Product> SortByDes();
        void Create(Product product);
        void Remove(int id);
        void Update(Product product);
        IEnumerable<Product> GetProductsByCategory(int Category);
    }
}
