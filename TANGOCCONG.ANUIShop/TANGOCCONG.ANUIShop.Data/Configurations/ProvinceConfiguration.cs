﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.Data.Configurations
{
    class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("Districts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(6);
            builder.Property(x => x.Name).HasMaxLength(45);
            builder.Property(x => x.Type).HasMaxLength(45);
            builder.Property(x => x.Province_Id).HasMaxLength(6);
        }
    }
}
