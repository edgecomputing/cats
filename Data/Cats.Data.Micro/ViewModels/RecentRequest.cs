using System;




namespace Cats.Data.Micro.ViewModels

{
    public class RecentRequest
    {
        public int RequestID { get; set; }
        public string RequestNumber { get; set; }
        public int Month { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
    }
}