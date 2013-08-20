using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ProcessTemplateMap : EntityTypeConfiguration<ProcessTemplate>
    {
        public ProcessTemplateMap()
        {

            //this.ToTable("Workflow.ProcessTemplate");
            this.ToTable("ProcessTemplate");
            this.HasKey(t => t.ProcessTemplateID);

            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

        }
    }
}