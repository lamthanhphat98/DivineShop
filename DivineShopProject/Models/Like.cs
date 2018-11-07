using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Username")]
        public String UserId { get; set; }
        [ForeignKey("Id")]
        public int ProductId { get; set; }
        public Product product { get; set; }
    }
}
