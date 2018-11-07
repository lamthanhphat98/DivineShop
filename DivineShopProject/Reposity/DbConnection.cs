
using DivineShopProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Reposity
{
    public class DbConnection : DbContext
    {
        public DbConnection (DbContextOptions<DbConnection> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> OrderDetail { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Like { get; set; }
    }
}
