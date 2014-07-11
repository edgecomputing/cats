using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class WorkflowMap : EntityTypeConfiguration<Workflow>
    {
        public WorkflowMap()
        {
            // Primary Key
            this.HasKey(t => t.WorkflowID);

            // Properties
            this.Property(t => t.WorkflowID);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Workflow");
            this.Property(t => t.WorkflowID).HasColumnName("WorkflowID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
