using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GiftShop1.Models
{
    public class Product
    {
        public int productID { get; set; }

        [DisplayName("Product")]
        public string name { get; set; }
        [DisplayName("Price")]
        public decimal price { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }

        //A product correspond to one category
        public int? categoryID { get; set; }
        public virtual Category category { get; set; }

        //Can be purchased many times
        public virtual ICollection<ProductInCart> purchasesHistory { get; set; }

        /// <summary>
        /// Product View Model
        /// </summary>
        public class VMProduct
        {
            public int productID { get; set; }

            public string name { get; set; }
            public decimal price { get; set; }
            public string categoryName { get; set; }
            public int categoryID { get; set; }
            public string description { get; set; }

            public VMProduct(Product p)
            {
                this.productID = p.productID;
                this.name = p.name;
                this.price = p.price;
                this.categoryName = p.category == null ? string.Empty : p.category.name;
                this.categoryID = p.categoryID.HasValue ? p.categoryID.Value : 0;
                this.description = p.description;
            }
        }
    }
}