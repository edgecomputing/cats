

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.ViewModels;


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
           var requisitions = GetProjectCodeAssignedRequisitions();

           var result= (from requisition in requisitions
                   select new RequisitionToDispatch
                              {
                                  HubID = requisition.HubAllocations.FirstOrDefault().HubID,
                                  RequisitionID = requisition.RequisitionID,
                                 RequisitionNo = requisition.RequisitionNo,
                                RequisitionStatus = requisition.Status.Value,
                                  ZoneID = requisition.ZoneID.Value,
                                 QuanityInQtl = requisition.ReliefRequisitionDetails.Sum(m => m.Amount),
                              OrignWarehouse = requisition.HubAllocations.FirstOrDefault().Hub.Name,
                                  CommodityID = requisition.CommodityID.Value,
                                 CommodityName = requisition.Commodity.Name,
                                Zone=requisition.AdminUnit.Name,
                                  RegionID=requisition.RegionID.Value ,
                                 RegionName=requisition.AdminUnit1.Name 

                              });


           return result;
       }

       public IEnumerable<ReliefRequisition> GetProjectCodeAssignedRequisitions()
       {
         return   _unitOfWork.ReliefRequisitionRepository.Get(t => t.Status == (int) REGIONAL_REQUEST_STATUS.HubAssigned, null,
                                                       "HubAllocations,HubAllocations.Hub,ReliefRequisitionDetails,Program,AdminUnit1,AdminUnit.AdminUnit2,Commodity");
       }

       public IEnumerable<ReliefRequisitionDetail> GetProjectCodeAssignedRequisitionDetails()
       {
           return _unitOfWork.ReliefRequisitionDetailRepository.Get(t => t.ReliefRequisition.Status == (int)REGIONAL_REQUEST_STATUS.HubAssigned, null,
                                                         "ReliefRequisition");
       }
   }
   }
   
         
      