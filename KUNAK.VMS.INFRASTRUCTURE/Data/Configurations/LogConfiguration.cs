using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(e => e.IdLog)
                    .HasName("PK__logs__3C7153CACFE8AC6F");

            builder.ToTable("logs");

            builder.Property(e => e.IdLog).HasColumnName("idLog");

            builder.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");

            builder.Property(e => e.Description)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("description");

            builder.Property(e => e.Event)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("event");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Table)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("table");
        }
    }
}

