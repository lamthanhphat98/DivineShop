using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Picture { get; set; }
        public Double Price { get; set; }
        public int Category { get; set; }
        public int Sale { get; set; }
        public bool Available { get; set; }
        public Product()
        {

        }

        public Product(int id, string name, string description, string picture, Double price, int _category , int sale, bool available)
        {
            Id = id;
            Name = name;
            Description = description;
            Picture = picture;
            Price = price;
            Category = _category;
            Sale = sale;
            Available = available;
        }
    }
}
