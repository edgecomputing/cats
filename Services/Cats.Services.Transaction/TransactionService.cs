using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Data.Hub;
using Cats.Models;

namespace Cats.Services.Transaction
{
   
    public class TransactionService : ITransactionService
    {
        private readonly Cats.Data.UnitWork.IUnitOfWork _unitOfWork;

        public TransactionService(Cats.Data.UnitWork.IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region generic methods
        public bool AddTransaction(Models.Transaction item)
        {
            _unitOfWork.TransactionRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateTransaction(Models.Transaction item)
        {
            if (item == null) return false;
            _unitOfWork.TransactionRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteTransaction(Models.Transaction item)
        {
            if (item == null) return false;
            _unitOfWork.TransactionRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(Guid id)
        {
            var item = _unitOfWork.TransactionRepository.FindById(id);
            return DeleteTransaction(item);
        }
        public Models.Transaction FindById(Guid id)
        {
            return _unitOfWork.TransactionRepository.FindById(id);
        }
        public List<Models.Transaction> GetAllTransaction()
        {
            return _unitOfWork.TransactionRepository.GetAll();

        }
        public List<Models.Transaction> FindBy(Expression<Func<Models.Transaction, bool>> predicate)
        {
            return _unitOfWork.TransactionRepository.FindBy(predicate);

        }
#endregion
        
        public IEnumerable<Models.Transaction> PostTransaction(IEnumerable<Models.Transaction> entries)
        {
            Guid transactionGroupID = Guid.NewGuid();
            DateTime TransactionDate = DateTime.Now;
            _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup
                                                           {PartitionID = 0, TransactionGroupID = transactionGroupID});
            foreach (Models.Transaction entry in entries)
            {
                entry.TransactionDate = TransactionDate;
                entry.TransactionGroupID = transactionGroupID;
                entry.TransactionID = Guid.NewGuid();
                _unitOfWork.TransactionRepository.Add(entry);
                
            }
            _unitOfWork.Save();
            return entries;
        }

#region HRD / PSNP Post
        public List<Models.Transaction> PostHRDPlan(HRD hrd, Ration ration)
        {
            List<Models.Transaction> entries = new List<Models.Transaction>();
            List<int> regionalBenCount = new List<int>();
            ration = hrd.Ration;
            int RegionID=0;
            
            for (int r = 0; r < 12; r++)
            {
                regionalBenCount.Add(0);
            }
            foreach (HRDDetail fdp in hrd.HRDDetails)
            {
                for (int r = 0; r < fdp.DurationOfAssistance; r++)
                {
                    regionalBenCount[r] += (int)fdp.NumberOfBeneficiaries;
                    RegionID = fdp.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID;
                }
            }
            for (int r = 0; r < 12; r++)
            {
                int noben = regionalBenCount[r];
                if (noben > 0)
                {
                    Guid transactionGroupID = Guid.NewGuid();
                    DateTime transactionDate = DateTime.Now;

                    _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup() { PartitionID = 0, TransactionGroupID = transactionGroupID });

                    foreach (RationDetail rd in ration.RationDetails)
                    {
                        decimal amount = (rd.Amount/1000)*noben;

                        var entry2 = new Models.Transaction
                                                        {
                                                            RegionID = RegionID,
                                                            CommodityID = rd.CommodityID,
                                                            ParentCommodityID = rd.Commodity.ParentID,
                                                            Round = r + 1,
                                                            ProgramID = TransactionConstants.Constants.HRD_PROGRAM_ID,
                                                            QuantityInUnit = amount,
                                                            UnitID = 1,
                                                            QuantityInMT = amount,
                                                            LedgerID =
                                                                Cats.Models.Ledger.Constants.REQUIRMENT_DOCUMENT_PLAN,
                                                            // previously 200
                                                            TransactionDate = transactionDate,
                                                            TransactionGroupID = transactionGroupID,
                                                            PlanId = hrd.PlanID,
                                                            TransactionID = Guid.NewGuid(),
                                                        };
                        var entry1 = new Models.Transaction
                                                        {
                                                            RegionID = RegionID,
                                                            CommodityID = rd.CommodityID,
                                                            ParentCommodityID = rd.Commodity.ParentID,
                                                            Round = r + 1,
                                                            ProgramID = TransactionConstants.Constants.HRD_PROGRAM_ID,
                                                            QuantityInUnit = -amount,
                                                            UnitID = 1,
                                                            QuantityInMT = -amount,
                                                            LedgerID = Cats.Models.Ledger.Constants.REQUIRMENT_DOCUMENT,
                                                            //previously 100
                                                            TransactionDate = transactionDate,
                                                            TransactionGroupID = transactionGroupID,
                                                            PlanId = hrd.PlanID,
                                                            TransactionID = Guid.NewGuid(),
                                                        };
                        _unitOfWork.TransactionRepository.Add(entry1);
                        _unitOfWork.TransactionRepository.Add(entry2);
                       
                        entries.Add(entry1);
                        entries.Add(entry2);

                        //hrd.TransactionGroupID = transactionGroupID;
                        //_unitOfWork.HRDRepository.Edit(hrd);
                        _unitOfWork.Save();
                    }
                }
            }
            return entries;
        }
        
        //TODO: Check if this method is being called, 
        //Where in PSNP workflow did this get posted. 
        public List<Models.Transaction> PostPSNPPlan(RegionalPSNPPlan plan, Ration ration)
        {
            List<Models.Transaction> entries = new List<Models.Transaction>();
            List<int> regionalBenCount = new List<int>();
            for (int r = 0; r < plan.Duration; r++)
            {
                regionalBenCount.Add(0);
            }
            foreach (RegionalPSNPPlanDetail fdp in plan.RegionalPSNPPlanDetails)
            {
                for (int r = 0; r < fdp.FoodRatio; r++)
                {
                    regionalBenCount[r] += (int)fdp.BeneficiaryCount;
                }
            }
            for (int r = 0; r < plan.Duration; r++)
            {
                int noben = regionalBenCount[r];
                if (noben > 0)
                {
                    Guid transactionGroupID = Guid.NewGuid();
                    DateTime transactionDate = DateTime.Now;
                    foreach (RationDetail rd in ration.RationDetails)
                    {
                        decimal amount = rd.Amount * noben;

                        Models.Transaction entry2 = new Models.Transaction
                        {
                           
                            CommodityID = rd.CommodityID,
                            Round = r + 1,
                            ProgramID = TransactionConstants.Constants.PSNP_PROGRAM_ID,
                            QuantityInUnit = amount,
                            UnitID=1,
                            QuantityInMT = amount,
                            LedgerID = Cats.Models.Ledger.Constants.REQUIRMENT_DOCUMENT_PLAN, // previously 200
                            TransactionDate = transactionDate,
                            TransactionGroupID = transactionGroupID,
                            PlanId = plan.PlanId,
                            TransactionID = Guid.NewGuid(),
                        };
                        Models.Transaction entry1 = new Models.Transaction
                        {
                           
                            CommodityID = rd.CommodityID,
                            Round = r + 1,
                            ProgramID = TransactionConstants.Constants.PSNP_PROGRAM_ID,
                            QuantityInUnit = -amount,
                            UnitID = 1,
                            QuantityInMT = -amount,
                            LedgerID = Cats.Models.Ledger.Constants.REQUIRMENT_DOCUMENT, //previously 100
                            TransactionDate = transactionDate,
                            TransactionGroupID = transactionGroupID,
                            PlanId = plan.PlanId,
                            TransactionID = Guid.NewGuid(),
                        };
                        _unitOfWork.TransactionRepository.Add(entry1);
                        _unitOfWork.TransactionRepository.Add(entry2);
                        _unitOfWork.Save();
                        entries.Add(entry1);
                        entries.Add(entry2);
                    }
                    plan.TransactionGroupID = transactionGroupID;
                    _unitOfWork.RegionalPSNPPlanRepository.Edit(plan);
                }
            }
            return entries;
            
        }
#endregion

#region Post RRD
        //TODO: check if this is called for PSNP. It must be called for both programs
        public bool PostRequestAllocation(int requestId)//RRD
        {
            var result = new List<Models.Transaction>();
            var allocationDetails =
                _unitOfWork.ReliefRequisitionDetailRepository.FindBy(r => r.ReliefRequisition.RequisitionID == requestId);
            if (allocationDetails == null) return false;

            var transactionGroup = Guid.NewGuid();
            var transactionDate = DateTime.Now;
            _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup() { PartitionID = 0, TransactionGroupID = transactionGroup });


            foreach (var detail in allocationDetails)
            {
                
                    var transaction = new Models.Transaction();
                    transaction.TransactionID = Guid.NewGuid();
                    transaction.TransactionGroupID = transactionGroup;
                    transaction.TransactionDate = transactionDate;
                    transaction.UnitID = 1;

                    transaction.QuantityInMT = - detail.Amount;
                        transaction.QuantityInUnit = - detail.Amount;
                    transaction.LedgerID = Models.Ledger.Constants.PLEDGED_TO_FDP;
                    transaction.CommodityID = detail.CommodityID;
                    if (detail.Commodity.ParentID!=null)
                        transaction.ParentCommodityID = detail.Commodity.ParentID;
                    transaction.FDPID = detail.FDPID;
                    transaction.ProgramID = detail.ReliefRequisition.ProgramID;
                    transaction.RegionID = detail.ReliefRequisition.RegionID;
                    transaction.PlanId = detail.ReliefRequisition.RegionalRequest.PlanID;

                   transaction.Round = detail.ReliefRequisition.RegionalRequest.Round;
                   transaction.Month = detail.ReliefRequisition.RegionalRequest.Month;
                    _unitOfWork.TransactionRepository.Add(transaction);


                    //for Register Doc
                    transaction = new Models.Transaction();
                    transaction.TransactionID = Guid.NewGuid();
                    transaction.TransactionGroupID = transactionGroup;
                    transaction.TransactionDate = transactionDate;
                    transaction.UnitID = 1;

                    transaction.QuantityInMT = detail.Amount;
                    transaction.QuantityInUnit = detail.Amount;
                    transaction.LedgerID = Models.Ledger.Constants.REQUIRMENT_DOCUMENT;
                    transaction.CommodityID = detail.CommodityID;
                    if (detail.Commodity.ParentID != null)
                        transaction.ParentCommodityID = detail.Commodity.ParentID;
                    transaction.FDPID = detail.FDPID;
                    transaction.ProgramID = detail.ReliefRequisition.ProgramID;
                    transaction.RegionID = detail.ReliefRequisition.RegionID;
                    transaction.PlanId = detail.ReliefRequisition.RegionalRequest.PlanID;

                    transaction.Round = detail.ReliefRequisition.RegionalRequest.Round;
                    transaction.Month = detail.ReliefRequisition.RegionalRequest.Month;
                    _unitOfWork.TransactionRepository.Add(transaction);
                
            }

            _unitOfWork.Save();
            return true;
        }

#endregion

#region Post Dispatch Plan

        public bool PostSIAllocation(int requisitionID)
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
                transaction.ParentCommodityID = allocationDetail.ReliefRequisitionDetail.Commodity.ParentID;
                transaction.FDPID = allocationDetail.ReliefRequisitionDetail.FDPID;
                transaction.ProgramID = (int)allocationDetail.ReliefRequisitionDetail.ReliefRequisition.ProgramID;
                transaction.RegionID = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionID;
                transaction.PlanId = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionalRequest.PlanID;
                transaction.Round = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.Round;

                int hubID1=0 ;
                if (allocationDetail.AllocationType == TransactionConstants.Constants.SHIPPNG_INSTRUCTION)
                {
                    transaction.ShippingInstructionID = allocationDetail.Code;
                    hubID1= (int) _unitOfWork.TransactionRepository.FindBy(m=>m.ShippingInstructionID==allocationDetail.Code && m.LedgerID==Ledger.Constants.GOODS_ON_HAND).Select(m=>m.HubID).FirstOrDefault();
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

                if (hubID1!=0)
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
                transaction2.ParentCommodityID = allocationDetail.ReliefRequisitionDetail.Commodity.ParentID;
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

                    hubID2=(int) _unitOfWork.TransactionRepository.FindBy(m => m.ShippingInstructionID == allocationDetail.Code &&
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


                if (hubID2!=0)
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

        public bool PostSIAllocationUncommit(int requisitionID)
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

                // I see some logical error here
                // what happens when hub x was selected and the allocation was made from hub y? 
                //TOFIX: 
                // Hub is required for this transaction
                // Try catch is danger!! Either throw the exception or use conditional statement. 
                int hubID = 0;
                if (allocationDetail.AllocationType == TransactionConstants.Constants.SHIPPNG_INSTRUCTION)
                {
                    var siCode = allocationDetail.Code.ToString();
                    var shippingInstruction =
                        _unitOfWork.ShippingInstructionRepository.Get(t => t.Value == siCode).
                            FirstOrDefault();
                    if (shippingInstruction != null) transaction.ShippingInstructionID = shippingInstruction.ShippingInstructionID;

                    hubID = (int)_unitOfWork.TransactionRepository.FindBy(m => m.ShippingInstructionID == allocationDetail.Code &&
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

                    hubID = (int)_unitOfWork.TransactionRepository.FindBy(m => m.ProjectCodeID == allocationDetail.Code &&
                               m.LedgerID == Ledger.Constants.GOODS_ON_HAND).Select(m => m.HubID).FirstOrDefault();

                }


                if (hubID != 0)
                {
                    transaction.HubID = hubID;
                }

                else
                {
                    transaction.HubID =
                                       _unitOfWork.HubAllocationRepository.FindBy(r => r.RequisitionID == allocation.ReliefRequisitionDetail.RequisitionID).Select(
                                               h => h.HubID).FirstOrDefault();

                }


                transaction.QuantityInMT = allocationDetail.AllocatedAmount;
                transaction.QuantityInUnit = allocationDetail.AllocatedAmount;
                transaction.LedgerID = Ledger.Constants.COMMITED_TO_FDP;
                transaction.CommodityID = allocationDetail.ReliefRequisitionDetail.CommodityID;
                transaction.FDPID = allocationDetail.ReliefRequisitionDetail.FDPID;
                transaction.ProgramID = (int)allocationDetail.ReliefRequisitionDetail.ReliefRequisition.ProgramID;
                transaction.RegionID = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionID;
                if (allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionalRequest!=null)
                {
                    transaction.PlanId = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionalRequest.PlanID;
                }
                
                transaction.Round = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.Round;


                if (allocationDetail.AllocationType == TransactionConstants.Constants.SHIPPNG_INSTRUCTION)
                {
                    transaction.ShippingInstructionID = allocationDetail.Code;
                }
                else
                {
                    transaction.ProjectCodeID = allocationDetail.Code;
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

                //TOFIX: do not use try catch
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
                transaction2.QuantityInMT = -allocationDetail.AllocatedAmount;
                transaction2.QuantityInUnit = -allocationDetail.AllocatedAmount;
                transaction2.LedgerID = Ledger.Constants.PLEDGED_TO_FDP;
                transaction2.CommodityID = allocationDetail.ReliefRequisitionDetail.CommodityID;
                transaction2.FDPID = allocationDetail.ReliefRequisitionDetail.FDPID;
                transaction2.ProgramID = (int)allocationDetail.ReliefRequisitionDetail.ReliefRequisition.ProgramID;
                transaction2.RegionID = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionID;
                if (allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionalRequest != null)
                {
                    transaction2.PlanId =
                        allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionalRequest.PlanID;
                }
                transaction2.Round = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.Round;

                if (allocationDetail.AllocationType == TransactionConstants.Constants.SHIPPNG_INSTRUCTION)
                {
                    var siCode = allocationDetail.Code.ToString();
                    var shippingInstruction =
                        _unitOfWork.ShippingInstructionRepository.Get(t => t.Value == siCode).
                            FirstOrDefault();
                    if (shippingInstruction != null) transaction.ShippingInstructionID = shippingInstruction.ShippingInstructionID;
                }
                else
                {
                    var detail = allocationDetail;
                    var code = detail.Code.ToString();
                    var projectCode =
                        _unitOfWork.ProjectCodeRepository.Get(t => t.Value == code).
                            FirstOrDefault();
                    if (projectCode != null) transaction.ProjectCodeID = projectCode.ProjectCodeID;
                }

                
                _unitOfWork.TransactionRepository.Add(transaction2);

                _unitOfWork.SIPCAllocationRepository.Delete(allocationDetail);
                //result.Add(transaction);
            }
            var requisition = _unitOfWork.ReliefRequisitionRepository.FindById(requisitionID);
            requisition.Status = (int) Cats.Models.Constant.ReliefRequisitionStatus.Approved;
            _unitOfWork.ReliefRequisitionRepository.Edit(requisition);
            _unitOfWork.Save();
            //return result;
            return true;
        }

#endregion

#region Post Donation Plan

        public bool PostDonationPlan(DonationPlanHeader donationPlanHeader)
        {
            var transactionGroup = Guid.NewGuid();
            var transactionDate = DateTime.Now;

            _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup()
            {
                PartitionID = 0,
                TransactionGroupID = transactionGroup
            });

            foreach (var donationPlanDetail in donationPlanHeader.DonationPlanDetails)
            {
                var transaction = new Models.Transaction
                                      {
                                          TransactionID = Guid.NewGuid(),
                                          ProgramID = donationPlanHeader.ProgramID,
                                          DonorID = donationPlanHeader.DonorID,
                                          CommoditySourceID = 1,
                                          QuantityInMT = donationPlanDetail.AllocatedAmount,
                                          TransactionGroupID = transactionGroup,
                                          TransactionDate = transactionDate,
                                          CommodityID = donationPlanHeader.CommodityID,
                                          ParentCommodityID = donationPlanHeader.Commodity.ParentID,
                                          ShippingInstructionID = donationPlanHeader.ShippingInstructionId,
                                          HubID = donationPlanDetail.HubID,
                                          LedgerID = Ledger.Constants.GOODS_RECIEVABLE
                                      };

                _unitOfWork.TransactionRepository.Add(transaction);


                transaction= new Models.Transaction
                                 {
                                     TransactionID = Guid.NewGuid(),
                                     ProgramID = donationPlanHeader.ProgramID,
                                     DonorID = donationPlanHeader.DonorID,
                                     CommoditySourceID = 1,
                                     QuantityInMT = -donationPlanDetail.AllocatedAmount,
                                     TransactionGroupID = transactionGroup,
                                     TransactionDate = transactionDate,
                                     CommodityID = donationPlanHeader.CommodityID,
                                     ParentCommodityID = donationPlanHeader.Commodity.ParentID,
                                     ShippingInstructionID = donationPlanHeader.ShippingInstructionId,
                                     HubID = donationPlanDetail.HubID,
                                     LedgerID = Ledger.Constants.GIFT_CERTIFICATE //good promissed - pledged is not in ledger list // Former LedgerID = 4
                                 };

                _unitOfWork.TransactionRepository.Add(transaction);
            }

            var donationHeader =
                _unitOfWork.DonationPlanHeaderRepository.FindById(donationPlanHeader.DonationHeaderPlanID);
            if (donationHeader!=null)
                donationPlanHeader.TransactionGroupID = transactionGroup;
            _unitOfWork.Save();
            return true;

        }

        #endregion

#region Post Local Purchase

        public bool PostLocalPurchase(List<LocalPurchaseDetail> localPurchaseDetail)
        {
            var transactionGroup = Guid.NewGuid();
            var transactionDate = DateTime.Now;

            _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup()
            {
                PartitionID = 0,
                TransactionGroupID = transactionGroup
            });

            if (localPurchaseDetail != null)
                foreach (var detail in localPurchaseDetail)
                {
                    var transaction = new Models.Transaction
                                          {
                                              TransactionID = Guid.NewGuid(),
                                              CommoditySourceID =
                                                  Models.Constant.CommoditySourceConst.Constants.LOCALPURCHASE,
                                              CommodityID = detail.LocalPurchase.CommodityID,
                                              ParentCommodityID = detail.LocalPurchase.Commodity.ParentID,
                                              DonorID = detail.LocalPurchase.DonorID,
                                              HubID = detail.HubID,
                                              ProgramID = detail.LocalPurchase.ProgramID,
                                             
                                              QuantityInMT = detail.AllocatedAmount,
                                              QuantityInUnit = detail.AllocatedAmount,
                                              ShippingInstructionID = detail.LocalPurchase.ShippingInstructionID,
                                              TransactionDate = transactionDate,
                                              TransactionGroupID = transactionGroup
                                              //LedgerID =  Models.Ledger.Constants
                                          };

                  
                    _unitOfWork.TransactionRepository.Add(transaction);



                    transaction = new Models.Transaction
                    {
                        TransactionID = Guid.NewGuid(),
                        CommoditySourceID =
                            Models.Constant.CommoditySourceConst.Constants.LOCALPURCHASE,
                        CommodityID = detail.LocalPurchase.CommodityID,
                        ParentCommodityID = detail.LocalPurchase.Commodity.ParentID,
                        DonorID = detail.LocalPurchase.DonorID,
                        HubID = detail.HubID,
                        ProgramID = detail.LocalPurchase.ProgramID,
                       
                        QuantityInMT = detail.AllocatedAmount,
                        QuantityInUnit = detail.AllocatedAmount,
                        ShippingInstructionID = detail.LocalPurchase.ShippingInstructionID,
                        TransactionDate = transactionDate,
                        TransactionGroupID = transactionGroup
                        //LedgerID =  Models.Ledger.Constants
                    };

                    _unitOfWork.TransactionRepository.Add(transaction);

                }
            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
                
            }
           

        }
        #endregion

#region Post Loan

