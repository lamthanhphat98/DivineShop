using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }      
        public int Id { get; set; }
        [ForeignKey("Id")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime DateBuy { get; set; }
        public String UserId { get; set; }

        public Cart()
        {

        }
        public Cart(int _id, Product _product, int _quantity, DateTime _datebuy, String _userId)
        {
            Id = _id;
            Product = _product;
            Quantity = _quantity;
            DateBuy = _datebuy;
            UserId = _userId;
        }

    }
}
