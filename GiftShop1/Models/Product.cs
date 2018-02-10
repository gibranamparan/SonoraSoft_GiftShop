using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftShop1.Models
{
    public class Product
    {
        public int productID { get; set; }

        public string name { get; set; }
        public decimal price { get; set; }

        public virtual ICollection<PurchaseCart> purchases { get; set; }

        public class VMProduct
        {
            public int productID { get; set; }

            public string name { get; set; }
            public decimal price { get; set; }

            public VMProduct(Product p)
            {
                this.productID = p.productID;
                this.name = p.name;
                this.price = p.price;
            }
        }
    }
}