        public bool PostLoan(LoanReciptPlan loanReciptPlan)
        {
            var transactionGroup = Guid.NewGuid();
            var transactionDate = DateTime.Now;

            _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup()
            {
                PartitionID = 0,
                TransactionGroupID = transactionGroup
            });

            foreach (var loan in loanReciptPlan.LoanReciptPlanDetails)
            {

                var transaction = new Models.Transaction()
                                            {
                                                TransactionID = Guid.NewGuid(),
                                                CommoditySourceID =Models.Constant.CommoditySourceConst.Constants.LOAN,
                                                CommodityID = loan.LoanReciptPlan.CommodityID,
                                                ParentCommodityID = loan.LoanReciptPlan.Commodity.ParentID,
                                                ShippingInstructionID = loanReciptPlan.ShippingInstruction.ShippingInstructionID,
                                                QuantityInMT = loan.RecievedQuantity,
                                                HubID = loan.HubID,
                                                TransactionDate = transactionDate,
                                                TransactionGroupID = transactionGroup
                                               // LedgerID = Cats.Models.Ledger.Constants
                                            };

                _unitOfWork.TransactionRepository.Add(transaction);


                transaction = new Models.Transaction()
                {
                    TransactionID = Guid.NewGuid(),
                    CommoditySourceID = Models.Constant.CommoditySourceConst.Constants.LOAN,
                    CommodityID = loan.LoanReciptPlan.CommodityID,
                    ParentCommodityID = loan.LoanReciptPlan.Commodity.ParentID,
                    ShippingInstructionID = loanReciptPlan.ShippingInstruction.ShippingInstructionID,
                    QuantityInMT = loan.RecievedQuantity,
                    HubID = loan.HubID,
                    TransactionDate = transactionDate,
                    TransactionGroupID = transactionGroup
                    // LedgerID = Cats.Models.Ledger.Constants
                };

                _unitOfWork.TransactionRepository.Add(transaction);


            }

            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        #endregion
        //commodityParentId not found
#region Post Distribution
        public bool PostDistribution(int distributionId)
        {
            var woredaStcokDistribution = _unitOfWork.WoredaStockDistributionRepository.FindById(distributionId);
            if (woredaStcokDistribution!=null)
            {
                var transactionGroup = Guid.NewGuid();
                var transactionDate = DateTime.Now;

                _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup()
                {
                    PartitionID = 0,
                    TransactionGroupID = transactionGroup
                });

                foreach (var woredaStockDistributionDetail in woredaStcokDistribution.WoredaStockDistributionDetails)
                {
                    var transaction = new Models.Transaction
                    {

                        TransactionID = Guid.NewGuid(),
                        ProgramID = woredaStcokDistribution.ProgramID,
                        QuantityInMT = woredaStockDistributionDetail.DistributedAmount,
                        TransactionGroupID = transactionGroup,
                        TransactionDate = transactionDate,
                        FDPID = woredaStockDistributionDetail.FdpId,
                        Month = woredaStcokDistribution.Month,
                        PlanId = woredaStcokDistribution.PlanID,
                        CommodityID = woredaStockDistributionDetail.CommodityID,
                        
                        //add commodity
                       LedgerID = Ledger.Constants.GOODS_UNDER_CARE
                    };

                    _unitOfWork.TransactionRepository.Add(transaction);

                    transaction = new Models.Transaction
                    {

                        TransactionID = Guid.NewGuid(),
                        ProgramID = woredaStcokDistribution.ProgramID,
                        QuantityInMT = -woredaStockDistributionDetail.DistributedAmount,
                        TransactionGroupID = transactionGroup,
                        TransactionDate = transactionDate,
                        FDPID = woredaStockDistributionDetail.FdpId,
                        Month = woredaStcokDistribution.Month,
                        PlanId = woredaStcokDistribution.PlanID,
                        CommodityID = woredaStockDistributionDetail.CommodityID,
                        //add commodity
                        LedgerID = Ledger.Constants.DELIVERY_RECEIPT
                    };

                    _unitOfWork.TransactionRepository.Add(transaction);


                }

                woredaStcokDistribution.TransactionGroupID = transactionGroup;
                _unitOfWork.Save();
            }
            return true;
        }

