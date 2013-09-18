using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub
{
    /// <summary>
    /// 
    /// </summary>
    public class FDPBalance
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FDPBalance"/> class.
        /// </summary>
        public FDPBalance()
        {
            TotalAllocation = 0;
            CommitedAllocation = 0;
            CurrentBalance = 0;
        }

        /// <summary>
        /// Gets or sets the commodity.
        /// </summary>
        /// <value>
        /// The commodity.
        /// </value>
        public string Commodity { get; set; }
        /// <summary>
        /// Gets or sets the total allocation.
        /// </summary>
        /// <value>
        /// The total allocation.
        /// </value>
        public decimal TotalAllocation { get; set; }

        /// <summary>
        /// Gets or sets the total dispatched MT.
        /// </summary>
        /// <value>
        /// The total dispatched MT.
        /// </value>
        public decimal TotalDispatchedMT { get; set; }

        /// <summary>
        /// Gets or sets the commited allocation.
        /// </summary>
        /// <value>
        /// The commited allocation.
        /// </value>
        public decimal CommitedAllocation { get; set; }
        /// <summary>
        /// Gets or sets the current balance.
        /// </summary>
        /// <value>
        /// The current balance.
        /// </value>
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// Gets or sets the bid number.
        /// </summary>
        /// <value>
        /// The bid number.
        /// </value>
        public string BidNumber { get; set; }

        /// <summary>
        /// Gets or sets the program ID.
        /// </summary>
        /// <value>
        /// The program ID.
        /// </value>
        public int ProgramID { get; set; }

        /// <summary>
        /// Gets or sets the commodity type ID.
        /// </summary>
        /// <value>
        /// The commodity type ID.
        /// </value>
        public int CommodityTypeID { get; set; }

        /// <summary>
        /// Gets or sets the transporter ID.
        /// </summary>
        /// <value>
        /// The transporter ID.
        /// </value>
        public int TransporterID { get; set; }

        /// <summary>
        /// Gets or sets the project code.
        /// </summary>
        /// <value>
        /// The project code.
        /// </value>
        public string ProjectCode { get; set; }

        public string mesure { get; set; }

        public int multiplier { get; set; }

    }
}
