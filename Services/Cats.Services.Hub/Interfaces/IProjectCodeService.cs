
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Common;


namespace Cats.Services.Hub
{
    public interface IProjectCodeService:IDisposable
    {

        bool AddProjectCode(ProjectCode projectCode);
        bool DeleteProjectCode(ProjectCode projectCode);
        bool DeleteById(int id);
        bool EditProjectCode(ProjectCode projectCode);
        ProjectCode FindById(int id);
        List<ProjectCode> GetAllProjectCode();
        List<ProjectCode> FindBy(Expression<Func<ProjectCode, bool>> predicate);

        //Gets the project code id.
        /// </summary>
        /// <param name="projectCode">The project code.</param>
        /// <returns></returns>
        int GetProjectCodeId(string projectCode);
         /// <summary>
        /// Gets the project code id W ith create.
        /// </summary>
        /// <param name="projectNumber">The project number.</param>
        /// <returns></returns>
        ProjectCode GetProjectCodeIdWIthCreate(string projectNumber);

        /// <summary>
        /// Gets all the project code in ProejctCodeViewModel 
        /// </summary>
        /// <returns></returns>
        List<ProjectCodeViewModel> GetAllProjectCodeForReport();

        List<ProjectCodeViewModel> GetProjectCodesForCommodity(int hubID, int parentCommodityId);

        string GetProjectCodeValueByProjectCodeId(int ProjectCodeID);
    }
}


