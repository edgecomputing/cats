using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class HubDto
    {
        public HubDto(){ }

        public HubDto(int id, string name)
        {
            this.HubId = id;
            this.HubName = name;
        }
        public int  HubId { get; set; }
        public string HubName { get; set; }
    }
}