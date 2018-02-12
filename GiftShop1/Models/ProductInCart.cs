using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GiftShop1.Models
{
    public class ProductInCart
    {
        public int id { get; set; }
        public int qty { get; set; }

        public int productID { get; set; }
        public virtual Product product { get; set; }

        [ForeignKey("buyer")]
        public string buyerID { get; set; }
        public virtual Buyer buyer { get; set; }
    }
}