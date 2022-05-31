using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class RoleHasPermissionConfiguration : IEntityTypeConfiguration<RoleHasPermission>
    {
        public void Configure(EntityTypeBuilder<RoleHasPermission> builder)
        {
            builder.HasKey(e => new { e.IdPermission, e.IdRol })
                    .HasName("PK__role_has__D34374767FB5FA34");

            builder.ToTable("role_has_permissions");

            builder.Property(e => e.IdPermission).HasColumnName("idPermission");

            builder.Property(e => e.IdRol).HasColumnName("idRol");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdPermissionNavigation)
                .WithMany(p => p.RoleHasPermissions)
                .HasForeignKey(d => d.IdPermission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_9");

            builder.HasOne(d => d.IdRolNavigation)
                .WithMany(p => p.RoleHasPermissions)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_10");
        }
    }
}

