using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSqlAzMan.Interfaces;
using Cats.Services.Security;
namespace Cats.Services.Security
{
    public interface IEarlyWarningCheckAccess
    {
        IAzManStorage Storage { get; }

        // protected NetSqlAzMan.Interfaces.AuthorizationType CheckAccess(string itemName, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters);
        bool CheckAccess(EarlyWarningCheckAccess.Operation operation, NetSqlAzMan.Interfaces.IAzManSid customSID,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(EarlyWarningCheckAccess.Operation operation, string dbUserName,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(EarlyWarningCheckAccess.Operation operation,
                         out System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
                             attributes, params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(EarlyWarningCheckAccess.Operation operation, NetSqlAzMan.Interfaces.IAzManSid customSID,
                         params KeyValuePair<string, object>[] contextParameters);

        bool CheckAccess(EarlyWarningCheckAccess.Operation operation, string dbUserName,
                         params KeyValuePair<string, object>[] contextParameters);


        bool CheckAccess(EarlyWarningCheckAccess.Operation operation, params KeyValuePair<string, object>[] contextParameters);

    }
}