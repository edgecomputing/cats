using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models.Hub
{
    public class PrintCertificateModel
    {
        /// <summary>
        /// Gets or sets the selcted template id.
        /// </summary>
        /// <value>
        /// The selcted template id.
        /// </value>
        public int SelctedTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the selected certificate id.
        /// </summary>
        /// <value>
        /// The selected certificate id.
        /// </value>
        public int SelectedCertificateId { get; set; }
    }
}