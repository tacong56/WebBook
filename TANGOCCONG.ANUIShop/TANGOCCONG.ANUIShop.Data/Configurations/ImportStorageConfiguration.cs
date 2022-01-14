using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.Data.Configurations
{
    public class ImportStorageConfiguration : IEntityTypeConfiguration<ImportStorage>
    {
        public void Configure(EntityTypeBuilder<ImportStorage> builder)
        {
            builder.ToTable("ImportStorages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.Property(x => x.ProductId).IsRequired();

            //builder.HasOne(x => x.AppUser).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);

        }
    }
}
