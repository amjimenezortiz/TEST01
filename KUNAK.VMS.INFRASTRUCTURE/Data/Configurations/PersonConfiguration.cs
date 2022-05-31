using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.IdPerson)
                    .HasName("PK__people__BAB33700BA910682");

            builder.ToTable("people");

            builder.Property(e => e.IdPerson).HasColumnName("idPerson");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");

            builder.Property(e => e.EmployeeCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("employeeCode");

            builder.Property(e => e.IdArea).HasColumnName("idArea");

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lastName");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");

            builder.HasOne(d => d.IdAreaNavigation)
                .WithMany(p => p.People)
                .HasForeignKey(d => d.IdArea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_35");
        }
    }
}

