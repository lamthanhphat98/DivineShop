using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.View_Model
{
    public class Cash
    {
        public String Option { get; set; }
        [MaxLength(10)]
        public String Seri { get; set; }
        [MaxLength(10)]
        public String Pin { get; set; }
    }
}
