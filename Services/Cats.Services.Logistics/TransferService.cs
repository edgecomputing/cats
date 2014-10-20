using System;
using System.Collections.Generic;
using System.Linq;
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
                       PartitionId = 0,
                       IsCommited = false
                   };
                   _unitOfWork.ReceiptAllocationReository.Add(reciptAllocaltion);
                   _unitOfWork.Save();
                   return true;

               }
           return false;
       }
       public bool CreateRequisitonForTransfer(Transfer transfer)
       {
           if (transfer != null)
           {
               var fdp = _unitOfWork.FDPRepository.FindBy(m => m.HubID == transfer.DestinationHubID).FirstOrDefault();
               if (fdp != null)
               {

                   var relifRequisition = new ReliefRequisition()
                       {

                           //RegionalRequestID = regionalRequest.RegionalRequestID,
                           //Round = regionalRequest.Round,
                           Month = transfer.CreatedDate.Month,
                           ProgramID = transfer.ProgramID,
                           CommodityID = transfer.CommodityID,
                           RequestedDate = transfer.CreatedDate,
                           //RationID = regionalRequest.RationID

                          
                           RequisitionNo = Guid.NewGuid().ToString(),
                           RegionID = fdp.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID,
                           ZoneID = fdp.AdminUnit.AdminUnit2.AdminUnitID,
                           Status = (int) ReliefRequisitionStatus.Draft,
                          

                       };
                   _unitOfWork.ReliefRequisitionRepository.Add(relifRequisition);

                   var relifRequistionDetail = new ReliefRequisitionDetail();

                   relifRequistionDetail.DonorID = 1;
                  
                   relifRequistionDetail.FDPID = fdp.FDPID;
                   
                   relifRequistionDetail.BenficiaryNo = 0; //since there is no need of benficiaryNo on transfer
                   relifRequistionDetail.CommodityID = transfer.CommodityID;
                   relifRequistionDetail.Amount = transfer.Quantity;
                   relifRequisition.ReliefRequisitionDetails.Add(relifRequistionDetail);

                   // save hub allocation
                   var hubAllocation = new HubAllocation
                       {
                           AllocatedBy = 1,
                           RequisitionID = relifRequisition.RequisitionID,
                           AllocationDate = transfer.CreatedDate,
                           ReferenceNo = "001",
                           HubID = transfer.SourceHubID
                       };
                   _unitOfWork.HubAllocationRepository.Add(hubAllocation);
                   relifRequisition.Status = (int)ReliefRequisitionStatus.HubAssigned;
                   _unitOfWork.Save();


                   SIPCAllocation allocation = new SIPCAllocation
                   {
                       Code = transfer.ShippingInstructionID,
                       AllocatedAmount = transfer.Quantity,
                       AllocationType = "SI",
                       RequisitionDetailID = relifRequistionDetail.RequisitionDetailID
                   };
                   _unitOfWork.SIPCAllocationRepository.Add(allocation);
                   relifRequisition.RequisitionNo = String.Format("REQ-{0}", relifRequisition.RequisitionID);
                   relifRequisition.Status = (int) ReliefRequisitionStatus.SiPcAllocationApproved;
                   _unitOfWork.Save();

                   return true;

               }
           }
           return false;
       }
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }



      
   }
   }
   
         
      