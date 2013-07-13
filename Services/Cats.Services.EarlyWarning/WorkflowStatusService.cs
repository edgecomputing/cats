using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
    public class WorkflowStatusService : IWorkflowStatusService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkflowStatusService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public string GetStatusName(Models.Constant.Workflow workflow, int statusId)
        {
            var workflowStatus =
                _unitOfWork.WorkflowStatusRepository.Get(t => t.WorkflowID == (int)workflow && t.StatusID == statusId).
                    FirstOrDefault();
            return workflowStatus != null ? workflowStatus.Description :
             string.Empty;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }


        public List<Models.WorkflowStatus> GetStatus(Models.Constant.Workflow workflow)
        {
          return   _unitOfWork.WorkflowStatusRepository.Get(t => t.WorkflowID == (int) workflow).ToList();
        }
    }
}
