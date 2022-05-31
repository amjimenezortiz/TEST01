using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(e => e.IdPermission)
                    .HasName("PK__permissi__A08B0681C952C4FE");

            builder.ToTable("permissions");

            builder.Property(e => e.IdPermission).HasColumnName("idPermission");

            builder.Property(e => e.ForClient).HasColumnName("forClient");

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");
        }
    }
}

