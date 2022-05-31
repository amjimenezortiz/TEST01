using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class DetailHasEvidenceConfiguration : IEntityTypeConfiguration<DetailHasEvidence>
    {
        public void Configure(EntityTypeBuilder<DetailHasEvidence> builder)
        {
            builder.HasKey(e => new { e.IdVulnerabilityAssessmentDetail, e.IdEvidence })
                    .HasName("PK__detail_h__9181E4C9CB268BF9");

            builder.ToTable("detail_has_evidences");

            builder.Property(e => e.IdVulnerabilityAssessmentDetail).HasColumnName("idVulnerabilityAssessmentDetail");

            builder.Property(e => e.IdEvidence).HasColumnName("idEvidence");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdEvidenceNavigation)
                .WithMany(p => p.DetailHasEvidences)
                .HasForeignKey(d => d.IdEvidence)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_44");

            builder.HasOne(d => d.IdVulnerabilityAssessmentDetailNavigation)
                .WithMany(p => p.DetailHasEvidences)
                .HasForeignKey(d => d.IdVulnerabilityAssessmentDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_43");
        }
    }
}

