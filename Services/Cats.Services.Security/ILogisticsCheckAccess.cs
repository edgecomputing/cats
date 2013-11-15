using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Security;
using NetSqlAzMan.Interfaces;

namespace Cats.Services.Security
{
    public interface ILogisticsCheckAccess
    {
       
            IAzManStorage Storage { get; }

            bool CheckAccess(LogisticsCheckAccess.Role role, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Role role, string dbUserName,
                         params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Role role, NetSqlAzMan.Interfaces.IAzManSid customSID,
                         params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Role role,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Role role, string dbUserName,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Role role, NetSqlAzMan.Interfaces.IAzManSid customSID,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Task task, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Task task, string dbUserName, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Task task, NetSqlAzMan.Interfaces.IAzManSid customSID,
                         params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Task task,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Task task, string dbUserName,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(LogisticsCheckAccess.Task task, NetSqlAzMan.Interfaces.IAzManSid customSID,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(LogisticsCheckAccess.Operation operation, NetSqlAzMan.Interfaces.IAzManSid customSID,
                             out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                                 attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(LogisticsCheckAccess.Operation operation, string dbUserName,
                             out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                                 attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(LogisticsCheckAccess.Operation operation,
                             out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                                 attributes, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(LogisticsCheckAccess.Operation operation, NetSqlAzMan.Interfaces.IAzManSid customSID,
                             params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(LogisticsCheckAccess.Operation operation, string dbUserName,
                             params KeyValuePair<string, object>[] contextParameters);


            bool CheckAccess(LogisticsCheckAccess.Operation operation, params KeyValuePair<string, object>[] contextParameters);

            bool CheckAccess(LogisticsCheckAccess.Operation hubAllocation, IAzManSid dbUserName);
    }
    }
