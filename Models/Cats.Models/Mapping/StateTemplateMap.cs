using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class StateTemplateMap : EntityTypeConfiguration<StateTemplate>
    {
        public StateTemplateMap()
        {

            //this.ToTable("Workflow.StateTemplate");
            this.ToTable("StateTemplate");
            this.HasKey(t => t.StateTemplateID);

            this.Property(t => t.ParentProcessTemplateID).HasColumnName("ParentProcessTemplateID");

            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.AllowedAccessLevel).HasColumnName("AllowedAccessLevel");
            this.Property(t => t.StateType).HasColumnName("StateType");
            this.Property(t => t.StateNo).HasColumnName("StateNo");


            this.HasRequired(t => t.ParentProcessTemplate)
                   .WithMany(t => t.ParentProcessTemplateStateTemplates)
                   .HasForeignKey(d => d.ParentProcessTemplateID);

        }
    }
}