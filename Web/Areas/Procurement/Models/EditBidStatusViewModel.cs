using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class EditBidStatusViewModel
    {
        public int BidID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime OpeningDate { get; set; }
        public int StatusID { get; set; }
        public EditBIdStatus EditStatus;

        public class  EditBIdStatus
        {
            public int Number { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime OpeningDate { get; set; }
            public int StatusID { get; set; }
        }
       
    }
}