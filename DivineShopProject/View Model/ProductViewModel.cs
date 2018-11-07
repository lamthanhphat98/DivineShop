using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.View_Model
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Picture { get; set; }
        public Double Price { get; set; }
        public String Category { get; set; }
        public int Sale { get; set; }
        public bool Available { get; set; }
        
    }
}
