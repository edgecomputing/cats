using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Hubs.Mapping
{
    public class VWDispatchCommodityMap:EntityTypeConfiguration<VWDispatchCommodity>
    {
        public VWDispatchCommodityMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DispatchedAmountInMT, t.DispatchedAmountInUnit, t.LedgerID, t.Amount, t.FDPID, t.Hub, t.ProjectCode, t.ShippingInstruction, t.IsClosed, t.Donor, t.CommodityID, t.Commodity, t.RequisitionNo, t.Unit, t.Name, t.DispatchDate, t.CreatedDate, t.DispatchedByStoreMan, t.GIN, t.RegionId, t.ZoneId });

            // Properties
            this.Property(t => t.DispatchedAmountInMT);


            this.Property(t => t.DispatchedAmountInUnit);


            this.Property(t => t.LedgerID);


            this.Property(t => t.Amount);


            this.Property(t => t.FDPID);
               

            this.Property(t => t.Hub)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProjectCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ShippingInstruction)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FDPName)
                .HasMaxLength(50);

            this.Property(t => t.Donor)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CommodityID);
               

            this.Property(t => t.Commodity)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RequisitionNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BidRefNo)
                .HasMaxLength(50);

            this.Property(t => t.Unit);
              

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DispatchedByStoreMan)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.GIN)
                .IsRequired()
                .HasMaxLength(7);

            this.Property(t => t.Zone)
                .HasMaxLength(50);

            this.Property(t => t.Region)
                .HasMaxLength(50);

            this.Property(t => t.RegionId);


            this.Property(t => t.ZoneId);
              

            // Table & Column Mappings
            this.ToTable("vw_dispatchReport");
            this.Property(t => t.DispatchedAmountInMT).HasColumnName("DispatchedAmountInMT");
            this.Property(t => t.DispatchedAmountInUnit).HasColumnName("DispatchedAmountInUnit");
            this.Property(t => t.LedgerID).HasColumnName("LedgerID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.RemainingInMT).HasColumnName("RemainingInMT");
            this.Property(t => t.RemainingInUnit).HasColumnName("RemainingInUnit");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.Hub).HasColumnName("Hub");
            this.Property(t => t.ProjectCode).HasColumnName("ProjectCode");
            this.Property(t => t.ShippingInstruction).HasColumnName("ShippingInstruction");
            this.Property(t => t.FDPName).HasColumnName("FDPName");
            this.Property(t => t.AdminUnitTypeID).HasColumnName("AdminUnitTypeID");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.IsClosed).HasColumnName("IsClosed");
            this.Property(t => t.Donor).HasColumnName("Donor");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.Commodity).HasColumnName("Commodity");
            this.Property(t => t.RequisitionNo).HasColumnName("RequisitionNo");
            this.Property(t => t.BidRefNo).HasColumnName("BidRefNo");
            this.Property(t => t.ContractStartDate).HasColumnName("ContractStartDate");
            this.Property(t => t.ContractEndDate).HasColumnName("ContractEndDate");
            this.Property(t => t.Beneficiery).HasColumnName("Beneficiery");
            this.Property(t => t.Unit).HasColumnName("Unit");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Round).HasColumnName("Round");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.ShippingInstructionID).HasColumnName("ShippingInstructionID");
            this.Property(t => t.ProjectCodeID).HasColumnName("ProjectCodeID");
            this.Property(t => t.DispatchDate).HasColumnName("DispatchDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.DispatchedByStoreMan).HasColumnName("DispatchedByStoreMan");
            this.Property(t => t.GIN).HasColumnName("GIN");
            this.Property(t => t.DispatchID).HasColumnName("DispatchID");
            this.Property(t => t.Zone).HasColumnName("Zone");
            this.Property(t => t.Region).HasColumnName("Region");
            this.Property(t => t.RegionId).HasColumnName("RegionId");
            this.Property(t => t.ZoneId).HasColumnName("ZoneId");
        }
    }
}
