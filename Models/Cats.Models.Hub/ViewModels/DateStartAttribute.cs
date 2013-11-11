using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Cats.Models.Hubs;

namespace Cats.Models.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DateStartAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            DateTime dateReceipt = (DateTime)value;
            //Receipt must not start in the future time.
            return (dateReceipt < DateTime.Now);
        }
    }
}