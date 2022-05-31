using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class UsersHasPermissionConfiguration : IEntityTypeConfiguration<UsersHasPermission>
    {
        public void Configure(EntityTypeBuilder<UsersHasPermission> builder)
        {
            builder.HasKey(e => e.IdUserPermission)
                    .HasName("PK__users_ha__92DE1DB3904BAC24");

            builder.ToTable("users_has_permissions");

            builder.Property(e => e.IdUserPermission).HasColumnName("idUserPermission");

            builder.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            builder.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");

            builder.Property(e => e.Enddate)
                .HasColumnType("datetime")
                .HasColumnName("enddate");

            builder.Property(e => e.IdPermission).HasColumnName("idPermission");

            builder.Property(e => e.IdUser).HasColumnName("idUser");

            builder.Property(e => e.Stardate)
                .HasColumnType("datetime")
                .HasColumnName("stardate");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            builder.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");

            builder.HasOne(d => d.IdPermissionNavigation)
                .WithMany(p => p.UsersHasPermissions)
                .HasForeignKey(d => d.IdPermission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_11");

            builder.HasOne(d => d.IdUserNavigation)
                .WithMany(p => p.UsersHasPermissions)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_12");
        }
    }
}

