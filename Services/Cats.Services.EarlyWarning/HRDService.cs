using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models.ViewModels.HRD;
using Cats.Services.EarlyWarning;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public class HRDService : IHRDService
    {
        private readonly IUnitOfWork _unitOfWork;
         public HRDService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddHRD(HRD hrd)
        {
            _unitOfWork.HRDRepository.Add(hrd);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteHRD(HRD hrd)
        {

            if (hrd == null) return false;
            _unitOfWork.HRDRepository.Delete(hrd);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HRDRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HRDRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditHRD(HRD hrd)
        {
            _unitOfWork.HRDRepository.Edit(hrd);
            _unitOfWork.Save();
            return true;
        }

        public HRD FindById(int id)
        {
            return _unitOfWork.HRDRepository.FindById(id);
        }

        public List<HRD> GetAllHRD()
        {
            return _unitOfWork.HRDRepository.GetAll();
        }

        public List<HRD> FindBy(System.Linq.Expressions.Expression<Func<HRD, bool>> predicate)
        {
            return _unitOfWork.HRDRepository.FindBy(predicate);
        }

        public IEnumerable<HRDDetail> GetHRDDetailByHRDID(int hrdID)
        {
            return _unitOfWork.HRDDetailRepository.Get(t => t.HRDID == hrdID);
        }
        public IEnumerable<HRD> Get(System.Linq.Expressions.Expression<Func<HRD, bool>> filter = null,
                                    Func<IQueryable<HRD>, IOrderedQueryable<HRD>> orderBy = null,
                                    string includeProperties = "")
        {
            return _unitOfWork.HRDRepository.Get(filter, orderBy, includeProperties);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}

