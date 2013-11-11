using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Models.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    public class NotFoundModel : HandleErrorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundModel"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="exception"/> parameter is null.</exception>
        ///   
        /// <exception cref="T:System.ArgumentException">The <paramref name="controllerName"/> or <paramref name="actionName"/> parameter is null or empty.</exception>
        public NotFoundModel(Exception exception, string controllerName, string actionName)
            : base(exception, controllerName, actionName)
        {
        }
        /// <summary>
        /// Gets or sets the requested URL.
        /// </summary>
        /// <value>
        /// The requested URL.
        /// </value>
        public string RequestedUrl { get; set; }
        /// <summary>
        /// Gets or sets the referrer URL.
        /// </summary>
        /// <value>
        /// The referrer URL.
        /// </value>
        public string ReferrerUrl { get; set; }
    }
}