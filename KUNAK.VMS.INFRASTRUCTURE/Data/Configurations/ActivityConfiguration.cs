using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.HasKey(e => e.IdActivity)
                    .HasName("PK__activiti__C34866C02D45B303");

            builder.ToTable("activities");

            builder.Property(e => e.IdActivity).HasColumnName("idActivity");

            builder.Property(e => e.Description)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("description");

            builder.Property(e => e.Enddate)
                .HasColumnType("datetime")
                .HasColumnName("enddate");

            builder.Property(e => e.IdResponsablePerson).HasColumnName("idResponsablePerson");

            builder.Property(e => e.IdTreatment).HasColumnName("idTreatment");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.Startdate)
                .HasColumnType("datetime")
                .HasColumnName("startdate");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.StatusDescription)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("statusDescription");

            builder.HasOne(d => d.IdTreatmentNavigation)
                .WithMany(p => p.Activities)
                .HasForeignKey(d => d.IdTreatment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_66");
        }
    }
}
