using AnimalWebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalWebApp.Domain.Configuration
{
    public class AnimalConfiguration : BaseEntityConfiguration<AppAnimal>
    {
        public override void Configure(EntityTypeBuilder<AppAnimal> builder)
        {
            this.DefaultConfiguration(builder);
            builder.ToTable("tblAnimals");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Birthday).IsRequired();
        }
    }
}
