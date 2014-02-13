using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models
{
    public class Month
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Month(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}