        #endregion

#region Post GiftCeritifficate

        public bool PostGiftCertificate(int giftCertificateId)
        {
            var giftCertificate = _unitOfWork.GiftCertificateRepository.Get(t => t.GiftCertificateID == giftCertificateId, null,"GiftCertificateDetails").FirstOrDefault();
           if(giftCertificate==null) return false;

            var transactionGroup = Guid.NewGuid();
            var transactionDate = DateTime.Now;
            _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup()
                                                           {
                                                               PartitionID = 0, TransactionGroupID = transactionGroup
                                                           });
            foreach (var giftCertificateDetail in giftCertificate.GiftCertificateDetails)
            {
                var transaction = new Models.Transaction();
                transaction.TransactionID = Guid.NewGuid();
                transaction.ProgramID = giftCertificate.ProgramID;
                transaction.DonorID = giftCertificate.DonorID;
                transaction.CommoditySourceID = giftCertificateDetail.DFundSourceID;
                transaction.GiftTypeID = giftCertificateDetail.DFundSourceID;
                transaction.QuantityInMT = giftCertificateDetail.WeightInMT;
                transaction.QuantityInUnit = giftCertificateDetail.WeightInMT;
                transaction.TransactionGroupID = transactionGroup;
                transaction.TransactionDate = transactionDate;
                transaction.UnitID = 1;
                transaction.LedgerID = Ledger.Constants.GIFT_CERTIFICATE;//Goods Promised - Gift Certificate - Commited not found in ledger list
                transaction.CommodityID = giftCertificateDetail.CommodityID;
                transaction.ParentCommodityID = giftCertificateDetail.Commodity.ParentID;
               // transaction.ShippingInstructionID = giftCertificate.SINumber;
                _unitOfWork.TransactionRepository.Add(transaction);

                transaction = new Models.Transaction();
                transaction.TransactionID = Guid.NewGuid();
                transaction.ProgramID = giftCertificate.ProgramID;
                transaction.DonorID = giftCertificate.DonorID;
                transaction.QuantityInMT = -giftCertificateDetail.WeightInMT;
                transaction.TransactionGroupID = transactionGroup;
                transaction.TransactionDate = transactionDate;
                transaction.QuantityInUnit = giftCertificateDetail.WeightInMT;
                transaction.UnitID = 1;
                transaction.ParentCommodityID = giftCertificateDetail.Commodity.ParentID;
                transaction.LedgerID = Ledger.Constants.PLEDGE;//Goods Promised - Pledge	 not found in ledger list

                _unitOfWork.TransactionRepository.Add(transaction);


            }
           
