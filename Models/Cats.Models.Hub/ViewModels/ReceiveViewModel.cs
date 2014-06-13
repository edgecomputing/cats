using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Cats.Models.Hubs.ViewModels
{


    public class ReceiveViewModelDto
    {
        public Guid? ReceiveID { get; set; }
        public string GRN { get; set; }
        [UIHint("DateTime")]
        [DataType(DataType.DateTime)]
        public DateTime ReceiptDate { get; set; }
        public string ReceivedByStoreMan { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ReceiveViewModel
    {




        #region List of Things used in the recieve view model
        //todo:separation of concern
     //   private IUnitOfWork _Repository  = new UnitOfWork();
        private UserProfile _UserProfile = null;
        /// <summary>
        /// Lists of important Lookups,
        /// </summary>
        /// 
        private List<Unit> _units;
        public List<Unit> Units
        {
            get
            {
                //TODO://Refactor
                //if (_units == null)
                //{
                //    _units = _Repository.UnitRepository.GetAll().OrderBy(o => o.Name).ToList();
                //}
                return _units;
            }

        }
        public List<CommodityGrade> CommodityGrades { get; set; }
        public List<Commodity> Commodities { get; set; }
        public List<Transporter> Transporters { get; set; }
        public List<CommodityType> CommodityTypes { get; set; }
        public List<CommoditySource> CommoditySources { get; set; }
        public List<Donor> Donors { get; set; }

        public List<Store> Stores { get; set; }
        public List<Program> Programs { get; set; }
        public List<Hub> Hubs { get; set; }

        private List<AdminUnitItem> _stacks;
        public List<AdminUnitItem> Stacks
        {
            get
            {
                //TODO:Make sure constractor stacks variable brings same data with the same logic
                if (this.StoreID != 0)
                {
                    var store = new Store();
                    foreach (var store1 in Stores.Where(store1 => store1.StoreID == StoreID))
                    {
                        store = store1;
                    }
                    var stacks = new List<AdminUnitItem>();
                    foreach (var i in store.Stacks)
                    {
                        stacks.Add(new AdminUnitItem { Name = i.ToString(), Id = i });
                    }
                    return stacks;
                }
                return new List<AdminUnitItem>();
                return _stacks;
            }
            set { _stacks = value; }
        }
        #endregion

        /// <summary>
        /// parameterless constructor, which is hidden so that this object is not constructed with out the user being specified,
        /// </summary>

        public ReceiveViewModel()
        {

        }


        /// <summary>
        /// constructor with the testable repositories and the user
        /// the user is required because we need to decide what wareshouses to display for her.
        /// </summary>
        public ReceiveViewModel(List<Commodity> commodities, List<CommodityGrade> commodityGrades, List<Transporter> transporters, List<CommodityType> commodityTypes,
            List<CommoditySource> commoditySources, List<Program> programs, List<Donor> donors, List<Hub> hubs, UserProfile user,List<Unit> units  )
        {
            _UserProfile = user;
            InitalizeViewModel(commodities, commodityGrades, transporters, commodityTypes,
             commoditySources, programs, donors, hubs, user, units);
        }

        /// <summary>
        /// Initalizes the view model.
        /// </summary>
        private void InitalizeViewModel(List<Commodity> commodities, List<CommodityGrade> commodityGrades, List<Transporter> transporters, List<CommodityType> commodityTypes,
            List<CommoditySource> commoditySources, List<Program> programs, List<Donor> donors, List<Hub> hubs, UserProfile user, List<Unit> units)
        {
            _UserProfile = user;
            ReceiveID = null;
            IsEditMode = false;
            ReceiptDate = DateTime.Now;
            InitializeEditLists(commodities, commodityGrades, transporters, commodityTypes,
             commoditySources, programs, donors, hubs, user, units);
            ReceiveDetails = new List<ReceiveDetailViewModel>();
            ReceiveDetails.Add(new ReceiveDetailViewModel());

        }

        /// <summary>
        /// Initializes the edit lists.
        /// </summary>
        public void InitializeEditLists(List<Commodity> commodities, List<CommodityGrade> commodityGrades, List<Transporter> transporters, List<CommodityType> commodityTypes,
            List<CommoditySource> commoditySources, List<Program> programs, List<Donor> donors, List<Hub> hubs, UserProfile user, List<Unit> units)
        {
            //_UserProfile = user;
            Commodities = commodities;// _Repository.Commodity.GetAll().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            CommodityGrades = commodityGrades;// _Repository.CommodityGrade.GetAll().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            Transporters = transporters;// _Repository.Transporter.GetAll().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            CommoditySources = commoditySources;// _Repository.CommoditySource.GetAll().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            CommodityTypes = commodityTypes;// _Repository.CommodityType.GetAll().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            Stores = user.DefaultHub.Stores.DefaultIfEmpty().ToList();
            Programs = programs;// _Repository.Program.GetAll().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            Donors = donors;// _Repository.Donor.GetAll().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            //=========================Old Comment============================================
            //_Repository.Hub.GetOthersWithDifferentOwner(user.DefaultHub); //
            //remove the users current ware house from the list not to allow receive from HUBX to HUBX 
            //==========================end old comment=======================================
            Hubs = hubs;// _Repository.Hub.GetAllWithoutId(user.DefaultHub.HubID).DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            _units = units;


        }

        /// <summary>
        /// Gets or sets the receive ID.
        /// </summary>
        /// <value>
        /// The receive ID.
        /// </value>
        [Key]
        public Guid? ReceiveID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is edit mode.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is edit mode; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditMode { get; set; }

        /// <summary>
        /// Gets or sets the SI number.
        /// </summary>
        /// <value>
        /// The SI number.
        /// </value>
        [Required(ErrorMessage = "SI Number is required")]
        public string SINumber { get; set; }

        /// <summary>
        /// Gets or sets the GRN.
        /// </summary>
        /// <value>
        /// The GRN.
        /// </value>
        [Required]
        [Key]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numbers allowed for GRN")]
        [StringLength(7, ErrorMessage = "Length Must be less than or equal to 7")]
        [Remote("NotUnique", "Receive", AdditionalFields = "ReceiveID")]
        public string GRN { get; set; }


        /// <summary>
        /// Gets or sets the hub ID.
        /// </summary>
        /// <value>
        /// The hub ID.
        /// </value>
        public int HubID { get; set; }

        /// <summary>
        /// Gets or sets the store ID.
        /// </summary>
        /// <value>
        /// The store ID.
        /// </value>
        [Required(ErrorMessage = "Store is required")]
        public int StoreID { get; set; }

        /// <summary>
        /// Gets or sets the stack number.
        /// </summary>
        /// <value>
        /// The stack number.
        /// </value>
        [Required(ErrorMessage = "Stack number is required")]
        public int StackNumber { get; set; }

        /// <summary>
        /// Gets or sets the commodity type ID.
        /// </summary>
        /// <value>
        /// The commodity type ID.
        /// </value>
        [Required(ErrorMessage = "Commodity type is required")]
        public int CommodityTypeID { get; set; }

        /// <summary>
        /// Gets or sets the source donor ID.
        /// </summary>
        /// <value>
        /// The source donor ID.
        /// </value>
        public int? SourceDonorID { get; set; }

        /// <summary>
        /// Gets or sets the responsible donor ID.
        /// </summary>
        /// <value>
        /// The responsible donor ID.
        /// </value>
        [Required(ErrorMessage = "Donor is required")]
        public int? ResponsibleDonorID { get; set; }

        /// <summary>
        /// Gets or sets the program ID.
        /// </summary>
        /// <value>
        /// The program ID.
        /// </value>
        [Required(ErrorMessage = "Program is required")]
        public int ProgramID { get; set; }

        /// <summary>
        /// Gets or sets the transporter ID.
        /// </summary>
        /// <value>
        /// The transporter ID.
        /// </value>
        [Required(ErrorMessage = "Transporter is required")]
        public int TransporterID { get; set; }

        /// <summary>
        /// Gets or sets the user profile ID.
        /// </summary>
        /// <value>
        /// The user profile ID.
        /// </value>
        public int UserProfileID { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }


        /// <summary>
        /// Gets or sets the receipt date.
        /// </summary>
        /// <value>
        /// The receipt date.
        /// </value>
        [Required(ErrorMessage = "Reciept date is required")]
        [DateStart(ErrorMessage = "The Receipt Date Can not be in the Future")]
        public DateTime ReceiptDate { get; set; }

        /// <summary>
        /// Gets or sets the plate no_ prime.
        /// </summary>
        /// <value>
        /// The plate no_ prime.
        /// </value>
        [Required(ErrorMessage = "Prime plate number is required")]
        [StringLength(50)]
        public string PlateNo_Prime { get; set; }


        /// <summary>
        /// Gets or sets the plate no_ trailer.
        /// </summary>
        /// <value>
        /// The plate no_ trailer.
        /// </value>
        [StringLength(50)]
        public string PlateNo_Trailer { get; set; }

        /// <summary>
        /// Gets or sets the name of the driver.
        /// </summary>
        /// <value>
        /// The name of the driver.
        /// </value>
        ///
        //[RegularExpression("[a-zA-Z\\s]*", ErrorMessage = "Only letters are allowed for Driver name")]
        [Required(ErrorMessage = "Driver name is required")]
        [Display(Name = "Delivered By (Driver Name)")]
        [StringLength(50)]
        [UIHint("AmharicTextBox")]
        public string DriverName { get; set; }

        /// <summary>
        /// Gets or sets the weight before unloading.
        /// </summary>
        /// <value>
        /// The weight before unloading.
        /// </value>
        public decimal? WeightBeforeUnloading { get; set; }

        /// <summary>
        /// Gets or sets the weight after unloading.
        /// </summary>
        /// <value>
        /// The weight after unloading.
        /// </value>
        public decimal? WeightAfterUnloading { get; set; }

        /// <summary>
        /// Gets or sets the way bill no.
        /// </summary>
        /// <value>
        /// The way bill no.
        /// </value>
        [Required(ErrorMessage = "Waybill number is requied")]
        [StringLength(50)]
        public string WayBillNo { get; set; }

        /// <summary>
        /// Gets or sets the project number.
        /// </summary>
        /// <value>
        /// The project number.
        /// </value>
        [Required(ErrorMessage = "Project code is required")]
        [Display(Name = "Project Code")]
        public string ProjectNumber { get; set; }

        /// <summary>
        /// Gets or sets the commodity source ID.
        /// </summary>
        /// <value>
        /// The commodity source ID.
        /// </value>
        [Required(ErrorMessage = "Commodity source is required")]
        public int CommoditySourceID { get; set; }


        //TODO:Make sure to set value on commoditySoureText during mapping
        public string CommoditySourceText { get; set; }

        //public string CommoditySourceText{
        //    get { return _Repository.CommoditySource.FindById(CommoditySourceID).Name; }
        //}
        //TODO:Make sure to put SourceHubText during mapping
        public string SourceHubText { get; set; }
        //public string SourceHubText
        //{
        //    get { 
        //        if (SourceHubID != null) 
        //            return _Repository.Hub.FindById(SourceHubID.Value).HubNameWithOwner; 
        //        return "";
        //    }
        //}
        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        /// <value>
        /// The ticket number.
        /// </value>
        [StringLength(10)]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        [StringLength(50)]
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        [Display(Name = "Remark")]
        [StringLength(4000)]
        [UIHint("AmharicTextArea")]
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the received by store man.
        /// </summary>
        /// <value>
        /// The received by store man.
        /// </value>

        [Required(ErrorMessage = "Name of store man is required")]
        [Display(Name = "Received By Store Man")]
        [StringLength(50)]
        [UIHint("AmharicTextBox")]
        public string ReceivedByStoreMan { get; set; }

        public bool ChangeStoreManPermanently { set; get; }

        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        /// <value>
        /// The name of the port.
        /// </value>
        [Display(Name = "Port Name")]
        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the receive details.
        /// </summary>
        /// <value>
        /// The receive details.
        /// </value>
        public List<ReceiveDetailViewModel> ReceiveDetails { get; set; }

        //[Required]
        //[StringLength(10000,MinimumLength= 3,ErrorMessage= "please insert atleast one commodity")] 
        /// <summary>
        /// Gets or sets the JSON inserted commodities.
        /// </summary>
        /// <value>
        /// The JSON inserted commodities.
        /// </value>
        public string JSONInsertedCommodities { get; set; }

        /// <summary>
        /// Gets or sets the JSON deleted commodities.
        /// </summary>
        /// <value>
        /// The JSON deleted commodities.
        /// </value>
        public string JSONDeletedCommodities { get; set; }

        /// <summary>
        /// Gets or sets the JSON updated commodities.
        /// </summary>
        /// <value>
        /// The JSON updated commodities.
        /// </value>
        public string JSONUpdatedCommodities { get; set; }

        /// <summary>
        /// Gets or sets the JSON prev.
        /// </summary>
        /// <value>
        /// The JSON prev.
        /// </value>
        /// 
        [Required]
        //[StringLength(Int32.MaxValue,ErrorMessage = "Please add atleast one commodity to save this Reciept",MinimumLength = 2)]
        public string JSONPrev { get; set; }


        [StringLength(50)]
        [Required]
        public String PurchaseOrder { get; set; }

        [StringLength(50)]
        [Required]
        public String SupplierName { get; set; }

        [Required]
        public Int32? SourceHubID { get; set; }


        /// <summary>
        /// Generates the receive model.
        /// </summary>
        /// <param name="receive">The receive.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static ReceiveViewModel GenerateReceiveModel(Receive receive, List<Commodity> commodities, List<CommodityGrade> commodityGrades, List<Transporter> transporters, List<CommodityType> commodityTypes,
            List<CommoditySource> commoditySources, List<Program> programs, List<Donor> donors, List<Hub> hubs, UserProfile user,List<Unit> units )
        {

            ReceiveViewModel model = new ReceiveViewModel();
            model._UserProfile = user;

            model.InitalizeViewModel(commodities, commodityGrades, transporters, commodityTypes,
             commoditySources, programs, donors, hubs, user,units);
            model.IsEditMode = true;
            model.ReceiveID = receive.ReceiveID;
            model.DriverName = receive.DriverName;
            model.GRN = receive.GRN;
            model.PlateNo_Prime = receive.PlateNo_Prime;
            model.PlateNo_Trailer = receive.PlateNo_Trailer;

            model.TransporterID = receive.TransporterID;
            model.HubID = receive.HubID;

            ReceiveDetail receiveDetail = receive.ReceiveDetails.FirstOrDefault();//p=>p.QuantityInMT>0);
            Transaction receiveDetailtransaction = null;
            if (receiveDetail != null)
                foreach (Transaction transaction in receiveDetail.TransactionGroup.Transactions)
                {
                    var negTransaction = receiveDetail.TransactionGroup.Transactions.FirstOrDefault(p => p.QuantityInMT < 0);
                    if (negTransaction != null)
                        model.SourceHubID = negTransaction.Account.EntityID;
                    receiveDetailtransaction = transaction;
                    break;
                }
            if (receiveDetailtransaction != null)
            {
                model.SINumber = receiveDetailtransaction.ShippingInstruction != null ? receiveDetailtransaction.ShippingInstruction.Value : "";

                model.ProjectNumber = receiveDetailtransaction.ProjectCode != null ? receiveDetailtransaction.ProjectCode.Value : "";

                model.ProgramID = receiveDetailtransaction.Program != null ? receiveDetailtransaction.Program.ProgramID : default(int);

                model.StoreID = receiveDetailtransaction.Store != null ? receiveDetailtransaction.Store.StoreID : default(int);

                model.StackNumber = receiveDetailtransaction.Stack.HasValue ? receiveDetailtransaction.Stack.Value : default(int);



            }
            else
            {
                model.SINumber = "";
                model.ProjectNumber = "";
                model.ProgramID = default(int);
                model.StoreID = default(int);
                model.StackNumber = default(int);
            }

            model.ReceiptDate = receive.ReceiptDate;
            model.WayBillNo = receive.WayBillNo;
            model.CommodityTypeID = receive.CommodityTypeID;
            model.ResponsibleDonorID = receive.ResponsibleDonorID;
            model.SourceDonorID = receive.SourceDonorID;
            model.CommoditySourceID = receive.CommoditySourceID;
            model.TicketNumber = receive.WeightBridgeTicketNumber;
            model.WeightBeforeUnloading = receive.WeightBeforeUnloading;
            model.WeightAfterUnloading = receive.WeightAfterUnloading;
            model.VesselName = receive.VesselName;
            model.PortName = receive.PortName;
            model.ReceiptAllocationID = receive.ReceiptAllocationID;
            model.PurchaseOrder = receive.PurchaseOrder;
            model.SupplierName = receive.SupplierName;

            model.Remark = receive.Remark;
            model.ReceivedByStoreMan = receive.ReceivedByStoreMan;

            model.ReceiveDetails =Cats.Models.Hubs.ReceiveDetailViewModel.GenerateReceiveDetailModels(receive.ReceiveDetails);
            return model;
        }

        /// <summary>
        /// Generates the receive.
        /// </summary>
        /// <returns></returns>
        public Receive GenerateReceive()
        {
            Receive receive = new Receive()
            {
                CreatedDate = DateTime.Now,
                ReceiptDate = this.ReceiptDate,

                DriverName = this.DriverName,
                GRN = this.GRN,
                PlateNo_Prime = this.PlateNo_Prime,
                PlateNo_Trailer = this.PlateNo_Trailer,
                TransporterID = this.TransporterID,
                HubID = this.HubID,
                CommodityTypeID = this.CommodityTypeID,
                WayBillNo = this.WayBillNo,
                ResponsibleDonorID = this.ResponsibleDonorID,
                SourceDonorID = this.SourceDonorID,
                CommoditySourceID = this.CommoditySourceID,
                WeightBridgeTicketNumber = this.TicketNumber,
                WeightBeforeUnloading = this.WeightBeforeUnloading,
                WeightAfterUnloading = this.WeightAfterUnloading,
                ReceivedByStoreMan = this.ReceivedByStoreMan,
                VesselName = this.VesselName,
                PortName = this.PortName,
                PurchaseOrder = this.PurchaseOrder,
                SupplierName = this.SupplierName,
                ReceiptAllocationID = this.ReceiptAllocationID,
                Remark = this.Remark,
            };
            if (this.ReceiveID.HasValue)
            {
                receive.ReceiveID = this.ReceiveID.Value;
            }
            else
            {
                receive.ReceiveID = Guid.NewGuid();
            }
            return receive;
        }

        public Guid? ReceiptAllocationID { get; set; }
    }
}