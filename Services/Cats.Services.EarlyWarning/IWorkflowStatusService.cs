using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Workflow = Cats.Models.Constant.WORKFLOW;

namespace Cats.Services.EarlyWarning
{
    public interface IWorkflowStatusService:IDisposable
    {
        string GetStatusName(Workflow workflow, int statusId);
        List<WorkflowStatus> GetStatus(Workflow workflow);
    }
}
