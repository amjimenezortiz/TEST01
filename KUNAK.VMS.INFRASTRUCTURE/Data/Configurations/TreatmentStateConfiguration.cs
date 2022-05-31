using KUNAK.VMS.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Data.Configurations
{
    public class TreatmentStateConfiguration : IEntityTypeConfiguration<TreatmentState>
    {
        public void Configure(EntityTypeBuilder<TreatmentState> builder)
        {
            builder.HasKey(e => e.IdTreatmentStates)
                    .HasName("PK__treatmen__BF9104EF75C8AEB7");

            builder.ToTable("treatment_states");

            builder.Property(e => e.IdTreatmentStates).HasColumnName("idTreatmentStates");

            builder.Property(e => e.Color)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("color");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.Order).HasColumnName("order");
        }
    }
}
