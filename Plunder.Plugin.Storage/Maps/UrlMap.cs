using System.Data.Entity.ModelConfiguration;
using Plunder.Plugin.Storage.Models;

namespace Plunder.Plugin.Storage.Maps
{
    public class UrlMap : EntityTypeConfiguration<Url>
    {
        public UrlMap()
        {
            this.ToTable("Url");
            this.HasKey(e => e.Id);
            this.Property(e => e.SiteId).IsOptional().HasMaxLength(300);
            this.Property(e => e.Hash).IsRequired().HasMaxLength(100);
            this.Property(e => e.Value).IsRequired().HasMaxLength(300);
            this.Property(e => e.HttpMethod).IsRequired().HasMaxLength(10);
            this.Property(e => e.UrlType).IsRequired();
            this.Property(e => e.Status).IsRequired();
            this.Property(e => e.AlreadyRetryCount).IsRequired();
        }
    }
}
