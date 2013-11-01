using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSqlAzMan.Interfaces;

namespace Cats.Services.Security
{
    public interface IProcurementCheckAccess
    {
     
    

            IAzManStorage Storage { get; }

            // protected NetSqlAzMan.Interfaces.AuthorizationType CheckAccess(string itemName, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Operation operation, NetSqlAzMan.Interfaces.IAzManSid customSID,
                             out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                                 attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Operation operation, string dbUserName,
                             out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                                 attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Operation operation,
                             out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                                 attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Operation operation, NetSqlAzMan.Interfaces.IAzManSid customSID,
                             params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Operation operation, string dbUserName,
                             params KeyValuePair<string, object>[] contextParameters);


            bool CheckAccess(ProcurementCheckAccess.Operation operation, params KeyValuePair<string, object>[] contextParameters);

        }
    }

