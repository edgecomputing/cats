using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub.ViewModels.Report.Data
{
    public class BinCardViewModel
    {
        public string Identification { get; set; }
        public string ToFrom { get; set; }
        public string Transporter { get; set; }
        public string TransporterAM { get; set; }
        public string DriverName { get; set; }
        public string SINumber { get; set; }
        public DateTime Date { get; set; }
        public string Project { get; set; }
        public decimal? Received { get; set; }
        public decimal? Dispatched { get; set; }
        public decimal Balance { get; set; }
    }
}