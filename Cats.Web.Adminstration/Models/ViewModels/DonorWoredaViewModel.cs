using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Web.Adminstration.Models.ViewModels
{
    public class DonorWoredaViewModel
    {
        public int WoredaDonorInt { get; set; }
        public string RegionName { get; set; }
        public int RegionId { get; set; }
        public string Zone { get; set; }
        public int ZoneId { get; set; }
        public string WoredaName  { get; set; }
        public int WoredaId { get; set; }
        public int DonorId { get; set; }
        public string DonorName { get; set; }

    }
}