using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class TreatmentConfiguration : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> builder)
        {
            builder.HasKey(e => e.IdTreatment)
                    .HasName("PK__treatmen__AD393C7585A6D5FC");

            builder.ToTable("treatment");

            builder.Property(e => e.IdTreatment).HasColumnName("idTreatment");

            builder.Property(e => e.Cost).HasColumnName("cost");

            builder.Property(e => e.Currency)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("currency");

            builder.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");

            builder.Property(e => e.IdResponsableArea).HasColumnName("idResponsableArea");

            builder.Property(e => e.IdResponsablePerson).HasColumnName("idResponsablePerson");

            builder.Property(e => e.IdSupervisorArea).HasColumnName("idSupervisorArea");

            builder.Property(e => e.IdSupervisorPerson).HasColumnName("idSupervisorPerson");

            builder.Property(e => e.MaximalEndDate)
                .HasColumnType("datetime")
                .HasColumnName("maximalEndDate");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.Order).HasColumnName("order");

            builder.Property(e => e.ProposedEndDate)
                .HasColumnType("datetime")
                .HasColumnName("proposedEndDate");

            builder.Property(e => e.RealEndDate)
                .HasColumnType("datetime")
                .HasColumnName("realEndDate");

            builder.Property(e => e.StatusDescription)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("statusDescription");
        }
    }
}
