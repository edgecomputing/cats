using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class TransReqWithoutTransporterMap : EntityTypeConfiguration<TransReqWithoutTransporter>
    {
       public TransReqWithoutTransporterMap()
       {
           // Primary Key
           this.HasKey(t => t.TransReqWithoutTransporterID);

           // Table & Column Mappings
           this.ToTable("TransReqWithoutTransporter", "dbo");
          

           // Relationships
           this.Property(t => t.TransReqWithoutTransporterID).HasColumnName("TransReqWithoutTransporterID");
           this.Property(t => t.TransportRequisitionID).HasColumnName("TransportRequisitionID");
           this.Property(t => t.IsAssigned).HasColumnName("IsAssigned");

           // Relationships
           this.HasRequired(t => t.TransportRequisition)
               .WithMany(t => t.TransReqWithoutTransporters)
               .HasForeignKey(d => d.TransportRequisitionID);

          
       }
    }
}
