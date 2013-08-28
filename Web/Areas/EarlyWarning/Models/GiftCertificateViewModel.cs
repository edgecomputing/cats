using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;

namespace Cats.Areas.GiftCertificate.Models
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

        public string CommodityName { get; set; }
        public string Donor { get; set; }
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

        public string Program { get; set; }
        /// <summary>
        /// Gets or sets the D mode of transport.
        /// </summary>
        /// <value>
        /// The D mode of transport.
        /// </value>
        [Required(ErrorMessage = "Mode Of Transport is required")]
        public Int32 DModeOfTransport { get; set; }

        public string DModeOfTransportName { get; set; }

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

        public string CommodityType { get; set; }
        /// <summary>
        /// Gets or sets the row count.
        /// </summary>
        /// <value>
        /// The row count.
        /// </value>
        //[Required]
        //[Range(1, 9999, ErrorMessage = "Please insert at least one commodity")]
        public int rowCount { get; set; }

       
     }
}