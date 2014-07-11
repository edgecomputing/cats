    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace Cats.Models.Hubs
    {
        public class AdminUnitDto
        {
            public int? ParentID { get; set; }
            public int AdminUnitID { get; set; }
            public List<Fdp> Fdp { set; get; }
            public AdminUnitDto AdminUnitDtoInst{ get ;set;}
        }

        public class AdminUnitTree
        {

            public List<Region> theTree { get; set; }
        
        }

        public class Region
        {
            public int regionId { get; set; }
            public string regionName { get; set; }
            public List<Woreda> Woredas { set;get; }
        }
        public class Woreda
        {
            public int regionId { get; set; }
            public int woredaId { get; set; }
            public string woredaName { get; set; }
            public List<Zone> Zones { set; get; }
        }

        public class Zone
        {
            public int woredaId { get; set; }
            public int zoneId { get; set; }
            public string zoneName { get; set; }
            public List<Fdp> FDPs { set;get; }
        }

        public class Fdp
        {
            public int FdpId { get; set; }
            public string FdpName { get; set; }
            public int zoneId { get; set; }
        }

        public class CommodityBalance
        {
            /// <summary>
            /// Gets or sets the commodity.
            /// </summary>
            /// <value>
            /// The commodity.
            /// </value>
            public string Commodity { get; set; }
            /// <summary>
            /// Gets or sets the balances.
            /// </summary>
            /// <value>
            /// The balances.
            /// </value>
            public List<SIBalance> Balances { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class SIBalance
        {
            /// <summary>
            /// Gets or sets the SI number ID.
            /// </summary>
            /// <value>
            /// The SI number ID.
            /// </value>
            public int SINumberID { get; set; }
            /// <summary>
            /// Gets or sets the SI number.
            /// </summary>
            /// <value>
            /// The SI number.
            /// </value>
            public string SINumber { get; set; }
            /// <summary>
            /// Gets or sets the available balance.
            /// </summary>
            /// <value>
            /// The available balance.
            /// </value>
            public decimal AvailableBalance { get; set; }


            /// <summary>
            /// Gets or sets the commited to FDP.
            /// </summary>
            /// <value>
            /// The commited to FDP.
            /// </value>
            public decimal CommitedToFDP { get; set; }

            /// <summary>
            /// Gets or sets the commited to others.
            /// </summary>
            /// <value>
            /// The commited to others.
            /// </value>
            public decimal CommitedToOthers { get; set; }


            /// <summary>
            /// Gets or sets the dispatchable stock in MT.
            /// </summary>
            /// <value>
            /// The dispatchable.
            /// </value>
            public decimal Dispatchable { get; set; }


            public decimal ReaminingExpectedReceipts { get; set; }


            public decimal TotalDispatchable { get; set; }
            /// <summary>
            /// Gets or sets the commodity.
            /// </summary>
            /// <value>
            /// The commodity.
            /// </value>
            public string Commodity { get; set; }
            /// <summary>
            /// Gets or sets the project.
            /// </summary>
            /// <value>
            /// The project.
            /// </value>
            public string Project { get; set; }
            /// <summary>
            /// Gets or sets the project code ID.
            /// </summary>
            /// <value>
            /// The project code ID.
            /// </value>
            public int ProjectCodeID { get; set; }
            /// <summary>
            /// Gets or sets the program.
            /// </summary>
            /// <value>
            /// The program.
            /// </value>
            public string Program { get; set; }


        }
    }