            giftCertificate.StatusID = 2;
            giftCertificate.TransactionGroupID = transactionGroup;
            _unitOfWork.Save();
            return true;
        }
#endregion

        // commodityId and commodityParentId not found
#region Post Delivery Reconcile

        public bool PostDeliveryReconcileReceipt(int deliveryReconcileID)
        {
            var deliveryReconcile = _unitOfWork.DeliveryReconcileRepository.Get(t => t.DeliveryReconcileID == deliveryReconcileID, null, "Dispatch").FirstOrDefault();
            if (deliveryReconcile == null) return false;

            var transactionGroup = Guid.NewGuid();
            var transactionDate = DateTime.Now;
            _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup() { PartitionID = 0, TransactionGroupID = transactionGroup });




            var transaction = new Models.Transaction();
            transaction.TransactionID = Guid.NewGuid();

            var reliefRequisition = _unitOfWork.ReliefRequisitionRepository.Get(t => t.RequisitionNo == deliveryReconcile.RequsitionNo).FirstOrDefault();
            if (reliefRequisition != null)
                transaction.ProgramID = reliefRequisition.ProgramID;
            var orDefault = _unitOfWork.DispatchRepository.Get(t => t.DispatchID == deliveryReconcile.DispatchID).FirstOrDefault();
            if (orDefault !=null)
                transaction.DonorID = orDefault.DispatchAllocation.DonorID;
            transaction.TransactionGroupID = transactionGroup;
            transaction.TransactionDate = transactionDate;
            var dispatch = _unitOfWork.DispatchRepository.Get(t => t.DispatchID == deliveryReconcile.DispatchID).FirstOrDefault();
            if (dispatch !=null)
                transaction.ShippingInstructionID = dispatch.DispatchAllocation.ShippingInstructionID;
            if (reliefRequisition != null) transaction.PlanId = reliefRequisition.RegionalRequest.PlanID;
            if (reliefRequisition != null) transaction.Round = reliefRequisition.RegionalRequest.Round;
            //transaction.LedgerID = Ledger.Constants.DELIVERY_RECEIPT;
            //transaction.FDPID = delivery.FDPID;
            //var firstOrDefault = delivery.DeliveryDetails.FirstOrDefault();

            transaction.LedgerID = Models.Ledger.Constants.DELIVERY_RECEIPT;
            transaction.FDPID = deliveryReconcile.FDPID;
            var firstOrDefault = deliveryReconcile.Dispatch.DispatchAllocation;

            if (firstOrDefault != null)
            {
                transaction.CommodityID = firstOrDefault.CommodityID;
                
            }
            transaction.QuantityInMT = deliveryReconcile.ReceivedAmount / 10;
            transaction.QuantityInUnit = deliveryReconcile.ReceivedAmount;
            var @default = _unitOfWork.UnitRepository.Get(t => t.Name == "Quintal").FirstOrDefault();
            transaction.UnitID = @default != null ? @default.UnitID : 1;
            _unitOfWork.TransactionRepository.Add(transaction);




            transaction = new Models.Transaction();
            transaction.TransactionID = Guid.NewGuid();
            if (reliefRequisition != null)
                transaction.ProgramID = reliefRequisition.ProgramID;
            
            if (orDefault != null)
                transaction.DonorID = orDefault.DispatchAllocation.DonorID;
            transaction.TransactionGroupID = transactionGroup;
            transaction.TransactionDate = transactionDate;
            if (dispatch != null)
                transaction.ShippingInstructionID = dispatch.DispatchAllocation.ShippingInstructionID;
            if (reliefRequisition != null) transaction.PlanId = reliefRequisition.RegionalRequest.PlanID;
            if (reliefRequisition != null) transaction.Round = reliefRequisition.RegionalRequest.Round;
            transaction.LedgerID = Models.Ledger.Constants.GOODS_IN_TRANSIT;
            transaction.HubID = deliveryReconcile.HubID;
            transaction.FDPID = deliveryReconcile.FDPID;
            
            
            if (firstOrDefault != null)
            {
                transaction.CommodityID = firstOrDefault.CommodityID;
            }
            transaction.QuantityInMT = -deliveryReconcile.ReceivedAmount / 10;
            transaction.QuantityInUnit = -deliveryReconcile.ReceivedAmount;
            transaction.UnitID = @default != null ? @default.UnitID : 1;
            _unitOfWork.TransactionRepository.Add(transaction);

            deliveryReconcile.TransactionGroupID = transactionGroup;
            _unitOfWork.Save();
            return true;
        }

        #endregion

        public List<ReceiptAllocation> ReceiptAllocationFindBy(Expression<Func<ReceiptAllocation, bool>> predicate)
        {
            return _unitOfWork.ReceiptAllocationReository.FindBy(predicate);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}