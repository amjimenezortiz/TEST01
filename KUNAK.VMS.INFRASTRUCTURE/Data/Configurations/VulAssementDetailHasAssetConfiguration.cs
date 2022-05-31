using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class VulAssementDetailHasAssetConfiguration : IEntityTypeConfiguration<VulAssementDetailHasAsset>
    {
        public void Configure(EntityTypeBuilder<VulAssementDetailHasAsset> builder)
        {
            builder.HasKey(e => new { e.IdVulnerabilityAssessmentDetail, e.IdAsset })
                    .HasName("PK__vul_asse__49527A4513418B76");

            builder.ToTable("vul_assement_detail_has_asset");

            builder.Property(e => e.IdVulnerabilityAssessmentDetail).HasColumnName("idVulnerabilityAssessmentDetail");

            builder.Property(e => e.IdAsset).HasColumnName("idAsset");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdAssetNavigation)
                .WithMany(p => p.VulAssementDetailHasAssets)
                .HasForeignKey(d => d.IdAsset)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_28");

            builder.HasOne(d => d.IdVulnerabilityAssessmentDetailNavigation)
                .WithMany(p => p.VulAssementDetailHasAssets)
                .HasForeignKey(d => d.IdVulnerabilityAssessmentDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_27");
        }
    }
}

