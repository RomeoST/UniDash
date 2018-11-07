using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UniDash.Model.Models;

namespace UniDash
{
    public class UserInitializer
    {
        public static async Task InitializeAsync(UserManager<DutUser> userManager, RoleManager<DutRole> roleManager)
        {
            var defaultUser = "romeost";
            var defaultPassword = "123456";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new DutRole("admin"));
            }

            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new DutRole("user"));
            }

            if (await userManager.FindByNameAsync(defaultUser) == null)
            {
                var dutUser = new DutUser {UserName = defaultUser};
                var result = await userManager.CreateAsync(dutUser, defaultPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(dutUser, "admin");
                }
            }
        }
    }
}
