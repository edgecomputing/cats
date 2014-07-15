using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// 
    public class OtherDispatchAllocationDto
    {
        public Guid? OtherDispatchAllocationID { get; set; }
        public string ReferenceNumber { get; set; }
        public String CommodityName { get; set; }
        public Decimal QuantityInMT { get; set; }
        public Decimal QuantityInUnit { get; set; }
        public Decimal RemainingQuantityInMt { get; set; }
        public Decimal RemainingQuantityInUnit { get; set; }
        public DateTime EstimatedDispatchDate { get; set; }
        public DateTime AgreementDate { get; set; }
        public string SINumber { get; set; }
        public string ProjectCode { get; set; }
        public bool IsClosed { get; set; }

    }

    public class DispatchAllocationViewModelDto
    {

        public Guid? DispatchAllocationID { get; set; }

        public String CommodityName { get; set; }

        public String RequisitionNo { get; set; }

        public String BidRefNo { get; set; }

        public Decimal Amount { get; set; }

        public Decimal AmountInUnit { get; set; }

        public String TransporterName { get; set; }

        public String FDPName { get; set; }

        public bool IsClosed { get; set; }

        public decimal DispatchedAmount { get; set; }

        public Decimal DispatchedAmountInUnit { get; set; }

        public decimal RemainingQuantityInQuintals { get; set; }

        public Decimal RemainingQuantityInUnit { get; set; }

        public string Region { get; set; }

        public string Zone { get; set; }

        public string Woreda { get; set; }

        public Int32? ProjectCodeID {get;set;}
        public Int32? ShippingInstructionID{ get; set; }
        public Int32 CommodityID { get; set; }
    }

    public class DispatchAllocationViewModel
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchAllocationViewModel"/> class.
        /// </summary>
        public DispatchAllocationViewModel()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchAllocationViewModel"/> class.
        /// </summary>
        /// <param name="fdpID">The FDP ID.</param>
        /// <param name="repository">The repository.</param>
        //TODO:Make sure FDP is loaded with all include properties
        public DispatchAllocationViewModel(FDP fdp)
        {
            FDPID = fdp.FDPID;
            // Initalize the parents of the FDP.
            FDPName = fdp.Name;
            Woredas = fdp.AdminUnit.AdminUnit2.AdminUnit1.OrderBy(o=>o.Name).ToList();
            WoredaID = fdp.AdminUnit.AdminUnitID;

            this.FDPs = fdp.AdminUnit.FDPs.OrderBy(o => o.Name).ToList();

            Zones = fdp.AdminUnit.AdminUnit2.AdminUnit2.AdminUnit1.OrderBy(o => o.Name).ToList();
            ZoneID = fdp.AdminUnit.AdminUnit2.AdminUnitID;

            Regions = fdp.AdminUnit.AdminUnit2.AdminUnit2.AdminUnit2.AdminUnit1.OrderBy(o => o.Name).ToList();
            RegionID = fdp.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID;
        }

        /// <summary>
        /// Gets or sets the regions.
        /// </summary>
        /// <value>
        /// The regions.
        /// </value>
        public List<AdminUnit> Regions { get; set; }
        /// <summary>
        /// Gets or sets the zones.
        /// </summary>
        /// <value>
        /// The zones.
        /// </value>
        public List<AdminUnit> Zones { get; set; }
        /// <summary>
        /// Gets or sets the woredas.
        /// </summary>
        /// <value>
        /// The woredas.
        /// </value>
        public List<AdminUnit> Woredas { get; set; }
        /// <summary>
        /// Gets or sets the FD ps.
        /// </summary>
        /// <value>
        /// The FD ps.
        /// </value>
        public List<FDP> FDPs { get; set; }

        /// <summary>
        /// Gets or sets the partition ID.
        /// </summary>
        /// <value>
        /// The partition ID.
        /// </value>
        //[Required(ErrorMessage = "Partition is required")]
        public Int32 PartitionId { get; set; }

        /// <summary>
        /// Gets or sets the dispatch allocation ID.
        /// </summary>
        /// <value>
        /// The dispatch allocation ID.
        /// </value>
        //[Required(ErrorMessage = "Dispatch Allocation is required")]
        public Guid? DispatchAllocationID { get; set; }

        /// <summary>
        /// Gets or sets the hub ID.
        /// </summary>
        /// <value>
        /// The hub ID.
        /// </value>
        [Required(ErrorMessage = "Hub is required")]
        public Int32 HubID { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public Int32? Year { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public Int32? Month { get; set; }

        /// <summary>
        /// Gets or sets the round.
        /// </summary>
        /// <value>
        /// The round.
        /// </value>
        public Int32? Round { get; set; }

        /// <summary>
        /// Gets or sets the donor ID.
        /// </summary>
        /// <value>
        /// The donor ID.
        /// </value>
        public Int32? DonorID { get; set; }

        /// <summary>
        /// Gets or sets the program ID.
        /// </summary>
        /// <value>
        /// The program ID.
        /// </value>
        [Required]
        public Int32? ProgramID { get; set; }

        [Required(ErrorMessage = "Commodity is required")]
        public Int32 CommodityID { get; set; }

        /// <summary>
        /// Gets or sets the requisition no.
        /// </summary>
        /// <value>
        /// The requisition no.
        /// </value>
        [Required(ErrorMessage = "Requisition No is required")]
        [StringLength(50)]
        public String RequisitionNo { get; set; }

        /// <summary>
        /// Gets or sets the bid ref no.
        /// </summary>
        /// <value>
        /// The bid ref no.
        /// </value>
        [StringLength(50)]
        [Required(ErrorMessage = "BidRefNo No is required")]
        public String BidRefNo { get; set; }

        /// <summary>
        /// Gets or sets the beneficiery.
        /// </summary>
        /// <value>
        /// The beneficiery.
        /// </value>
        public Int32? Beneficiery { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [Required(ErrorMessage = "Amount is required")]
        [UIHint("PreferedWeightMeasurmentQi")]
        public Decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        [Required(ErrorMessage = "Unit is required")]
        public int Unit { get; set; }

        /// <summary>
        /// Gets or sets the transporter ID.
        /// </summary>
        /// <value>
        /// The transporter ID.
        /// </value>
        public Int32? TransporterID { get; set; }

        /// <summary>
        /// Gets or sets the FDPID.
        /// </summary>
        /// <value>
        /// The FDPID.
        /// </value>
        [Required(ErrorMessage = "FDP is required")]
        public Int32 FDPID { get; set; }

        /// <summary>
        /// Gets or sets the FDPName.
        /// </summary>
        /// <value>
        /// The FDPName.
        /// </value>

        public string FDPName { get; set; }

        /// <summary>
        /// Gets or sets the project code ID.
        /// </summary>
        /// <value>
        /// The project code ID.
        /// </value>
        public Int32? ProjectCodeID { get; set; }

        /// <summary>
        /// Gets or sets the shipping instruction ID.
        /// </summary>
        /// <value>
        /// The shipping instruction ID.
        /// </value>
        public Int32? ShippingInstructionID { get; set; }

        /// <summary>
        /// Gets or sets the region ID.
        /// </summary>
        /// <value>
        /// The region ID.
        /// </value>
        public Int32? RegionID { get; set; }
        
        /// <summary>
        /// Gets or sets the zone ID.
        /// </summary>
        /// <value>
        /// The zone ID.
        /// </value>
        public Int32? ZoneID { get; set; }
       
        /// <summary>
        /// Gets or sets the woreda ID.
        /// </summary>
        /// <value>
        /// The woreda ID.
        /// </value>
        public Int32? WoredaID { get; set; }

        [Required]
        public int CommodityTypeID { get; set; }
    }
}
