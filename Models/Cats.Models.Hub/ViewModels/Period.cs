using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Cats.Models.Hubs.MetaModels;

namespace Cats.Models.Hubs
{
    
    partial class Period
    {

        /// <summary>
        /// Gets the months.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public static List<int> GetMonths(int year)
        {
            throw new NotImplementedException();
        }
    }
}
