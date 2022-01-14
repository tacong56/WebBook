using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.Data.Configurations
{
    public class ExportStorageConfiguration : IEntityTypeConfiguration<ExportStorage>
    {
        public void Configure(EntityTypeBuilder<ExportStorage> builder)
        {
            builder.ToTable("ExportStorages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.Property(x => x.ProductId).IsRequired();

            //builder.HasOne(x => x.AppUser).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);

        }
    }
}
