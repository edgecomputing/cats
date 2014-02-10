
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;

namespace Cats.Services.Logistics
{

    public class LocalPurchaseService:ILocalPurchaseService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public LocalPurchaseService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddLocalPurchase(LocalPurchase localPurchase)
       {
           _unitOfWork.LocalPurchaseRepository.Add(localPurchase);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditLocalPurchase(LocalPurchase localPurchase)
       {
           _unitOfWork.LocalPurchaseRepository.Edit(localPurchase);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteLocalPurchase(LocalPurchase localPurchase)
        {
             if(localPurchase==null) return false;
           _unitOfWork.LocalPurchaseRepository.Delete(localPurchase);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var localPurchase = _unitOfWork.LocalPurchaseRepository.FindById(id);
           if(localPurchase==null) return false;
           _unitOfWork.LocalPurchaseRepository.Delete(localPurchase);
           _unitOfWork.Save();
           return true;
       }
       public List<LocalPurchase> GetAllLocalPurchase()
       {
           return _unitOfWork.LocalPurchaseRepository.GetAll();
       } 
       public LocalPurchase FindById(int id)
       {
           return _unitOfWork.LocalPurchaseRepository.FindById(id);
       }
       public List<LocalPurchase> FindBy(Expression<Func<LocalPurchase, bool>> predicate)
       {
           return _unitOfWork.LocalPurchaseRepository.FindBy(predicate);
       }
       #endregion
       

       public List<Models.Hub> GetAllHub()
       {
           return _unitOfWork.HubRepository.GetAll();
       }
       

       public void Dispose()
       {
           _unitOfWork.Dispose();

       }


       public bool Approve(LocalPurchase localPurchase)
       {
           try
           {
               localPurchase.StatusID = (int) LocalPurchaseStatus.Approved;
               _unitOfWork.LocalPurchaseRepository.Edit(localPurchase);
               foreach (var localPurchaseDetail in localPurchase.LocalPurchaseDetails)
               {
                   var reciptAllocaltion = new ReceiptAllocation()
                       {
                           ReceiptAllocationID = Guid.NewGuid(),
                           ProgramID = localPurchase.ProgramID,
                           CommodityID = localPurchase.CommodityID,
                           DonorID = localPurchase.DonorID,
                           ETA = localPurchase.DateCreated,
                           SINumber = localPurchase.ShippingInstruction.Value,
                           QuantityInMT = localPurchaseDetail.AllocatedAmount,
                           HubID = localPurchaseDetail.HubID,
                           CommoditySourceID = 3, //Local Purchase
                           ProjectNumber = localPurchase.ProjectCode,
                           //PurchaseOrder = localPurchase.PurchaseOrder, 
                           PartitionID = 0,
                           IsCommited = false
                       };
                   _unitOfWork.ReceiptAllocationReository.Add(reciptAllocaltion);
                   _unitOfWork.Save();
               }
               return true;
           }
           catch (Exception)
           {

               return false;
           }
       }
   }
   }
   
         
      