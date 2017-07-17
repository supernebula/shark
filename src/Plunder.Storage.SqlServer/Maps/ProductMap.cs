using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Storage.SqlServer.Models;

namespace Plunder.Storage.SqlServer.Maps
{
    class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.ToTable("Product");
            this.HasKey(e => e.Id);
            this.Property(e => e.Title).IsRequired().HasMaxLength(100).HasColumnName("Title");
            this.Property(e => e.Description).IsOptional().HasMaxLength(8000);
            this.Property(e => e.Price).IsRequired().HasColumnAnnotation("商品价格", "value");
            this.Property(e => e.PicUri).IsOptional().HasMaxLength(300);
            this.Property(e => e.Uri).IsRequired().HasMaxLength(300);
            this.Property(e => e.CommentCount).IsRequired();
            this.Property(e => e.SiteName).IsRequired().HasMaxLength(100);
            this.Property(e => e.SiteDomain).IsRequired().HasMaxLength(100);
            this.Property(e => e.ElapsedSecond).IsRequired();
            this.Property(e => e.Downloader).IsRequired().HasMaxLength(100);
            this.Property(e => e.CreateTime).IsRequired();
            this.Property(e => e.UpdateTime).HasPrecision(10);
        }
    }
}
