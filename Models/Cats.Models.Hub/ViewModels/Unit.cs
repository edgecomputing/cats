using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Cats.Models.Hub.MetaModels;

namespace Cats.Models.Hub
{
  
    partial class Unit
    {
        public class Constants
        {
            public const int BAG = 1;
            public const int CARTON = 2;
            public const int BUNDLE = 3;
            public const int CAN = 4;
            public const int SILO = 7;
            
        }

        public static Unit GetUnitByName(string name)
        {
            return null;
            //TODO:refactor
            //return new CTSContext().Units.Where(u => u.Name == name).SingleOrDefault();
        }

    }
}
