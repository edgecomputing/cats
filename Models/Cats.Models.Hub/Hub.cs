using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class Hub
    {
        public Hub()
        {
            this.Dispatches = new List<Dispatch>();
            this.DispatchAllocations = new List<DispatchAllocation>();
            this.OtherDispatchAllocations = new List<OtherDispatchAllocation>();
            this.OtherDispatchAllocations1 = new List<OtherDispatchAllocation>();
            this.ReceiptAllocations = new List<ReceiptAllocation>();
            this.ReceiptAllocations1 = new List<ReceiptAllocation>();
            this.Receives = new List<Receive>();
            this.Stores = new List<Store>();
            this.Transactions = new List<Transaction>();
            this.UserHubs = new List<UserHub>();
            this.HubSettingValues = new List<HubSettingValue>();
        }
        [Key]
        public int HubID { get; set; }
        public string Name { get; set; }
        public int HubOwnerID { get; set; }
        public virtual ICollection<Dispatch> Dispatches { get; set; }
        public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
        public virtual ICollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }
        public virtual ICollection<OtherDispatchAllocation> OtherDispatchAllocations1 { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations1 { get; set; }
        public virtual ICollection<Receive> Receives { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<UserHub> UserHubs { get; set; }
        public virtual HubOwner HubOwner { get; set; }
        public virtual ICollection<HubSettingValue> HubSettingValues { get; set; }
    }
}
