using AnimalWebApp.Domain.Configuration;
using AnimalWebApp.Domain.Configuration.Identity;
using AnimalWebApp.Domain.Entities;
using AnimalWebApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalWebApp.Domain
{
    public class EFDataContext : IdentityDbContext<AppUser, AppRole,long, IdentityUserClaim<long>, AppUserRole,
        IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public EFDataContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<AppAnimal> animals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Identity config
                builder.ApplyConfiguration(new IdentityConfiguration());
            #endregion

            #region Animal Configuration
                builder.ApplyConfiguration(new AnimalConfiguration());
            #endregion
        }
    }
}
