using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Spatial;

namespace Cats.Models
{
    public partial class FDP
    {
        public FDP()
        {
            this.DispatchAllocations = new List<DispatchAllocation>();
            this.RegionalRequestDetails = new List<RegionalRequestDetail>();
            this.ReliefRequisitionDetails = new List<ReliefRequisitionDetail>();
            this.TransportOrderDetails = new List<TransportOrderDetail>();
            this.Contacts = new List<Contact>();
            this.DistributionByAgeDetails=new List<DistributionByAgeDetail>();
           
        }

        public int FDPID { get; set; }
        
        public string Name { get; set; }
        public string NameAM { get; set; }
        public int AdminUnitID { get; set; }
        //public DbGeography FDPLocation { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<int> HubID { get; set; }

        public virtual AdminUnit AdminUnit { get; set; }
        public virtual ICollection<Dispatch> Dispatches { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
        public virtual ICollection<RegionalRequestDetail> RegionalRequestDetails { get; set; }
        public virtual ICollection<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public virtual ICollection<TransportOrderDetail> TransportOrderDetails { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<DistributionByAgeDetail> DistributionByAgeDetails  { get; set; }
        public virtual ICollection<WoredaStockDistributionDetail> WoredaStockDistributionDetails { get; set; }
        public virtual ICollection<DeliveryReconcile> DeliveryReconciles { get; set; }
  

    }
}
