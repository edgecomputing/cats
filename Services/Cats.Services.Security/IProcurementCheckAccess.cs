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

            bool CheckAccess(ProcurementCheckAccess.Role role, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Role role, string dbUserName, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Role role, NetSqlAzMan.Interfaces.IAzManSid customSID,
                         params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Role role,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Role role, string dbUserName,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Role role, NetSqlAzMan.Interfaces.IAzManSid customSID,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Task task, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Task task, string dbUserName, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Task task, NetSqlAzMan.Interfaces.IAzManSid customSID,
                         params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Task task,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Task task, string dbUserName,
                                        out
                                            System.Collections.Generic.List
                                            <System.Collections.Generic.KeyValuePair<string, string>> attributes,
                                        params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(ProcurementCheckAccess.Task task, NetSqlAzMan.Interfaces.IAzManSid customSID,
                                        out
                                            System.Collections.Generic.List
                                            <System.Collections.Generic.KeyValuePair<string, string>> attributes,
                                        params KeyValuePair<string, object>[] contextParameters);

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

