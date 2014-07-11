using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Cats.Models.Hubs.MetaModels;

namespace Cats.Models.Hubs
{
    
    partial class Store
    {
        [NotMapped]
        public List<int> Stacks
        {
            get
            {
                List<int> stacks = new List<int>();
                for (int count = 1; count <= this.StackCount; count++)
                {
                    stacks.Add(count);
                }
                return stacks;
            }
        }
    }
}
