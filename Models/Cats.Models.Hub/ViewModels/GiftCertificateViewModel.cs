using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hub;

namespace Cats.Models.Hub
{
    /// <summary>
    /// 
    /// </summary>
    public class GiftCertificateViewModel
    {
        /// <summary>
        /// Gets or sets the gift certificate ID.
        /// </summary>
        /// <value>
        /// The gift certificate ID.
        /// </value>
        [Required(ErrorMessage = "Gift Certificate is required")]
        public Int32 GiftCertificateID { get; set; }

        /// <summary>
        /// Gets or sets the gift date.
        /// </summary>
        /// <value>
        /// The gift date.
        /// </value>
        [Required(ErrorMessage = "Gift Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime GiftDate { get; set; }

        /// <summary>
        /// Gets or sets the donor ID.
        /// </summary>
        /// <value>
        /// The donor ID.
        /// </value>
        [Required(ErrorMessage = "Donor is required")]
        public Int32 DonorID { get; set; }

        /// <summary>
        /// Gets or sets the SI number.
        /// </summary>
        /// <value>
        /// The SI number.
        /// </value>
        [Required(ErrorMessage = "SI Number is required")]
        [Remote("NotUnique", "GiftCertificate", AdditionalFields = "GiftCertificateID")]
        [StringLength(50)]
        //[Key]
        public String SINumber { get; set; }

        /// <summary>
        /// Gets or sets the reference no.
        /// </summary>
        /// <value>
        /// The reference no.
        /// </value>
        [Required(ErrorMessage = "Reference No is required")]
        [StringLength(50)]
        public String ReferenceNo { get; set; }

        /// <summary>
        /// Gets or sets the vessel.
        /// </summary>
        /// <value>
        /// The vessel.
        /// </value>
        [StringLength(50)]
        [Required(ErrorMessage = "Vessel is required")]
        public String Vessel { get; set; }

        /// <summary>
        /// Gets or sets the ETA.
        /// </summary>
        /// <value>
        /// The ETA.
        /// </value>
        [Required(ErrorMessage = "ETA is required")]
        [DataType(DataType.DateTime)]
        public DateTime ETA { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is printed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is printed; otherwise, <c>false</c>.
        /// </value>
        [Required(ErrorMessage = "Is Printed is required")]
        public Boolean IsPrinted { get; set; }

        /// <summary>
        /// Gets or sets the program ID.
        /// </summary>
        /// <value>
        /// The program ID.
        /// </value>
        [Required(ErrorMessage = "Program is required")]
        public Int32 ProgramID { get; set; }

        /// <summary>
        /// Gets or sets the D mode of transport.
        /// </summary>
        /// <value>
        /// The D mode of transport.
        /// </value>
        [Required(ErrorMessage = "Mode Of Transport is required")]
        public Int32 DModeOfTransport { get; set; }

        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        /// <value>
        /// The name of the port.
        /// </value>
        [StringLength(50)]
        public String PortName { get; set; }


        //public EntityCollection<Donor> Donor { get; set; }

        /// <summary>
        /// Gets or sets the gift certificate details.
        /// </summary>
        /// <value>
        /// The gift certificate details.
        /// </value>
        public List<GiftCertificateDetailsViewModel> GiftCertificateDetails { get; set; }

        /// <summary>
        /// Gets or sets the JSON inserted gift certificate details.
        /// </summary>
        /// <value>
        /// The JSON inserted gift certificate details.
        /// </value>
        public string JSONInsertedGiftCertificateDetails { get; set; }

        /// <summary>
        /// Gets or sets the JSON deleted gift certificate details.
        /// </summary>
        /// <value>
        /// The JSON deleted gift certificate details.
        /// </value>
        public string JSONDeletedGiftCertificateDetails { get; set; }

        /// <summary>
        /// Gets or sets the JSON updated gift certificate details.
        /// </summary>
        /// <value>
        /// The JSON updated gift certificate details.
        /// </value>
        public string JSONUpdatedGiftCertificateDetails { get; set; }

        [Required]
        public int CommodityTypeID { get; set; }
        /// <summary>
        /// Gets or sets the row count.
        /// </summary>
        /// <value>
        /// The row count.
        /// </value>
        [Required]
        [Range(1, 9999, ErrorMessage = "Please insert at least one commodity")]
        public int rowCount { get; set; }

        /// <summary>
        /// Gifts the certificate model.
        /// </summary>
        /// <param name="GiftCertificateModel">The gift certificate model.</param>
        /// <returns></returns>
        public static GiftCertificateViewModel GiftCertificateModel(GiftCertificate GiftCertificateModel)
        {
            GiftCertificateViewModel giftCertificateViewModel = new GiftCertificateViewModel();

            giftCertificateViewModel.GiftCertificateID = GiftCertificateModel.GiftCertificateID;
            giftCertificateViewModel.GiftDate = GiftCertificateModel.GiftDate;
            giftCertificateViewModel.DonorID = GiftCertificateModel.DonorID;
            giftCertificateViewModel.SINumber = GiftCertificateModel.SINumber;
            giftCertificateViewModel.ReferenceNo = GiftCertificateModel.ReferenceNo;
            giftCertificateViewModel.Vessel = GiftCertificateModel.Vessel;
            giftCertificateViewModel.ETA = GiftCertificateModel.ETA;
            giftCertificateViewModel.ProgramID = GiftCertificateModel.ProgramID;
            giftCertificateViewModel.PortName = GiftCertificateModel.PortName;
            giftCertificateViewModel.DModeOfTransport = GiftCertificateModel.DModeOfTransport;
            var giftCertificateDetail = GiftCertificateModel.GiftCertificateDetails.FirstOrDefault();
            if (giftCertificateDetail != null)
                giftCertificateViewModel.CommodityTypeID = giftCertificateDetail.Commodity.CommodityTypeID;
            else
                giftCertificateViewModel.CommodityTypeID = 1;//by default 'food' 
            giftCertificateViewModel.GiftCertificateDetails =
               GiftCertificateDetailsViewModel.GenerateListOfGiftCertificateDetailsViewModel(
                   GiftCertificateModel.GiftCertificateDetails.ToList());


            return giftCertificateViewModel;
        }


        /// <summary>
        /// Generates the gift certificate.
        /// </summary>
        /// <returns></returns>
        public GiftCertificate GenerateGiftCertificate()
        {
            GiftCertificate giftCertificate = new GiftCertificate()
                                                  {
                                                      GiftCertificateID = this.GiftCertificateID,
                                                      GiftDate = this.GiftDate,
                                                      SINumber = this.SINumber,
                                                      DonorID = this.DonorID,
                                                      ReferenceNo = this.ReferenceNo,
                                                      Vessel = this.Vessel,
                                                      ETA = this.ETA,
                                                      IsPrinted = this.IsPrinted,
                                                      DModeOfTransport  = this.DModeOfTransport,
                                                      ProgramID = this.ProgramID,
                                                      PortName = this.PortName
                                                  };
            return giftCertificate;

        }


        /// <summary>
        /// Generates the gift certificate detail.
        /// </summary>
        /// <param name="giftCertificateDetails">The gift certificate details.</param>
        /// <returns></returns>
        public static List<GiftCertificateDetail> GenerateGiftCertificateDetail(List<GiftCertificateDetailsViewModel> giftCertificateDetails)
        {
            if (giftCertificateDetails != null)
            {
                var gifts = from g in giftCertificateDetails
                            where g != null
                            select new GiftCertificateDetail()
                            {
                                CommodityID = g.CommodityID,
                                BillOfLoading = g.BillOfLoading,
                                YearPurchased = g.YearPurchased,
                                AccountNumber = g.AccountNumber,
                                WeightInMT = g.WeightInMT,
                                EstimatedPrice = g.EstimatedPrice,
                                EstimatedTax = g.EstimatedTax,
                                DCurrencyID = g.DCurrencyID,
                                DFundSourceID = g.DFundSourceID,
                                DBudgetTypeID = g.DBudgetTypeID,
                                GiftCertificateDetailID = g.GiftCertificateDetailID,

                            };
                return gifts.ToList();
            }
            else
            {
                return Enumerable.Empty<GiftCertificateDetail>().ToList();
            }
        }

     }
}