using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalWebApp.Domain.Configuration
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
        public void DefaultConfiguration(EntityTypeBuilder<TEntity> builder) 
        {
            builder.HasKey("Id");
            builder.Property("DateCreated").IsRequired();
        }
    }
}
