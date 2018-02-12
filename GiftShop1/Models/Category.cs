using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GiftShop1.Models
{
    public class Category
    {
        public int categoryID { get; set; }

        [DisplayName("Category")]
        public string name { get; set; }

        public virtual ICollection<Product> products { get; set; }
    }
}