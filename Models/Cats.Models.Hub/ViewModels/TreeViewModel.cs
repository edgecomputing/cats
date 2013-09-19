using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub.Repository
{
    public class TreeViewModel
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public bool LoadOnDemand { get; set; }

        public bool Enabled { get; set; }

        public int Count { get; set; }
    }
}
