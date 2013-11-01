using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.Procurement
{
   public class TransReqWithoutTransporterService:ITransReqWithoutTransporterService
   {

       private readonly IUnitOfWork _unitOfWork;

       public TransReqWithoutTransporterService(IUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }
        public bool AddTransReqWithoutTransporter(Models.TransReqWithoutTransporter transReqWithoutTransporter)
        {
            _unitOfWork.TransReqWithoutTransporterRepository.Add(transReqWithoutTransporter);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteTransReqWithoutTransporter(Models.TransReqWithoutTransporter transReqWithoutTransporter)
        {
            if (transReqWithoutTransporter == null) return false;
            _unitOfWork.TransReqWithoutTransporterRepository.Delete(transReqWithoutTransporter);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TransReqWithoutTransporterRepository.FindById(id);
            if (entity == null) return true;
            _unitOfWork.TransReqWithoutTransporterRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditTransReqWithoutTransporter(Models.TransReqWithoutTransporter transReqWithoutTransporter)
        {
            _unitOfWork.TransReqWithoutTransporterRepository.Edit(transReqWithoutTransporter);
            _unitOfWork.Save();
            return true;
        }

        public Models.TransReqWithoutTransporter FindById(int id)
        {
            return _unitOfWork.TransReqWithoutTransporterRepository.FindById(id);
        }

        public List<Models.TransReqWithoutTransporter> GetAllTransReqWithoutTransporter()
        {
            return _unitOfWork.TransReqWithoutTransporterRepository.GetAll();
        }

        public List<Models.TransReqWithoutTransporter> FindBy(System.Linq.Expressions.Expression<Func<Models.TransReqWithoutTransporter, bool>> predicate)
        {
            return _unitOfWork.TransReqWithoutTransporterRepository.FindBy(predicate);
        }

        public IEnumerable<Models.TransReqWithoutTransporter> Get(System.Linq.Expressions.Expression<Func<Models.TransReqWithoutTransporter, bool>> filter = null, Func<IQueryable<Models.TransReqWithoutTransporter>, IOrderedQueryable<Models.TransReqWithoutTransporter>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.TransReqWithoutTransporterRepository.Get(filter, orderBy, includeProperties);
        }

        public bool Save()
        {
            _unitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
