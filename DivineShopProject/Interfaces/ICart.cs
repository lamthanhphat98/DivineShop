using DivineShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Interfaces
{
    public interface ICart
    {
        IEnumerable<Cart> GetCarts { get; }
        Cart GetById(int id);
        IEnumerable<Cart> GetByUSerId(String userId);
        void UpdateCart(Cart Cart);
        void AddCart(Cart Cart);
        int GetAllItems(String userId);
        void Remove(Cart Cart);
        void RemoveAll(String username);
        Cart GetCartByUser(String user,int id);   
    }
}
