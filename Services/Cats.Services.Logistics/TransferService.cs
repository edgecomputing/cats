using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;

namespace Cats.Services.Logistics
{

    public class TransferService:ITransferService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public TransferService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddTransfer(Transfer transfer)
       {
           _unitOfWork.TransferRepository.Add(transfer);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditTransfer(Transfer transfer)
       {
           _unitOfWork.TransferRepository.Edit(transfer);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteTransfer(Transfer transfer)
        {
             if(transfer==null) return false;
           _unitOfWork.TransferRepository.Delete(transfer);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.TransferRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.TransferRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<Transfer> GetAllTransfer()
       {
           return _unitOfWork.TransferRepository.GetAll();
       } 
       public Transfer FindById(int id)
       {
           return _unitOfWork.TransferRepository.FindById(id);
       }
       public List<Transfer> FindBy(Expression<Func<Transfer, bool>> predicate)
       {
           return _unitOfWork.TransferRepository.FindBy(predicate);
       }
       #endregion
       public bool Approve (Transfer transfer)
       {
               if (transfer!=null)
               {
                   transfer.StatusID = (int) LocalPurchaseStatus.Approved;
                   _unitOfWork.TransferRepository.Edit(transfer);
                   var reciptAllocaltion = new ReceiptAllocation()
                   {
                       ReceiptAllocationID = Guid.NewGuid(),
                       ProgramID = transfer.ProgramID,
                       CommodityID = transfer.CommodityID,
                       ETA = transfer.CreatedDate,
                       SINumber = transfer.ShippingInstruction.Value,
                       QuantityInMT = transfer.Quantity,
                       HubID = transfer.DestinationHubID,
                       CommoditySourceID = transfer.CommoditySourceID, 
                       ProjectNumber = transfer.ProjectCode,
                       SourceHubID = transfer.SourceHubID, 
                       PartitionID = 0,
                       IsCommited = false
                   };
                   _unitOfWork.ReceiptAllocationReository.Add(reciptAllocaltion);
                   _unitOfWork.Save();
                   return true;

               }
           return false;
       }
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
         
      