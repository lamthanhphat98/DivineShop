using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public String CustomerId { get; set; }
        public String Fullname { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        public Double Price { get; set; }
        [Range(1,99999)]
        public int CardSerialNumber { get; set; }
        public Boolean Accept { get; set; }

        public Order()
        {
        }

        public Order(int orderId, string customerId, string fullname, string address, string phone, string email, double price, bool accept)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Fullname = fullname;
            Address = address;         
            Phone = phone;
            Email = email;
            Price = price;
            Accept = accept;
        }
    }
}
