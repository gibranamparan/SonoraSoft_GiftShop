namespace GiftShop1.Migrations
{
    using GiftShop1.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GiftShop1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GiftShop1.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            //Create initial categories as seeds for testing
            string[] categories = new string[] { "Collares", "Pulseras", "Libros y Revistas", "Posters", "Figuras" };
            foreach(var cat in categories)
                context.Categories.AddOrUpdate( c => c.name,new Category { name =  cat } );

            //Se crean los roles que se manejaran en el sistema inicialmente
            string[] roles = ApplicationUser.RoleNames.ROLES_ARRAY;
            foreach (string rol in roles)
            {
                if (!context.Roles.Any(r => r.Name == rol))
                {
                    var store = new RoleStore<IdentityRole>(context);
                    var manager = new RoleManager<IdentityRole>(store);
                    var role = new IdentityRole { Name = rol };

                    manager.Create(role);
                }
            }

            //Se crea el usuario admin por defecto
            //Username : admin@admin.com
            //password: admin123*
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var PasswordHash = new PasswordHasher();
            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var newUser = new ApplicationUser
                {
                    Email = "admin@admin.com",
                    PasswordHash = PasswordHash.HashPassword("admin123*"),
                    UserName = "admin@admin.com",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                UserManager.Create(newUser);
                UserManager.AddToRole(newUser.Id, ApplicationUser.RoleNames.ADMIN);
            }
        }
    }
}
