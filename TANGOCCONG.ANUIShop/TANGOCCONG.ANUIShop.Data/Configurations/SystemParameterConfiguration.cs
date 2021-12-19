using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.Data.Configurations
{
    public class SystemParameterConfiguration : IEntityTypeConfiguration<SystemParameter>
    {
        public void Configure(EntityTypeBuilder<SystemParameter> builder)
        {
            builder.ToTable("SystemParameters");
            builder.Property(x => x.Type).HasMaxLength(50);
            builder.Property(x => x.Name).HasMaxLength(50);
        }
    }
}
