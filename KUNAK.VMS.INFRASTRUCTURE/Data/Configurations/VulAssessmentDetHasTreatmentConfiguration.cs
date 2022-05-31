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
    public class VulAssessmentDetHasTreatmentConfiguration : IEntityTypeConfiguration<VulAssessmentDetHasTreatment>
    {
        public void Configure(EntityTypeBuilder<VulAssessmentDetHasTreatment> builder)
        {
            builder.HasKey(e => new { e.IdVulnerabilityAssessmentDetail, e.IdTreatment })
                    .HasName("PK__vul_asse__D9A14E0AAFF50D03");

            builder.ToTable("vul_assessment_det_has_treatment");

            builder.Property(e => e.IdVulnerabilityAssessmentDetail).HasColumnName("idVulnerabilityAssessmentDetail");

            builder.Property(e => e.IdTreatment).HasColumnName("idTreatment");

            builder.Property(e => e.Description)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("description");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdTreatmentNavigation)
                .WithMany(p => p.VulAssessmentDetHasTreatments)
                .HasForeignKey(d => d.IdTreatment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_65");

            builder.HasOne(d => d.IdVulnerabilityAssessmentDetailNavigation)
                .WithMany(p => p.VulAssessmentDetHasTreatments)
                .HasForeignKey(d => d.IdVulnerabilityAssessmentDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_64");
        }
    }
}
