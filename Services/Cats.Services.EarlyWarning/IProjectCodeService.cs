using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IProjectCodeService
    {
        bool AddProjectCode(ProjectCode projectCode);
        bool EditProjectCode(ProjectCode projectCode);
        bool DeleteProjectCode(ProjectCode projectCode);
        bool DeleteById(int id);
        List<ProjectCode> GetAllProjectCode();
        ProjectCode FindById(int id);
        List<ProjectCode> FindBy(Expression<Func<ProjectCode, bool>> predicate);
    }
}
