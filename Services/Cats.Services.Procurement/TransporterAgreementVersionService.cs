using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public class TransporterAgreementVersionService : ITransporterAgreementVersionService
    {

        private readonly IUnitOfWork _unitOfWork;

        public TransporterAgreementVersionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Implementation of ITransporterAgreementVersionService

        public bool AddTransporterAgreementVersion(TransporterAgreementVersion transporterAgreementVersion)
        {
            _unitOfWork.TransporterAgreementVersionRepository.Add(transporterAgreementVersion);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteTransporterAgreementVersion(TransporterAgreementVersion transporterAgreementVersion)
        {
            if (transporterAgreementVersion == null) return false;
            _unitOfWork.TransporterAgreementVersionRepository.Delete(transporterAgreementVersion);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TransporterAgreementVersionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TransporterAgreementVersionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditTransporterAgreementVersion(TransporterAgreementVersion transporterAgreementVersion)
        {
            _unitOfWork.TransporterAgreementVersionRepository.Edit(transporterAgreementVersion);
            _unitOfWork.Save();
            return true;
        }

        public TransporterAgreementVersion FindById(int id)
        {
            return _unitOfWork.TransporterAgreementVersionRepository.FindById(id);
        }

        public List<TransporterAgreementVersion> GetAllTransporterAgreementVersion()
        {
            return _unitOfWork.TransporterAgreementVersionRepository.GetAll();
        }

        public List<TransporterAgreementVersion> FindBy(Expression<Func<TransporterAgreementVersion, bool>> predicate)
        {
            return _unitOfWork.TransporterAgreementVersionRepository.FindBy(predicate);
        }

        public IEnumerable<TransporterAgreementVersion> Get(System.Linq.Expressions.Expression<Func<TransporterAgreementVersion, bool>> filter = null,
                                    Func<IQueryable<TransporterAgreementVersion>, IOrderedQueryable<TransporterAgreementVersion>> orderBy = null,
                                    string includeProperties = "")
        {
            return _unitOfWork.TransporterAgreementVersionRepository.Get(filter, orderBy, includeProperties);
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}
