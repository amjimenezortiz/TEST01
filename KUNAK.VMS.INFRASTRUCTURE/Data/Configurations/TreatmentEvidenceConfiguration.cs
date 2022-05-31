using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class TreatmentEvidenceConfiguration : IEntityTypeConfiguration<TreatmentEvidence>
    {
        public void Configure(EntityTypeBuilder<TreatmentEvidence> builder)
        {
            builder.HasKey(e => e.IdTreatmentEvidences)
                    .HasName("PK__treatmen__D0B93EC37FE15A2D");

            builder.ToTable("treatment_evidences");

            builder.Property(e => e.IdTreatmentEvidences).HasColumnName("idTreatmentEvidences");

            builder.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");

            builder.Property(e => e.Description)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("description");

            builder.Property(e => e.Filename)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("filename");

            builder.Property(e => e.IdTreatment).HasColumnName("idTreatment");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdTreatmentNavigation)
                .WithMany(p => p.TreatmentEvidences)
                .HasForeignKey(d => d.IdTreatment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_67");
        }
    }
}
