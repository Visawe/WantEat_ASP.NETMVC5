namespace Eat.Migrations
{
    using Eat.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Eat.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Eat.Models.ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

          
            var role1 = new IdentityRole { Name = "Admin" };
            var role2 = new IdentityRole { Name = "User" };
            var role3 = new IdentityRole { Name = "Manager_Rest" };

            
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

           
            var admin = new ApplicationUser { Email = "ssamyraika@gmail.com", UserName = "ssamyraika@gmail.com" };
            string password = "121233TestAd.";
            var result = userManager.Create(admin, password);

            
            if (result.Succeeded)
            {
                
                userManager.AddToRole(admin.Id, role1.Name);
            }

            base.Seed(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
