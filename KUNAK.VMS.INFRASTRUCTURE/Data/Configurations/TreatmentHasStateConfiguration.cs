using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class TreatmentHasStateConfiguration : IEntityTypeConfiguration<TreatmentHasState>
    {
        public void Configure(EntityTypeBuilder<TreatmentHasState> builder)
        {
            builder.HasKey(e => e.IdTreatmentHasStates)
                    .HasName("PK__treatmen__D229291ABC7F7D78");

            builder.ToTable("treatment_has_states");

            builder.Property(e => e.IdTreatmentHasStates).HasColumnName("idTreatmentHasStates");

            builder.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");

            builder.Property(e => e.IdResponsablePerson).HasColumnName("idResponsablePerson");

            builder.Property(e => e.IdTreatment).HasColumnName("idTreatment");

            builder.Property(e => e.IdTreatmentStates).HasColumnName("idTreatmentStates");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdTreatmentNavigation)
                .WithMany(p => p.TreatmentHasStates)
                .HasForeignKey(d => d.IdTreatment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_69");

            builder.HasOne(d => d.IdTreatmentStatesNavigation)
                .WithMany(p => p.TreatmentHasStates)
                .HasForeignKey(d => d.IdTreatmentStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_68");
        }
    }
}
