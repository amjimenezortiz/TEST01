using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class EvidenceConfiguration : IEntityTypeConfiguration<Evidence>
    {
        public void Configure(EntityTypeBuilder<Evidence> builder)
        {
            builder.HasKey(e => e.IdEvidence)
                    .HasName("PK__evidence__2F339041549B8F64");

            builder.ToTable("evidences");

            builder.Property(e => e.IdEvidence).HasColumnName("idEvidence");

            builder.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");

            builder.Property(e => e.FileExtension)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("fileExtension");

            builder.Property(e => e.Filename)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("filename");

            builder.Property(e => e.IdVulnerabilityAssessment).HasColumnName("idVulnerabilityAssessment");
        }
    }
}

