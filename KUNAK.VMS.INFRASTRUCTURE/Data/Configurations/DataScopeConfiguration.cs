using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class DataScopeConfiguration : IEntityTypeConfiguration<DataScope>
    {
        public void Configure(EntityTypeBuilder<DataScope> builder)
        {
            builder.HasKey(e => e.IdDataScope)
                    .HasName("PK__data_sco__D3E5FA71F30DC4DA");

            builder.ToTable("data_scope");

            builder.Property(e => e.IdDataScope).HasColumnName("idDataScope");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        }
    }
}
