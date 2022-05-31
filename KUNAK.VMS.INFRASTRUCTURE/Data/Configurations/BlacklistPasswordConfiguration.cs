using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class BlacklistPasswordConfiguration : IEntityTypeConfiguration<BlacklistPassword>
    {
        public void Configure(EntityTypeBuilder<BlacklistPassword> builder)
        {
            builder.HasKey(e => e.IdPassword)
                    .HasName("PK__blacklis__AB73F1F0D9BCFC66");

            builder.ToTable("blacklist_password");

            builder.Property(e => e.IdPassword).HasColumnName("idPassword");

            builder.Property(e => e.Description)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("description");
        }
    }
}

