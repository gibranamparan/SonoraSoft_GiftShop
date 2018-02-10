using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace GiftShop1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public static class RoleNames
        {
            public const string ADMIN = "Admin";
            public const string BUYER = "Buyer";
            public static string[] ROLES_ARRAY = new string[] { BUYER, ADMIN };
        }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser(){}
        public ApplicationUser(string userName, string email) {
            this.UserName = userName;
            this.Email = email;
        }

        /// <summary>
        /// View Model to expose the basic information for the client application.
        /// </summary>
        public class VMUser
        {
            public string id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }

            public VMUser() { }
            public VMUser(ApplicationUser usr) {
                this.id = usr.Id;
                this.UserName = usr.UserName;
                this.Email = usr.Email;
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<GiftShop1.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<GiftShop1.Models.PurchaseCart> Purchases { get; set; }

        public System.Data.Entity.DbSet<GiftShop1.Models.Buyer> ApplicationUsers { get; set; }
    }
}