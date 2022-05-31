using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class AditionalDetailConfiguration : IEntityTypeConfiguration<AditionalDetail>
    {
        public void Configure(EntityTypeBuilder<AditionalDetail> builder)
        {
            builder.HasKey(e => e.IdAditionalDetail)
                    .HasName("PK__aditiona__A7DA410F321C1FBD");

            builder.ToTable("aditional_detail");

            builder.Property(e => e.IdAditionalDetail).HasColumnName("idAditionalDetail");

            builder.Property(e => e.DescriptionHtml)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("descriptionHtml");

            builder.Property(e => e.IdVulnerabilityAssessmentDetail).HasColumnName("idVulnerabilityAssessmentDetail");

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdVulnerabilityAssessmentDetailNavigation)
                .WithMany(p => p.AditionalDetails)
                .HasForeignKey(d => d.IdVulnerabilityAssessmentDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_60");
        }
    }
}
