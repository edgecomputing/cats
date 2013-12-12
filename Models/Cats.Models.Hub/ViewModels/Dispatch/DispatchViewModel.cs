using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Cats.Models.Hubs;

namespace Cats.Models.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class DispatchModelModelDto
    {
        public Guid? DispatchID { get; set; }
        public string GIN { get; set; }
        [UIHint("DateTime")]
        [DataType(DataType.DateTime)]
        public DateTime DispatchDate { get; set; }
        public string DispatchedByStoreMan { get; set; }
    }


    public class DispatchModel
    {

        #region Lists for the view model
        /// <summary>
        /// 
        /// </summary>
        /// //TODO:code smell separation of concern
        //   private IUnitOfWork Repository = new UnitOfWork();

        private List<Transporter> _transporters;

        /// <summary>
        /// Gets or sets the transporters.
        /// </summary>
        /// <value>
        /// The transporters.
        /// </value>
        public List<Transporter> Transporters
        {
            get
            {
                //TODO:Refactor
                //if(_transporters == null)
                //{
                //    _transporters = Repository.TransporterRepository.GetAll().OrderBy(o => o.Name).ToList();
                //}
                return _transporters.OrderBy(o => o.Name).ToList();
            }
            set { _transporters = value; }
        }

        private List<Commodity> _commodities;

        /// <summary>
        /// Gets or sets the commodities.
        /// </summary>
        /// <value>
        /// The commodities.
        /// </value>
        public List<Commodity> Commodities
        {
            get
            {
                //TODO:Make sure this property is loaded with commodity
                //if (_commodities == null)
                //{
                //    _commodities = Repository.Commodity.GetAllParents().OrderBy(o => o.Name).ToList();
                //}
                return _commodities.OrderBy(o => o.Name).ToList(); ;
            }
            set { _commodities = value; }
        }

        private List<Unit> _units;

        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        public List<Unit> Units
        {
            get
            {
                //TODO:Make sure this property is loaded with full of units
                //if (_units == null)
                //{
                //    _units = Repository.Unit.GetAll().OrderBy(o => o.Name).ToList();
                //}
                return _units.OrderBy(o => o.Name).ToList();
            }
            set { _units = value; }
        }

        private List<FDP> _FDPs;

        /// <summary>
        /// Gets or sets the FD ps.
        /// </summary>
        /// <value>
        /// The FD ps.
        /// </value>
        public List<FDP> FDPs
        {
            get
            {
                //TODO:make sure this property is loaded with full of FDPs
                //if(_FDPs == null)
                //{
                //    _FDPs = Repository.FDP.GetAll();
                //}
                return _FDPs.ToList();
            }
            set { _FDPs = value; }
        }

        private List<Program> _programs;

        /// <summary>
        /// Gets or sets the programs.
        /// </summary>
        /// <value>
        /// The programs.
        /// </value>
        public List<Program> Programs
        {
            get
            {
                //TODO:make sure programs to be loaded 
                //if(_programs == null)
                //{
                //    _programs = Repository.Program.GetAll().OrderBy(o => o.Name).ToList();
                //}
                return _programs.OrderBy(o => o.Name).ToList();
            }
            set { _programs = value; }
        }

        private List<AdminUnit> _Regions;

        /// <summary>
        /// Gets or sets the regions.
        /// </summary>
        /// <value>
        /// The regions.
        /// </value>
        public List<AdminUnit> Regions
        {
            get
            {
                //TODO:make sure regions are loaded
                //if(_Regions == null)
                //{
                //    _Regions = Repository.AdminUnit.GetRegions();
                //}
                return _Regions.ToList();
            }
            set { _Regions = value; }
        }

        private List<AdminUnit> _Zones;

        /// <summary>
        /// Gets or sets the zones.
        /// </summary>
        /// <value>
        /// The zones.
        /// </value>
        public List<AdminUnit> Zones
        {
            get
            {
                //TODO:Make sure zones are loaded
                //if(RegionID != null)
                //{
                //    _Zones = Repository.AdminUnit.GetChildren(RegionID.Value);
                //}else
                //{
                //    _Zones = new List<AdminUnit>();
                //}

                return _Zones;
            }
            set { _Zones = value; }
        }

        private List<Store> _stores;

        /// <summary>
        /// Gets or sets the stores.
        /// </summary>
        /// <value>
        /// The stores.
        /// </value>
        public List<Store> Stores
        {
            get
            {
                //TODO:make sure stores are loaded
                //if(_stores == null)
                //{
                //    _stores = Repository.Store.GetAll();
                //}
                return _stores;
            }
            set { _stores = value; }
        }

        #endregion


        /// <summary>
        /// Gets or sets the dispatch ID.
        /// </summary>
        /// <value>
        /// The dispatch ID.
        /// </value>
        public Guid? DispatchID { get; set; }

        /// <summary>
        /// Gets or sets the FDPID.
        /// </summary>
        /// <value>
        /// The FDPID.
        /// </value>
        [Required(ErrorMessage = "FDP is required")]
        [Display(Name = "FDP")]
        public int? FDPID { get; set; }

        /// <summary>
        /// Gets or sets the region ID.
        /// </summary>
        /// <value>
        /// The region ID.
        /// </value>
        [Required(ErrorMessage = "Region is required")]
        public int? RegionID { get; set; }
        /// <summary>
        /// Gets or sets the zone ID.
        /// </summary>
        /// <value>
        /// The zone ID.
        /// </value>
        [Required(ErrorMessage = "Zone is required")]
        public int? ZoneID { get; set; }
        /// <summary>
        /// Gets or sets the woreda ID.
        /// </summary>
        /// <value>
        /// The woreda ID.
        /// </value>
        [Required(ErrorMessage = "Woreda is required")]
        public int? WoredaID { get; set; }

        /// <summary>
        /// Gets or sets to hub ID.
        /// </summary>
        /// <value>
        /// To hub ID.
        /// </value>
        [Required]
        [Display(Name = "HUB")]
        public int? ToHubID { get; set; }

        /// <summary>
        /// Gets or sets the dispatched by store man.
        /// </summary>
        /// <value>
        /// The dispatched by store man.
        /// </value>
        [Required]
        [Display(Name = "Store Man")]
        [UIHint("AmharicTextBox")]
        public string DispatchedByStoreMan { get; set; }

        /// <summary>
        /// Gets or sets the warehouse ID.
        /// </summary>
        /// <value>
        /// The warehouse ID.
        /// </value>
        [Required]
        public int WarehouseID { get; set; }
        /// <summary>
        /// Gets or sets the store ID.
        /// </summary>
        /// <value>
        /// The store ID.
        /// </value>
        [Required]
        [Display(Name = "Store")]
        public int StoreID { get; set; }
        /// <summary>
        /// Gets or sets the stack number.
        /// </summary>
        /// <value>
        /// The stack number.
        /// </value>
        [Required]
        [Display(Name = "Stack Number")]
        public int StackNumber { get; set; }
        /// <summary>
        /// Gets or sets the dispatch date.
        /// </summary>
        /// <value>
        /// The dispatch date.
        /// </value>
        [Required]
        [Display(Name = "Dispatch Date")]
        public DateTime DispatchDate { get; set; }
        /// <summary>
        /// Gets or sets the requisition no.
        /// </summary>
        /// <value>
        /// The requisition no.
        /// </value>
        [Required]
        [Display(Name = "Requisition Number")]
        public string RequisitionNo { get; set; }
        /// <summary>
        /// Gets or sets the GIN.
        /// </summary>
        /// <value>
        /// The GIN.
        /// </value>
        [Required]
        [Display(Name = "GIN")]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numbers allowed for GIN")]
        [Remote("NotUnique", "Dispatch", AdditionalFields = "DispatchID")]
        [StringLength(7, ErrorMessage = "Length Must be less than or equal to 7")]
        public string GIN { get; set; }
        /// <summary>
        /// Gets or sets the bid number.
        /// </summary>
        /// <value>
        /// The bid number.
        /// </value>
        [Required(ErrorMessage = "Bid Number is a required field")]
        [Display(Name = "Bid Number")]
        public string BidNumber { get; set; }


        /// <summary>
        /// Gets or sets the name of the driver.
        /// </summary>
        /// <value>
        /// The name of the driver.
        /// </value>
        //[RegularExpression("[a-zA-Z\\s]*", ErrorMessage = "Only letters are allowed for Driver name")]
        [Required(ErrorMessage = "Name of the driver taking the commodities is a required field.")]
        [Display(Name = "Received By (Driver Name)")]
        [UIHint("AmharicTextBox")]
        public string DriverName { get; set; }

        /// <summary>
        /// Gets or sets the plate no_ prime.
        /// </summary>
        /// <value>
        /// The plate no_ prime.
        /// </value>
        [Required(ErrorMessage = "Prime Plate Number is a required field")]
        [Display(Name = "Plate No. Prime")]
        public string PlateNo_Prime { get; set; }

        /// <summary>
        /// Gets or sets the plate no_ trailer.
        /// </summary>
        /// <value>
        /// The plate no_ trailer.
        /// </value>
        [Display(Name = "Plate No. Trailer")]
        public string PlateNo_Trailer { get; set; }
        /// <summary>
        /// Gets or sets the transporter ID.
        /// </summary>
        /// <value>
        /// The transporter ID.
        /// </value>
        [Required(ErrorMessage = "Transporter is a required field")]
        [Display(Name = "Transporter")]
        public int TransporterID { get; set; }

        /// <summary>
        /// Gets or sets the project number.
        /// </summary>
        /// <value>
        /// The project number.
        /// </value>
        [Required(ErrorMessage = "Project Code is a required field")]
        [Display(Name = "Project Code")]
        [Remote("IsProjectValid", "Dispatch", ErrorMessage = "Project Number does not exist")]
        public string ProjectNumber { get; set; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        [Required]
        [Display(Name = "Month")]
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the program ID.
        /// </summary>
        /// <value>
        /// The program ID.
        /// </value>
        [Required(ErrorMessage = "Project is a required field")]
        [Display(Name = "Project")]
        public int ProgramID { get; set; }

        /// <summary>
        /// Gets or sets the weigh bridge ticket number.
        /// </summary>
        /// <value>
        /// The weigh bridge ticket number.
        /// </value>
        [Display(Name = "Weigh Bridge Ticket Number")]
        public string WeighBridgeTicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the round.
        /// </summary>
        /// <value>
        /// The round.
        /// </value>
        [Display(Name = "Round")]
        public int Round { get; set; }

        /// <summary>
        /// Gets or sets the SI number.
        /// </summary>
        /// <value>
        /// The SI number.
        /// </value>
        [Required(ErrorMessage = "SI or an equivalent is required.")]
        [Display(Name = "SI/Batch Number")]
        [Remote("IsSIValid", "Dispatch", ErrorMessage = @"SI/Batch Number does not exist")]
        public string SINumber { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        [Display(Name = "Remark")]
        [UIHint("AmharicTextArea")]
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the JSON inserted commodities.
        /// </summary>
        /// <value>
        /// The JSON inserted commodities.
        /// </value>
        public string JSONInsertedCommodities { get; set; }

        /// <summary>
        /// Gets or sets the JSON updated commodities.
        /// </summary>
        /// <value>
        /// The JSON updated commodities.
        /// </value>
        public string JSONUpdatedCommodities { get; set; }

        /// <summary>
        /// Gets or sets the JSON deleted commodities.
        /// </summary>
        /// <value>
        /// The JSON deleted commodities.
        /// </value>
        public string JSONDeletedCommodities { get; set; }

        public string JSONPrev { get; set; }

        public List<CommodityType> CommodityTypes { get; set; }

        [Required]
        public int CommodityTypeID { get; set; }

        //[Required]
        //[Range(1, 9999, ErrorMessage = "Please insert at least one commodity")]
        /// <summary>
        /// Gets or sets the row count.
        /// </summary>
        /// <value>
        /// The row count.
        /// </value>
        public int rowCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchModel"/> class.
        /// </summary>
        public DispatchModel(List<Commodity> commodities,
            List<Transporter> transporters,
            List<Unit> units,
            List<FDP> fdps,
            List<Program> programs,
            List<AdminUnit> regions,
            List<AdminUnit> zones,
            List<Store> stores)
        {
            this.DispatchID = null;
            this.DispatchDate = DateTime.Now;
            this._commodities = commodities;
            this._transporters = transporters;
            this._units = units;
            this._FDPs = fdps;
            this._programs = programs;
            this.Regions = regions;
            this.Zones = zones;
            this._stores = stores;



        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns></returns>
        internal bool Validate()
        {
            return true;
        }

        /// <summary>
        /// Generates the dipatch.
        /// </summary>
        /// <returns></returns>
        public Dispatch GenerateDipatch(UserProfile user)
        {
            //TODO:Check if any impact converting default constructor to injuct user
            //UserProfile user = Repository.UserProfile.GetUser(HttpContext.Current.User.Identity.Name);
            Dispatch dispatch = new Dispatch()
            {
                BidNumber = this.BidNumber,
                CreatedDate = DateTime.Now,
                DispatchDate = this.DispatchDate,

                DriverName = this.DriverName,
                DispatchedByStoreMan = DispatchedByStoreMan,
                FDPID = this.FDPID,
                GIN = this.GIN,
                PeriodYear = this.Year,
                PeriodMonth = this.Month,
                PlateNo_Prime = this.PlateNo_Prime,
                PlateNo_Trailer = this.PlateNo_Trailer,
                RequisitionNo = this.RequisitionNo,
                //HubID = user.DefaultHub.HubID;
                // StackNumber = this.StackNumber,
                // StoreID = this.StoreID,
                TransporterID = this.TransporterID,
                HubID = this.WarehouseID,
                //ProgramID = this.ProgramID,
                WeighBridgeTicketNumber = this.WeighBridgeTicketNumber,
                Round = this.Round,
                Remark = this.Remark,
                //ProjectNumber = this.ProjectNumber,
                // SINumber = this.SINumber 
            };
            if (this.DispatchID.HasValue)
            {
                dispatch.DispatchID = this.DispatchID.Value;
            }
            return dispatch;
        }

        /// <summary>
        /// Generates the dispatch model.
        /// </summary>
        /// <param name="dispatch">The dispatch.</param>
        /// <param name="Repository">The repository.</param>
        /// <returns></returns>
        public static DispatchModel GenerateDispatchModel(Dispatch dispatch, Transaction transactionObj,
            List<Commodity> commodities,
            List<Transporter> transporters,
            List<Unit> units,
            List<FDP> fdps,
            List<Program> programs,
            List<AdminUnit> regions,
            List<AdminUnit> zones,
            List<Store> stores)
        {
            DispatchModel model = new DispatchModel(commodities,
            transporters,
            units,
            fdps,
            programs,
           regions,
            zones,
          stores);
            model.BidNumber = dispatch.BidNumber;
            model.DispatchDate = dispatch.DispatchDate;
            model.DispatchID = dispatch.DispatchID;
            model.DriverName = dispatch.DriverName;
            model.DispatchedByStoreMan = dispatch.DispatchedByStoreMan;
            model.FDPID = dispatch.FDPID;
            model.GIN = dispatch.GIN;
            model.Month = dispatch.PeriodMonth;
            model.Year = dispatch.PeriodYear;
            model.PlateNo_Prime = dispatch.PlateNo_Prime;
            model.PlateNo_Trailer = dispatch.PlateNo_Trailer;
            model.RequisitionNo = dispatch.RequisitionNo;
            model.Type = dispatch.Type;
            //model.StackNumber = dispatch.StackNumber;
            //model.StoreID = dispatch.StoreID;
            model.TransporterID = dispatch.TransporterID;
            model.WarehouseID = dispatch.HubID;
            //model.ProgramID = dispatch.ProgramID;
            model.WeighBridgeTicketNumber = dispatch.WeighBridgeTicketNumber;
            model.Remark = dispatch.Remark;

            model.OtherDispatchAllocationID = dispatch.OtherDispatchAllocationID;
            model.DispatchAllocationID = dispatch.DispatchAllocationID;

            // model.ProjectNumber = dispatch.ProjectNumber;
            //model.SINumber = dispatch.SINumber;
            //TODO:Check modification have any impact
            Transaction transaction = transactionObj;// Repository.Dispatch.GetDispatchTransaction(dispatch.DispatchID);
            if (transaction != null)
            {
                if (transaction.Stack != null) model.StackNumber = transaction.Stack.Value;
                if (transaction.StoreID.HasValue) model.StoreID = transaction.StoreID.Value;
                model.ProgramID = transaction.ProgramID;
                model.ProjectNumber = transaction.ProjectCode.Value;
                model.SINumber = transaction.ShippingInstruction.Value;
                model.CommodityTypeID = transaction.Commodity.CommodityTypeID;
            }
            model.DispatchDetails = DispatchDetailModel.GenerateDispatchDetailModels(dispatch.DispatchDetails);
            return model;
        }

        /// <summary>
        /// Gets or sets the dispatch details.
        /// </summary>
        /// <value>
        /// The dispatch details.
        /// </value>
        public List<DispatchDetailModel> DispatchDetails { get; set; }

        public Guid? DispatchAllocationID { get; set; }

        public Guid? OtherDispatchAllocationID { get; set; }

        public bool ChangeStoreManPermanently { get; set; }
    }

    public class DispatchViewModel
    {


        public Guid? DispatchID { get; set; }


        public string FDP { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }

        [Required]
        [Display(Name = "Store Man")]
        [UIHint("AmharicTextBox")]
        public string DispatchedByStoreMan { get; set; }

        [Required]
        [Display(Name = "Dispatch Date")]
        public DateTime DispatchDate { get; set; }
         [Display(Name = "Requisition No")]
        public string RequisitionNo { get; set; }

        [Required]
        [Display(Name = "GIN")]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numbers allowed for GIN")]
        [Remote("NotUnique", "Dispatch", AdditionalFields = "DispatchID")]
        [StringLength(7, ErrorMessage = "Length Must be less than or equal to 7")]
        public string GIN { get; set; }

        public string BidNumber { get; set; }



        //[RegularExpression("[a-zA-Z\\s]*", ErrorMessage = "Only letters are allowed for Driver name")]
        [Required(ErrorMessage = "Name of the driver taking the commodities is a required field.")]
        [Display(Name = "Received By (Driver Name)")]
        [UIHint("AmharicTextBox")]
        public string DriverName { get; set; }


        [Required(ErrorMessage = "Prime Plate Number is a required field")]
        [Display(Name = "Plate No. Prime")]
        public string PlateNo_Prime { get; set; }


        [Display(Name = "Plate No. Trailer")]
        public string PlateNo_Trailer { get; set; }


        public string Transporter { get; set; }

         [Display(Name = "Project Code")]
        public string ProjectNumber { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }


        public String Program { get; set; }


        [Display(Name = "Weigh Bridge Ticket Number")]
        public string WeighBridgeTicketNumber { get; set; }


        public int Round { get; set; }



        public string SINumber { get; set; }

        public int CommodityID { get; set; }
        public string Commodity { get; set; }
        [Display(Name = "Remark")]
        [UIHint("AmharicTextArea")]
        public string Remark { get; set; }

        public int Type { get; set; }
         [Display(Name = "Quantity In Unit")]
        public decimal QuantityInUnit { get; set; }
        public decimal Quantity { get; set; }
        [Display(Name = "Unit")]
        public int UnitID { get; set; }

        public Guid DispatchAllocationID { get; set; }
        public int TransporterID { get; set; }

        public int ProgramID
        {
            get;
            set;
        }

        public int HubID
        {
            get;
            set;
        }

        public int FDPID
        {
            get;
            set;
        }
 [Display(Name = "Created Date")]
        public DateTime CreatedDate
        {
            get;
            set;
        }
        [Display(Name="Created Date")]
        public string CreatedDatePref { get; set; }
        [Display(Name="Dispatched Date")]
        public string DispatchDatePref { get; set; }
        public int UserProfileID { get; set; }
        public int? ShippingInstructionID { get; set; }
        public int? ProjectCodeID { get; set; }
    }
}
