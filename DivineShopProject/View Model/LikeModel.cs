using DivineShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.View_Model
{
    public class LikeModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public String UserId { get; set; }
        public Product product { get; set; }
    }
}
