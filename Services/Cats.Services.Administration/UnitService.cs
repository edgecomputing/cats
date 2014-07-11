using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Administration
{
   public class UnitService:IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;


        public UnitService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddUnit(Unit unit)
        {
            _unitOfWork.UnitRepository.Add(unit);
            _unitOfWork.Save();
            return true;

        }
        public bool EditUnit(Unit unit)
        {
            _unitOfWork.UnitRepository.Edit(unit);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteUnit(Unit unit)
        {
            if (unit == null) return false;
            _unitOfWork.UnitRepository.Delete(unit);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UnitRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UnitRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Unit> GetAllUnit()
        {
            return _unitOfWork.UnitRepository.GetAll();
        }
        public Unit FindById(int id)
        {
            return _unitOfWork.UnitRepository.FindById(id);
        }
        public List<Unit> FindBy(Expression<Func<Unit, bool>> predicate)
        {
            return _unitOfWork.UnitRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}
