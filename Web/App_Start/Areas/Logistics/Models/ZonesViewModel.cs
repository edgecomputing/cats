using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models.Hubs.ViewModels;

namespace Cats.Areas.Logistics.Models
{
    public class ZonesViewModel
    {
        public ZonesViewModel()
        {
            Woredas = new List<WoredaViewModel>();
        }
        public int AdminUnitID { get; set; }
        public string Zone { get; set; }
        public List<WoredaViewModel> Woredas { get; set; } 
    }

    public class WoredaViewModel
    {
        public WoredaViewModel()
        {
            //FDPs = new IEnumerable<FDPViewModel>();
        }
        public int AdminUnitID { get; set; }
        public string Woreda { get; set; }
        public IEnumerable<FDPViewModel> FDPs { get; set; } 
    }

    
}