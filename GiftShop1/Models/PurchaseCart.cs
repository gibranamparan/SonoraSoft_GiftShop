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

        public decimal totalAmount { get
            {
                decimal res = 0;
                if (this.products != null && this.products.Count()>0)
                {
                    res = this.products.Sum(p => p.product == null ? 0 : p.product.price);
                }

                return res;
            }
        } 

        public class VMPurchaseCart
        {
            public int purchaseID { get; set; }
            public DateTime createdAt { get; set; }
            public decimal totalAmount { get; set; }

            public Buyer.VMBuyer buyer { get; set; }
            public List<ProductInCart.VMProductInCart> products { get; set; }

            public VMPurchaseCart(PurchaseCart cart)
            {
                this.purchaseID = cart.purchaseID;
                this.createdAt = cart.createdAt;
                this.buyer = new Buyer.VMBuyer(cart.buyer);
                this.totalAmount = cart.totalAmount;

                this.products = new List<ProductInCart.VMProductInCart>();
                if(cart.products != null && cart.products.Count() > 0)
                {
                    this.products = cart.products.ToList().Select(p => new ProductInCart.VMProductInCart(p)).ToList();
                }
            }
        }
    }
}