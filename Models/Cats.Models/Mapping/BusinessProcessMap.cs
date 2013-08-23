using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class BusinessProcessMap : EntityTypeConfiguration<BusinessProcess>
    {
        public BusinessProcessMap()
        {

            //this.ToTable("Cats.BusinessProcess");
            this.ToTable("BusinessProcess");
            this.HasKey(t => t.BusinessProcessID);

            this.Property(t => t.ProcessTypeID).HasColumnName("ProcessTypeID");

            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.DocumentType).HasColumnName("DocumentType");
            this.Property(t => t.CurrentStateID).HasColumnName("CurrentStateID");


            this.HasRequired(t => t.ProcessType)
                   .WithMany(t => t.ProcessTypeBusinessProcesss)
                   .HasForeignKey(d => d.ProcessTypeID);

            this.HasRequired(t => t.CurrentState)
                   .WithMany(t => t.CurrentStateBusinessProcesss)
                   .HasForeignKey(d => d.CurrentStateID);

        }
    }
}