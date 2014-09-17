using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class VWRegionalRequestMap : EntityTypeConfiguration<VWRegionalRequest>
    {
        public VWRegionalRequestMap()
        {
            this.HasKey(t => new {t.RegionalRequestDetailID, t.RegionalRequestID,t.CommodityID});

            // Table & Column Mappings
            this.ToTable("RegionalRequestAllocation");
            this.Property(t => t.RegionalRequestID).HasColumnName("RegionalRequestID");
            this.Property(t => t.RegionalRequestDetailID).HasColumnName("RegionalRequestDetailID");
            this.Property(t => t.RequestDate).HasColumnName("RequestDate");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.Program).HasColumnName("Program");
            this.Property(t => t.RationID).HasColumnName("RationID");
            this.Property(t => t.RationName).HasColumnName("RationName");
            this.Property(t => t.Month).HasColumnName("Month");
           // this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.RegionName).HasColumnName("RegionName");
            this.Property(t => t.RequestNumber).HasColumnName("RequestNumber");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.FDPName).HasColumnName("FDPName");
            this.Property(t => t.Woreda).HasColumnName("Woreda");
            this.Property(t => t.ZoneName).HasColumnName("ZoneName");
            this.Property(t => t.Beneficiaries).HasColumnName("Beneficiaries");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Commodity).HasColumnName("Commodity");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.AllocatedAmount).HasColumnName("AllocatedAmount");
           
        }
    }
}
