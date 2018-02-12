using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GiftShop1.Models
{
    public class PurchaseCart
    {
        [Key]
        public int purchaseID { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime boughtAt { get; set; }

        [ForeignKey("buyer")]
        public string buyerID { get; set; }
        public virtual Buyer buyer { get; set; }

        public virtual ICollection<ProductInCart> products { get; set; }
    }
}