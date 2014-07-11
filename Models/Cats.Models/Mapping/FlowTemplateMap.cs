using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class FlowTemplateMap : EntityTypeConfiguration<FlowTemplate>
    {
        public FlowTemplateMap()
        {

            //this.ToTable("Workflow.FlowTemplate");
            this.ToTable("FlowTemplate");
            this.HasKey(t => t.FlowTemplateID);
            
            this.Property(t => t.ParentProcessTemplateID).HasColumnName("ParentProcessTemplateID");

            this.Property(t => t.InitialStateID).HasColumnName("InitialStateID");

            this.Property(t => t.FinalStateID).HasColumnName("FinalStateID");

            this.Property(t => t.Name).HasColumnName("Name");

            this.HasRequired(t => t.InitialState)
                   .WithMany(t => t.InitialStateFlowTemplates)
                   .HasForeignKey(d => d.InitialStateID);

            this.HasRequired(t => t.FinalState)
                   .WithMany(t => t.FinalStateFlowTemplates)
                   .HasForeignKey(d => d.FinalStateID);
            this.HasRequired(t => t.ParentProcessTemplate)
                              .WithMany(t => t.ParentProcessTemplateFlowTemplates)
                              .HasForeignKey(d => d.ParentProcessTemplateID);
        }
    }
}