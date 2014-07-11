using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    public class SIReportModel
    {
        /// <summary>
        /// Gets or sets the recieved.
        /// </summary>
        /// <value>
        /// The recieved.
        /// </value>
        public List<TransactedStock> Recieved { get; set; }
        /// <summary>
        /// Gets or sets the dispatched.
        /// </summary>
        /// <value>
        /// The dispatched.
        /// </value>
        public List<TransactedStock> Dispatched { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SIReportModel"/> class.
        /// </summary>
        public SIReportModel()
        {
            Recieved = new List<TransactedStock>();
            Dispatched = new List<TransactedStock>();
        }
    }

    public class TransactedStock
    {
        /// <summary>
        /// Gets or sets the GIN.
        /// </summary>
        /// <value>
        /// The GIN.
        /// </value>
        public string GIN { get; set; }
        /// <summary>
        /// Gets or sets the GRN.
        /// </summary>
        /// <value>
        /// The GRN.
        /// </value>
        public string GRN { get; set; }
        /// <summary>
        /// Gets or sets the warehouse.
        /// </summary>
        /// <value>
        /// The warehouse.
        /// </value>
        public string Warehouse { get; set; }
        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        public string Store { get; set; }
        /// <summary>
        /// Gets or sets the FDP.
        /// </summary>
        /// <value>
        /// The FDP.
        /// </value>
        public string FDP { get; set; }
        /// <summary>
        /// Gets or sets the woreda.
        /// </summary>
        /// <value>
        /// The woreda.
        /// </value>
        public string Woreda { get; set; }
        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        /// <value>
        /// The zone.
        /// </value>
        public string Zone { get; set; }
        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the commodity.
        /// </summary>
        /// <value>
        /// The commodity.
        /// </value>
        public string Commodity { get; set; }
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public decimal Quantity { get; set; }
    }
}