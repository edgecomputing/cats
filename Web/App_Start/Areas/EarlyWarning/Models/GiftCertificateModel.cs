using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.GiftCertificate.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GiftCertificateModel
    {
        /// <summary>
        /// Gets or sets the name of the month.
        /// </summary>
        /// <value>
        /// The name of the month.
        /// </value>
        public string MonthName { get; set; }
        /// <summary>
        /// Gets or sets the wheat.
        /// </summary>
        /// <value>
        /// The wheat.
        /// </value>
        public decimal Wheat { get; set; }
        /// <summary>
        /// Gets or sets the rice.
        /// </summary>
        /// <value>
        /// The rice.
        /// </value>
        public decimal Rice { get; set; }
        /// <summary>
        /// Gets or sets the CSB.
        /// </summary>
        /// <value>
        /// The CSB.
        /// </value>
        public decimal CSB { get; set; }
        /// <summary>
        /// Gets or sets the oil.
        /// </summary>
        /// <value>
        /// The oil.
        /// </value>
        public decimal Oil { get; set; }
       
    }
}