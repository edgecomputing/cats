using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Finance
{
    public class TransporterChequeDetailService : ITransporterChequeDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransporterChequeDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public bool AddTransporterChequeDetail(TransporterChequeDetail transporterChequeDetail)
        {
            _unitOfWork.TransporterChequeDetailRepository.Add(transporterChequeDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteTransporterChequeDetail(TransporterChequeDetail transporterChequeDetail)
        {
            if (transporterChequeDetail == null) return false;
            _unitOfWork.TransporterChequeDetailRepository.Delete(transporterChequeDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TransporterChequeDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TransporterChequeDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditTransporterChequeDetail(TransporterChequeDetail transporterChequeDetail)
        {
            _unitOfWork.TransporterChequeDetailRepository.Edit(transporterChequeDetail);
            _unitOfWork.Save();
            return true;
        }

        public TransporterChequeDetail FindById(int id)
        {
            return _unitOfWork.TransporterChequeDetailRepository.FindById(id);
        }

        public List<TransporterChequeDetail> GetAllTransporterChequeDetail()
        {
            return _unitOfWork.TransporterChequeDetailRepository.GetAll();
        }

        public List<TransporterChequeDetail> FindBy(Expression<Func<TransporterChequeDetail, bool>> predicate)
        {
            return _unitOfWork.TransporterChequeDetailRepository.FindBy(predicate);
        }

        public IEnumerable<TransporterChequeDetail> Get(Expression<Func<TransporterChequeDetail, bool>> filter = null, Func<IQueryable<TransporterChequeDetail>, IOrderedQueryable<TransporterChequeDetail>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.TransporterChequeDetailRepository.Get(filter, orderBy, includeProperties);
        }
    }
}
