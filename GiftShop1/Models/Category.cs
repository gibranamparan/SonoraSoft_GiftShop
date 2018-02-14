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

        public class VMCategory
        {
            public int categoryID { get; set; }
            public string name { get; set; }

            public VMCategory(Category c)
            {
                categoryID = c.categoryID;
                name = c.name;
            }
        }
    }
}