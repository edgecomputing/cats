using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models
{
    //TODO:This is temporary solution for the selection becasue the grid sends back on instead of true for selected

    public class SelectFromGrid
    {
        public int Number { get; set; }
        public object IsSelected { get; set; }
    }
    public class DataFromGrid
    {
        public int Number { get; set; }
        public string RequisitionNo { get; set; }
    }
}