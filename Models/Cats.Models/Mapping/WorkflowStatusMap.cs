using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class WorkflowStatusMap : EntityTypeConfiguration<WorkflowStatus>
    {
        public WorkflowStatusMap()
        {
            // Primary Key
            this.HasKey(t => new { t.StatusID, t.WorkflowID });

            // Properties
            this.Property(t => t.StatusID);

            this.Property(t => t.WorkflowID);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WorkflowStatus");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.WorkflowID).HasColumnName("WorkflowID");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasRequired(t => t.Workflow)
                .WithMany(t => t.WorkflowStatuses)
                .HasForeignKey(d => d.WorkflowID);

        }
    }
}
