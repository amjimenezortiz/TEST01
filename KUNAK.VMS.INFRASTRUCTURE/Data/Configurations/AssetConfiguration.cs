using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(e => e.IdAsset)
                    .HasName("PK__assets__A20A788C7D22FBB0");

            builder.ToTable("assets");

            builder.Property(e => e.IdAsset).HasColumnName("idAsset");

            builder.Property(e => e.IdAssetType).HasColumnName("idAssetType");

            builder.Property(e => e.IdCriticality).HasColumnName("idCriticality");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdAssetTypeNavigation)
                .WithMany(p => p.Assets)
                .HasForeignKey(d => d.IdAssetType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_17");

            builder.HasOne(d => d.IdCriticalityNavigation)
                .WithMany(p => p.Assets)
                .HasForeignKey(d => d.IdCriticality)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_22");
        }
    }
}