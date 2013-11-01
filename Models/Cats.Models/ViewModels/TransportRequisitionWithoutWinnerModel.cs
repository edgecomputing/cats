using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.ViewModels
{
    public class TransportRequisitionWithoutWinnerModel
    {
        
        public int TransReqWithoutTransporterID { get; set; }
        public int TransportRequisitionID{ get; set; }
        public int RequisitionDetailID { get; set; }
        public int RequisitionID { get; set; }
        public int FdpID { get; set; }
        [Display(Name = "Destination")]
        public string FDP { get; set; }
        public string Woreda { get; set; }
        public int SourceWarehouseID { get; set; }
        public int beneficiaryNumber { get; set; }
        public string OriginWarehouse { get; set; }
        public int HubID { get; set; }
        public decimal QuantityQtl { get; set; }
        public Nullable<decimal> DistanceFromOrigin { get; set; }
        public int CommodityID { get; set; }
        public string Commodity { get; set; }
        public string Zone { get; set; }
        public bool Selected { get; set; }
        public string RequisitionNo { get; set; }
        public decimal Amount
        {
            get { return QuantityQtl*beneficiaryNumber; }

        }
    }
    public class TransportRequisitionWithTransporter
    {
        public List<TransportRequisitionWithoutWinnerModel> TransReqwithOutTransporters { get; set; }
        //public List<Transporter> Transporters { get; set; }
        //public Transporter SelectedTransporter { get; set; }
        public int SelectedTransporterID { get; set; }
    }

    //public class TransporterViewModel
    //{
    //    public int TransporterID { get; set; }
    //    public string Name { get; set; }
    //}
}