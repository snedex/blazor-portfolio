﻿using Microsoft.AspNetCore.Identity;

namespace ServerAPI.Data
{
    public static class IdentitySeed
    {
        internal const string AdministratorRoleName = "Administrator";
        internal const string AdministratorUserName = "admin@sneddon.dev";

        internal async static Task SeedIdentity(RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager, string adminPassword)
        {
            await SeedAdminRole(roleManager);

            await SeedAdminUser(userManager, adminPassword);
        }

        private async static Task SeedAdminRole(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                return;

            await roleManager.CreateAsync(new IdentityRole(AdministratorRoleName));
        }

        private async static Task SeedAdminUser(UserManager<IdentityUser> userManager, string adminPassword)
        {
            if (await userManager.FindByEmailAsync(AdministratorUserName) != null)
                return;

            var result = await userManager.CreateAsync(new IdentityUser(AdministratorUserName), adminPassword);

        }
    }
}
