using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public class ProjectCodeService : IProjectCodeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectCodeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddProjectCode(ProjectCode projectCode)
        {
            _unitOfWork.ProjectCodeRepository.Add(projectCode);
            _unitOfWork.Save();
            return true;

        }
        public bool EditProjectCode(ProjectCode projectCode)
        {
            _unitOfWork.ProjectCodeRepository.Edit(projectCode);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteProjectCode(ProjectCode projectCode)
        {
            if (projectCode == null) return false;
            _unitOfWork.ProjectCodeRepository.Delete(projectCode);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ProjectCodeRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ProjectCodeRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<ProjectCode> GetAllProjectCode()
        {
            return _unitOfWork.ProjectCodeRepository.GetAll();
        }
        public ProjectCode FindById(int id)
        {
            return _unitOfWork.ProjectCodeRepository.FindById(id);
        }
        public List<ProjectCode> FindBy(Expression<Func<ProjectCode, bool>> predicate)
        {
            return _unitOfWork.ProjectCodeRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



        
    }
}
