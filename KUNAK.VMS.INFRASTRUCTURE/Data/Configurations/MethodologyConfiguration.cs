using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class MethodologyConfiguration : IEntityTypeConfiguration<Methodology>
    {
        public void Configure(EntityTypeBuilder<Methodology> builder)
        {
            builder.HasKey(e => e.IdMethodologie)
                    .HasName("PK__methodol__D0135F21E6A7F7B6");

            builder.ToTable("methodologies");

            builder.Property(e => e.IdMethodologie).HasColumnName("idMethodologie");

            builder.Property(e => e.Description)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("description");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");
        }
    }
}
