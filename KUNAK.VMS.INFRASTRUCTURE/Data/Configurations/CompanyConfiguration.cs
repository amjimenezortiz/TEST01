using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(e => e.IdCompany)
                    .HasName("PK__companie__BBAEF003CD6C0DDF");

            builder.ToTable("companies");

            builder.Property(e => e.IdCompany).HasColumnName("idCompany");

            builder.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");

            builder.Property(e => e.District)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("district");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");

            builder.Property(e => e.Logo)
                .IsUnicode(false)
                .HasColumnName("logo");

            builder.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");

            builder.Property(e => e.Province)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("province");

            builder.Property(e => e.Region)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("region");

            builder.Property(e => e.Ruc)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RUC");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.TradeName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tradeName");
        }
    }
}

