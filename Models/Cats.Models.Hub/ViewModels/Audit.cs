using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs
{
    
    partial class Audit
    {
        /// <summary>
        /// Gets the old values list.
        /// </summary>
         [NotMapped]
        public List<string> OldValuesList
        {
            get
            {
                return this.OldValue.Split(new char[] {';' }).ToList();
            }
        }

        /// <summary>
        /// Gets the new values list.
        /// </summary>
         [NotMapped]
        public List<string> NewValuesList
        {
            get
            {
                return this.NewValue.Split(new char[] { ';' }).ToList();
            }
        }

        /// <summary>
        /// Determines whether the specified id has updated.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="table">The table.</param>
        /// <param name="property">The property.</param>
        /// <returns>
        ///   <c>true</c> if the specified id has updated; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasUpdated(object id, string table, string property)
        {
            //todo:refactor
            //if(id != null)
            //{
            //    string stringId = id.ToString();
            //    CTSContext db = new CTSContext();
            //    var count = (from audit in db.Audits
            //                 where audit.TableName == table && audit.PrimaryKey.Equals(stringId) && audit.NewValue.Contains(property)
            //                 select audit).Count();
            //    return (count > 0);    
            //}
            return false;
        }

        /// <summary>
        /// Determines whether the specified id has updated.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="table">The table.</param>
        /// <returns>
        ///   <c>true</c> if the specified id has updated; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasUpdated(int id, string table)
        {
            //todo:refactor
            //CTSContext db = new CTSContext();
            //var count = (from audit in db.Audits
            //             where audit.TableName == table && audit.PrimaryKey == id.ToString()
            //             select audit).Count();
            //return (count > 0);
            return false;
        }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="table">The table.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static List<FieldChange> GetChanges(int id, string table, string property)
        {
            //todo:refactor
            //string key = id.ToString();
            //CTSContext db = new CTSContext();
            //var changes = (from audit in db.Audits
            //               where audit.TableName == table && audit.PrimaryKey == key && audit.NewValue.Contains(property)
            //              orderby audit.DateTime descending
            //              select audit);

            //List<FieldChange> filedsList = new List<FieldChange>();
            //foreach (Audit a in changes)
            //{
            //    filedsList.Add(new FieldChange(a,property));
            //}
            //return filedsList;
            return null;
        }

        //public static List<FieldChange> GetChanges(int id, string table, string property, string foreignTable, string foreignFeildName, string foreignFeildKey)
        //{
        //    string key = id.ToString();
        //    return GetChanges(table, property, foreignTable, foreignFeildName, foreignFeildKey, key);
        //}

        
    }


    /// <summary>
    /// 
    /// </summary>
    public class FieldChange
    {
        //private Audit a;
        //private string property;
        //private string foreignTable;
        //private string foreignFeildName;

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public string FieldName { get; set; }
        /// <summary>
        /// Gets or sets the previous value.
        /// </summary>
        /// <value>
        /// The previous value.
        /// </value>
        public string PreviousValue { get; set; }
        /// <summary>
        /// Gets or sets the changed value.
        /// </summary>
        /// <value>
        /// The changed value.
        /// </value>
        public string ChangedValue { get; set; }
        /// <summary>
        /// Gets or sets the change date.
        /// </summary>
        /// <value>
        /// The change date.
        /// </value>
        public DateTime ChangeDate { get; set; }
        /// <summary>
        /// Gets or sets the changed by.
        /// </summary>
        /// <value>
        /// The changed by.
        /// </value>
        public string ChangedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldChange"/> class.
        /// </summary>
        public FieldChange()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldChange"/> class.
        /// </summary>
        /// <param name="audit">The audit.</param>
        /// <param name="proprtyName">Name of the proprty.</param>
        public FieldChange(Audit audit, string proprtyName)
        {
          //  this.ChangeDate = audit.DateTime;
            UserProfile user = UserProfile.GetUserById(audit.LoginID);
            this.ChangedBy = (user != null) ? user.UserName : "Anonymous";
            this.FieldName = proprtyName;

            this.PreviousValue = audit.OldValue;
            this.ChangedValue = audit.NewValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldChange"/> class.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="property">The property.</param>
        /// <param name="foreignTable">The foreign table.</param>
        /// <param name="foreignFeildName">Name of the foreign feild.</param>
        /// <param name="foreignFeildKey">The foreign feild key.</param>
        public FieldChange(Audit a, string property, string foreignTable, string foreignFeildName, string foreignFeildKey)
        {
            //FieldChange x = new FieldChange(a, property);
          //todo:refactor  CTSContext db = new CTSContext();
            
         //   this.ChangeDate = a.DateTime;
            UserProfile user = UserProfile.GetUserById(a.LoginID);
            this.ChangedBy = (user != null) ? user.UserName : "Anonymous";
            this.FieldName = property;
            
            this.PreviousValue = a.OldValue;
            this.ChangedValue = a.NewValue;
            
            this.FieldName = property;

            var prevKey = Convert.ToInt32(this.PreviousValue);
            var CurrentKey = Convert.ToInt32(this.ChangedValue);
            //modified Banty:24/5/2013 from db.ExecuteStoreQuery to (db as IObjectContextAdapter).ObjectContext.ExecuteStoreQuery
          //todo:  var Prev = (db as IObjectContextAdapter).ObjectContext.ExecuteStoreQuery<string>(" SELECT " + foreignFeildName + " as field FROM " + foreignTable + " WHERE " + foreignFeildKey + " = " + prevKey).FirstOrDefault();
           //todo: var now = (db as IObjectContextAdapter).ObjectContext.ExecuteStoreQuery<string>(" SELECT " + foreignFeildName + " as field FROM " + foreignTable + " WHERE " + foreignFeildKey + " = " + CurrentKey).FirstOrDefault(); ;
         // var Prev = db.AuditForeignFeild(foreignTable,foreignFeildName,prevKey,foreignFeildKey).SingleOrDefault();
         // var now = db.AuditForeignFeild(foreignTable, foreignFeildName, CurrentKey, foreignFeildKey).SingleOrDefault();

            //todo:if(Prev != null)
            //{
            //    this.PreviousValue = Prev.ToString();
            //    //this.PreviousValue = Prev.field;
            //}
            //if(now != null)
            //{
            //    this.ChangedValue = now.ToString();
            //}


        }
    }
}
