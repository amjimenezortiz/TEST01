using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class ScopeConfiguration : IEntityTypeConfiguration<Scope>
    {
        public void Configure(EntityTypeBuilder<Scope> builder)
        {
            builder.HasKey(e => e.IdScope)
                    .HasName("PK__scope__84EEB173CE882704");

            builder.ToTable("scope");

            builder.Property(e => e.IdScope).HasColumnName("idScope");

            builder.Property(e => e.IdVulnerabilityAssessment).HasColumnName("idVulnerabilityAssessment");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdVulnerabilityAssessmentNavigation)
                .WithMany(p => p.Scopes)
                .HasForeignKey(d => d.IdVulnerabilityAssessment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_50");
        }
    }
}
