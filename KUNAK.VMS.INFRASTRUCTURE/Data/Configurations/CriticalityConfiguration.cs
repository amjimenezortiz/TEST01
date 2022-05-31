using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class CriticalityConfiguration : IEntityTypeConfiguration<Criticality>
    {
        public void Configure(EntityTypeBuilder<Criticality> builder)
        {
            builder.HasKey(e => e.IdCriticality)
                    .HasName("PK__critical__3003C0C903420423");

            builder.ToTable("criticality");

            builder.Property(e => e.IdCriticality).HasColumnName("idCriticality");

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

