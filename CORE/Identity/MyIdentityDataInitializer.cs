using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Identity
{
  public static  class MyIdentityDataInitializer
    {
        

        public static void SeedUsers
    (UserManager<ApplicationUser> userManager)
        {
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(Roles.Admin).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = Roles.Admin;
                // role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync(Roles.User).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = Roles.User;
                // role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
        public static void SeedData
(UserManager<ApplicationUser> userManager,
RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
    }
}
