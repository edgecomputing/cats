using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupedTransportation
    {
        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public string Program { get; set; }
        /// <summary>
        /// Gets or sets the transportations.
        /// </summary>
        /// <value>
        /// The transportations.
        /// </value>
        public List<TransporationReport> Transportations { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TransporationReport
    {
        /// <summary>
        /// Gets or sets the no of trucks.
        /// </summary>
        /// <value>
        /// The no of trucks.
        /// </value>
        public int NoOfTrucks { get; set; }
        /// <summary>
        /// Gets or sets the quantity in MT.
        /// </summary>
        /// <value>
        /// The quantity in MT.
        /// </value>
        public decimal QuantityInMT { get; set; }

        public decimal QuantityInUnit { get; set; }

        /// <summary>
        /// Gets or sets the commodity.
        /// </summary>
        /// <value>
        /// The commodity.
        /// </value>
        public string Commodity { get; set; }
        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public string Program { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public enum PeriodType
    {
        Daily = 0,
        Monthly = 1,
        Quarterly = 2,
        Annual = 3,
        Custom = 4
    }

    /// <summary>
    /// 
    /// </summary>
    public enum OperationMode
    {
        Dispatch = 0,
        Recieve = 1
    }
}
