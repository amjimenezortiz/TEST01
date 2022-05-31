using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class CvssDetailConfiguration : IEntityTypeConfiguration<CvssDetail>
    {
        public void Configure(EntityTypeBuilder<CvssDetail> builder)
        {
            builder.HasKey(e => e.IdCvssDetail)
                    .HasName("PK__CVSS_det__C926232D79ADF478");

            builder.ToTable("CVSS_detail");

            builder.Property(e => e.IdCvssDetail).HasColumnName("idCvssDetail");

            builder.Property(e => e.Availability)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("availability");

            builder.Property(e => e.AverageScore).HasColumnName("averageScore");

            builder.Property(e => e.BaseScore).HasColumnName("baseScore");

            builder.Property(e => e.Compexity)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("compexity");

            builder.Property(e => e.Confidentiality)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("confidentiality");

            builder.Property(e => e.EnvironmentScore).HasColumnName("environmentScore");

            builder.Property(e => e.ExploitAccess)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("exploitAccess");

            builder.Property(e => e.ExploitabilitySubscore).HasColumnName("exploitabilitySubscore");

            builder.Property(e => e.IdVulnerabilityAssessmentDetail).HasColumnName("idVulnerabilityAssessmentDetail");

            builder.Property(e => e.ImpactSubscore).HasColumnName("impactSubscore");

            builder.Property(e => e.Integrity)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("integrity");

            builder.Property(e => e.PrivilegeLevel)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("privilegeLevel");

            builder.Property(e => e.ReliabilityLevel)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("reliabilityLevel");

            builder.Property(e => e.Scope)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("scope");

            builder.Property(e => e.TemporaryPunctuation).HasColumnName("temporaryPunctuation");

            builder.Property(e => e.UserInteration)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("userInteration");

            builder.Property(e => e.Vector)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("vector");

            builder.Property(e => e.VulnerabilityFix)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("vulnerabilityFix");

            builder.HasOne(d => d.IdVulnerabilityAssessmentDetailNavigation)
                .WithMany(p => p.CvssDetails)
                .HasForeignKey(d => d.IdVulnerabilityAssessmentDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_59");
        }
    }
}
