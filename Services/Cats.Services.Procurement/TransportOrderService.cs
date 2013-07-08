

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;



namespace Cats.Services.Procurement
{

    public class TransportOrderService:ITransportOrderService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public TransportOrderService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddTransportOrder(TransportOrder transportOrder)
       {
           _unitOfWork.TransportOrderRepository.Add(transportOrder);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditTransportOrder(TransportOrder transportOrder)
       {
           _unitOfWork.TransportOrderRepository.Edit(transportOrder);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteTransportOrder(TransportOrder transportOrder)
        {
             if(transportOrder==null) return false;
           _unitOfWork.TransportOrderRepository.Delete(transportOrder);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.TransportOrderRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.TransportOrderRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<TransportOrder> GetAllTransportOrder()
       {
           return _unitOfWork.TransportOrderRepository.GetAll();
       } 
       public TransportOrder FindById(int id)
       {
           return _unitOfWork.TransportOrderRepository.FindById(id);
       }
       public List<TransportOrder> FindBy(Expression<Func<TransportOrder, bool>> predicate)
       {
           return _unitOfWork.TransportOrderRepository.FindBy(predicate);
       }
       
       public IEnumerable<TransportOrder> Get(
           Expression<Func<TransportOrder, bool>> filter = null,
           Func<IQueryable<TransportOrder>, IOrderedQueryable<TransportOrder>> orderBy = null,
           string includeProperties = "")
        {
           return _unitOfWork.TransportOrderRepository.Get(filter, orderBy, includeProperties);
        }
       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }



       public IEnumerable<Models.ViewModels.RequisitionToDispatch> GetRequisitionToDispatch()
       {
           throw new NotImplementedException();
       }

       public IEnumerable<ReliefRequisition> GetProjectCodeAssignedRequisitions()
       {
           throw new NotImplementedException();
       }
   }
   }
   
         
      