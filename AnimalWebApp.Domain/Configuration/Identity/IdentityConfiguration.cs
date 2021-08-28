using AnimalWebApp.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalWebApp.Domain.Configuration.Identity
{
    public class IdentityConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasKey(keys => new { keys.RoleId, keys.UserId });

            builder.HasOne(virtualElementForEntityUser => virtualElementForEntityUser.User)
                .WithMany(virtualCollectionForEntityUser => virtualCollectionForEntityUser.UserRoles)
                .HasForeignKey(intElementForForeignEntity => intElementForForeignEntity.UserId)
                .IsRequired();

            builder.HasOne(virtualElementForEntityRole => virtualElementForEntityRole.Role)
                .WithMany(virtualCollectionForEntityRole => virtualCollectionForEntityRole.UserRoles)
                .HasForeignKey(intElementForEntityRole => intElementForEntityRole.RoleId)
                .IsRequired();
        }
    }
}
