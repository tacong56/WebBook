using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.Data.Configurations
{
    class WardConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> builder)
        {
            builder.ToTable("Wards");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(6);
            builder.Property(x => x.Name).HasMaxLength(45);
            builder.Property(x => x.Type).HasMaxLength(45);
            builder.Property(x => x.District_Id).HasMaxLength(6);
        }
    }
}
