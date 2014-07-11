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
    public class AdminUnitModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminUnitModel"/> class.
        /// </summary>
        public AdminUnitModel()
        {
            
        }
        /// <summary>
        /// Gets or sets the admin unit types.
        /// </summary>
        /// <value>
        /// The admin unit types.
        /// </value>
        public List<AdminUnitType> AdminUnitTypes { get; set; }
        /// <summary>
        /// Gets or sets the selected admin unit type id.
        /// </summary>
        /// <value>
        /// The selected admin unit type id.
        /// </value>
        public int SelectedAdminUnitTypeId { get; set; }
        /// <summary>
        /// Gets or sets the regions.
        /// </summary>
        /// <value>
        /// The regions.
        /// </value>
        public List<AdminUnitItem> Regions { get; set; }
        /// <summary>
        /// Gets or sets the selected woreda id.
        /// </summary>
        /// <value>
        /// The selected woreda id.
        /// </value>
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings=true)]
        public int SelectedWoredaId { get; set; }
        /// <summary>
        /// Gets or sets the selected zone id.
        /// </summary>
        /// <value>
        /// The selected zone id.
        /// </value>
        public int SelectedZoneId { get; set; }
        /// <summary>
        /// Gets or sets the selected region id.
        /// </summary>
        /// <value>
        /// The selected region id.
        /// </value>
        public int SelectedRegionId { get; set; }
        /// <summary>
        /// Gets or sets the name of the selected region.
        /// </summary>
        /// <value>
        /// The name of the selected region.
        /// </value>
        public string SelectedRegionName { get; set; }
        /// <summary>
        /// Gets or sets the name of the selected zone.
        /// </summary>
        /// <value>
        /// The name of the selected zone.
        /// </value>
        public string SelectedZoneName { get; set; }
        /// <summary>
        /// Gets or sets the name of the selected woreda.
        /// </summary>
        /// <value>
        /// The name of the selected woreda.
        /// </value>
        public string SelectedWoredaName { get; set; }
        /// <summary>
        /// Gets or sets the name of the unit.
        /// </summary>
        /// <value>
        /// The name of the unit.
        /// </value>
        [Required(ErrorMessage = "Name is required")]        
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets the unit name AM.
        /// </summary>
        /// <value>
        /// The unit name AM.
        /// </value>
        [Required(ErrorMessage = "Name in Amahric is required")]
        [Display(Name = "Name (Amharic)")]
        [UIHint("AmharicTextBox")]
        public string UnitNameAM { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AdminUnitItem
    {
        /// <summary>
        /// Gets or sets the name of the grand parent.
        /// </summary>
        /// <value>
        /// The name of the grand parent.
        /// </value>
        public string GrandParentName { get; set; }
        /// <summary>
        /// Gets or sets the name of the parent.
        /// </summary>
        /// <value>
        /// The name of the parent.
        /// </value>
        public string ParentName { get; set; }
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
