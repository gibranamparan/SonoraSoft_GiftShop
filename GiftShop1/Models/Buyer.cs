using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftShop1.Models
{
    public class Buyer : ApplicationUser
    {
        public string token { get; set; }

        public virtual ICollection<PurchaseCart> purchases { get; set; }

        public Buyer() { }

        /// <summary>
        /// Custom constructor for buyers users
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        public Buyer(string userName, string email) : base(userName, email) {
        }

        public class VMBuyer:ApplicationUser.VMUser
        {
            public VMBuyer() { }
            public VMBuyer(Buyer buyer):base(buyer) { }
        }
    }
}
