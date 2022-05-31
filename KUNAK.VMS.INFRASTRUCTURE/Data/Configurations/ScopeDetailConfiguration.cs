using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class ScopeDetailConfiguration : IEntityTypeConfiguration<ScopeDetail>
    {
        public void Configure(EntityTypeBuilder<ScopeDetail> builder)
        {
            builder.HasKey(e => e.IdScopeDetail)
                    .HasName("PK__scope_de__B5D1F6FB54EF93C7");

            builder.ToTable("scope_detail");

            builder.Property(e => e.IdScopeDetail).HasColumnName("idScopeDetail");

            builder.Property(e => e.IdScope).HasColumnName("idScope");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.ScopeDetailHtml)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("scopeDetailHtml");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdScopeNavigation)
                .WithMany(p => p.ScopeDetails)
                .HasForeignKey(d => d.IdScope)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_51");
        }
    }
}
