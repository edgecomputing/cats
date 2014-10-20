using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Transaction;


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
                   relifRequisition.Status = (int) ReliefRequisitionStatus.ProjectCodeAssigned;
                   _unitOfWork.Save();
                   if (!PostSIAllocation(relifRequisition.RequisitionID))
                   {
                       return false;
                   }
                   return true;

               }
           }
           return false;
       }
       private bool PostSIAllocation(int requisitionID)
       {
           var allocationDetails = _unitOfWork.SIPCAllocationRepository.Get(t => t.ReliefRequisitionDetail.RequisitionID == requisitionID);
           if (allocationDetails == null) return false;

           var transactionGroup = Guid.NewGuid();
           var transactionDate = DateTime.Now;
           _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup { PartitionID = 0, TransactionGroupID = transactionGroup });

           //ProjectCodeID	ShippingInstructionID ProgramID QuantityInMT	QuantityInUnit	UnitID	TransactionDate	RegionID	Month	Round	DonorID	CommoditySourceID	GiftTypeID	FDP

           foreach (var allocationDetail in allocationDetails)
           {
               var transaction = new Models.Transaction
               {
                   TransactionID = Guid.NewGuid(),
                   TransactionGroupID = transactionGroup,
                   TransactionDate = transactionDate,
                   UnitID = 1
               };

               var allocation = allocationDetail;




               transaction.QuantityInMT = -allocationDetail.AllocatedAmount;
               transaction.QuantityInUnit = -allocationDetail.AllocatedAmount;
               transaction.LedgerID = Ledger.Constants.COMMITED_TO_FDP;
               transaction.CommodityID = allocationDetail.ReliefRequisitionDetail.CommodityID;
               transaction.FDPID = allocationDetail.ReliefRequisitionDetail.FDPID;
               transaction.ProgramID = (int)allocationDetail.ReliefRequisitionDetail.ReliefRequisition.ProgramID;
               transaction.RegionID = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionID;
               transaction.PlanId = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionalRequest.PlanID;
               transaction.Round = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.Round;

               int hubID1 = 0;
               if (allocationDetail.AllocationType == TransactionConstants.Constants.SHIPPNG_INSTRUCTION)
               {
                   transaction.ShippingInstructionID = allocationDetail.Code;
                   hubID1 = (int)_unitOfWork.TransactionRepository.FindBy(m => m.ShippingInstructionID == allocationDetail.Code && m.LedgerID == Ledger.Constants.GOODS_ON_HAND).Select(m => m.HubID).FirstOrDefault();
               }
               else
               {
                   transaction.ProjectCodeID = allocationDetail.Code;
                   hubID1 = (int)_unitOfWork.TransactionRepository.FindBy(m => m.ProjectCodeID == allocationDetail.Code && m.LedgerID == Ledger.Constants.GOODS_ON_HAND).Select(m => m.HubID).FirstOrDefault();

               }

               // I see some logical error here
               // what happens when hub x was selected and the allocation was made from hub y? 
               //TOFIX: 
               // Hub is required for this transaction
               // Try catch is danger!! Either throw the exception or use conditional statement. 

               if (hubID1 != 0)
               {
                   transaction.HubID = hubID1;
               }
               else
               {
                   transaction.HubID =
                                     _unitOfWork.HubAllocationRepository.FindBy(r => r.RequisitionID == allocation.ReliefRequisitionDetail.RequisitionID).Select(
                                             h => h.HubID).FirstOrDefault();
               }




               _unitOfWork.TransactionRepository.Add(transaction);
               // result.Add(transaction);

               /*post Debit-Pledged To FDP*/
               var transaction2 = new Models.Transaction
               {
                   TransactionID = Guid.NewGuid(),
                   TransactionGroupID = transactionGroup,
                   TransactionDate = transactionDate,
                   UnitID = 1
               };



               transaction2.QuantityInMT = allocationDetail.AllocatedAmount;
               transaction2.QuantityInUnit = allocationDetail.AllocatedAmount;
               transaction2.LedgerID = Ledger.Constants.PLEDGED_TO_FDP;
               transaction2.CommodityID = allocationDetail.ReliefRequisitionDetail.CommodityID;
               transaction2.FDPID = allocationDetail.ReliefRequisitionDetail.FDPID;
               transaction2.ProgramID = (int)allocationDetail.ReliefRequisitionDetail.ReliefRequisition.ProgramID;
               transaction2.RegionID = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionID;
               transaction2.PlanId = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionalRequest.PlanID;
               transaction2.Round = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.Round;

               int hubID2 = 0;
               if (allocationDetail.AllocationType == TransactionConstants.Constants.SHIPPNG_INSTRUCTION)
               {
                   var siCode = allocationDetail.Code.ToString();
                   var shippingInstruction =
                       _unitOfWork.ShippingInstructionRepository.Get(t => t.Value == siCode).
                           FirstOrDefault();
                   if (shippingInstruction != null) transaction.ShippingInstructionID = shippingInstruction.ShippingInstructionID;

                   hubID2 = (int)_unitOfWork.TransactionRepository.FindBy(m => m.ShippingInstructionID == allocationDetail.Code &&
                          m.LedgerID == Ledger.Constants.GOODS_ON_HAND).Select(m => m.HubID).FirstOrDefault();


               }
               else
               {
                   var detail = allocationDetail;
                   var code = detail.Code.ToString();
                   var projectCode =
                       _unitOfWork.ProjectCodeRepository.Get(t => t.Value == code).
                           FirstOrDefault();
                   if (projectCode != null) transaction.ProjectCodeID = projectCode.ProjectCodeID;

                   hubID2 = (int)_unitOfWork.TransactionRepository.FindBy(m => m.ProjectCodeID == allocationDetail.Code &&
                              m.LedgerID == Ledger.Constants.GOODS_ON_HAND).Select(m => m.HubID).FirstOrDefault();

               }


               if (hubID2 != 0)
               {
                   transaction2.HubID = hubID2;
               }

               else
               {
                   transaction2.HubID =
                                      _unitOfWork.HubAllocationRepository.FindBy(r => r.RequisitionID == allocation.ReliefRequisitionDetail.RequisitionID).Select(
                                              h => h.HubID).FirstOrDefault();

               }

               _unitOfWork.TransactionRepository.Add(transaction2);
               allocationDetail.TransactionGroupID = transactionGroup;
               _unitOfWork.SIPCAllocationRepository.Edit(allocationDetail);
               //result.Add(transaction);
           }
           var requisition = _unitOfWork.ReliefRequisitionRepository.FindById(requisitionID);
           requisition.Status = 4;
           _unitOfWork.ReliefRequisitionRepository.Edit(requisition);
           _unitOfWork.Save();
           //return result;
           return true;
       }
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }



      
   }
   }
   
         
      