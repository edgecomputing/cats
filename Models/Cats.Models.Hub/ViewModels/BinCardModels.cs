using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models.Hub.Repository;

namespace Cats.Models.Hub
{
    /// <summary>
    /// 
    /// </summary>
    public class BinCardModels
    {
        /// <summary>
        /// Gets or sets the warehouse id.
        /// </summary>
        /// <value>
        /// The warehouse id.
        /// </value>
        public int WarehouseId { get; set; }

        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        public int store { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinCardModels"/> class.
        /// </summary>
        public BinCardModels()
        {
            WarehouseId = 1;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the project ID.
        /// </summary>
        /// <value>
        /// The project ID.
        /// </value>
        public string ProjectID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NameComparer : IEqualityComparer<Project>
    {

        #region IEqualityComparer<Project> Members

        public bool Equals(Project x, Project y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Project obj)
        {
            return obj.Name.GetHashCode();
        }

        #endregion
    }

    public class TreeCountComparer : IEqualityComparer<TreeViewModel>
    {

        #region IEqualityComparer<Project> Members

        public bool Equals(TreeViewModel x, TreeViewModel y)
        {
            return x.Value == y.Value;
        }

        public int GetHashCode(TreeViewModel obj)
        {
            return obj.Name.GetHashCode();
        }

        #endregion
    }
}