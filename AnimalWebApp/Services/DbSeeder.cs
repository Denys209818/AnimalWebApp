using AnimalWebApp.Domain;
using AnimalWebApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalWebApp.Services
{
    public static class DbSeeder
    {
        public static void SeedAll(this IApplicationBuilder app) 
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) 
            {
                var context = scope.ServiceProvider.GetRequiredService<EFDataContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                SeedAnimals(context);
                SeedIdentity(userManager, roleManager);
            }
        }

        private static void SeedAnimals(EFDataContext context) 
        {
            if (!context.animals.Any()) 
            {
                for (int i = 0; i < 50; i++)
                {
                    context.animals.Add(BogusService.Generate());
                }
                context.SaveChanges();
            }

        }

        private static void SeedIdentity(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager) 
        {
            if (!userManager.Users.Any()) 
            {
                var result = userManager.CreateAsync(new AppUser { UserName = "Admin", Email = "admin@gmail.com" },
                    "1").Result;
            }

            if (!roleManager.Roles.Any()) 
            {
                var result = roleManager.CreateAsync(new AppRole {
                    Name = "ADMIN"
                }).Result;
            }
        }
    }
}
