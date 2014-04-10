﻿using System;
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
                    foreach (RationDetail rd in ration.RationDetails)
                    {
                        decimal amount = rd.Amount * noben;
                        
                        Models.Transaction entry2 = new Models.Transaction
                        {
                            RegionID = RegionID,
                            CommodityID = rd.CommodityID,
                            Round = r + 1,
                            ProgramID = TransactionConstants.Constants.HRD_PROGRAM_ID,
                            QuantityInUnit = amount,
                            UnitID = 1,
                            QuantityInMT = amount,
                            LedgerID = Cats.Models.Ledger.Constants.REQUIRMENT_DOCUMENT_PALN, // previously 200
                            TransactionDate = transactionDate,
                            TransactionGroupID = transactionGroupID,
                            TransactionID = Guid.NewGuid(),
                        };
                        Models.Transaction entry1 = new Models.Transaction
                        {
                            RegionID = RegionID,
                            CommodityID = rd.CommodityID,
                            Round = r + 1,
                            ProgramID = TransactionConstants.Constants.HRD_PROGRAM_ID,
                            QuantityInUnit = -amount,
                            UnitID = 1,
                            QuantityInMT = -amount,
                            LedgerID = Cats.Models.Ledger.Constants.REQUIRMENT_DOCUMENT, //previously 100
                            TransactionDate = transactionDate,
                            TransactionGroupID = transactionGroupID,
                            TransactionID = Guid.NewGuid(),
                        };
                        _unitOfWork.TransactionRepository.Add(entry1);
                        _unitOfWork.TransactionRepository.Add(entry2);
                        _unitOfWork.Save();
                        entries.Add(entry1);
                        entries.Add(entry2);
                    }
                    hrd.TransactionGroupID = transactionGroupID;
                    _unitOfWork.HRDRepository.Edit(hrd);
                }
            }
            return entries;
        }
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
                            LedgerID = Cats.Models.Ledger.Constants.REQUIRMENT_DOCUMENT_PALN, // previously 200
                            TransactionDate = transactionDate,
                            TransactionGroupID = transactionGroupID,
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
        public bool PostSIAllocation(int requisitionID)
        {
            var result = new List<Models.Transaction>();
            var allocationDetails = _unitOfWork.SIPCAllocationRepository.Get(t => t.ReliefRequisitionDetail.RequisitionID == requisitionID);
            if (allocationDetails == null) return false;

            var transactionGroup = Guid.NewGuid();
            var transactionDate = DateTime.Now;
            _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup() { PartitionID = 0, TransactionGroupID = transactionGroup });

            //ProjectCodeID	ShippingInstructionID ProgramID QuantityInMT	QuantityInUnit	UnitID	TransactionDate	RegionID	Month	Round	DonorID	CommoditySourceID	GiftTypeID	FDP
            
            foreach (var allocationDetail in allocationDetails)
            {
                var transaction = new Models.Transaction();
                transaction.TransactionID = Guid.NewGuid();
                transaction.TransactionGroupID = transactionGroup;
                transaction.TransactionDate = transactionDate;
                transaction.UnitID = 1;

                transaction.QuantityInMT = -allocationDetail.AllocatedAmount;
                transaction.QuantityInUnit = -allocationDetail.AllocatedAmount;
                transaction.LedgerID = Models.Ledger.Constants.COMMITED_TO_FDP;
                transaction.CommodityID = allocationDetail.ReliefRequisitionDetail.CommodityID;
                transaction.FDPID = allocationDetail.ReliefRequisitionDetail.FDPID;
                transaction.ProgramID = (int)allocationDetail.ReliefRequisitionDetail.ReliefRequisition.ProgramID;
                transaction.RegionID = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionID;

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
                    var projectCode =
                        _unitOfWork.ProjectCodeRepository.Get(t => t.Value == detail.Code.ToString()).
                            FirstOrDefault();
                    if (projectCode != null) transaction.ProjectCodeID = projectCode.ProjectCodeID;
                }
                _unitOfWork.TransactionRepository.Add(transaction);
               // result.Add(transaction);

                /*post Debit-Pledged To FDP*/
                var transaction2 = new Models.Transaction();
                transaction2.TransactionID = Guid.NewGuid();
                transaction2.TransactionGroupID = transactionGroup;
                transaction2.TransactionDate = transactionDate;
                transaction2.UnitID = 1;

                transaction2.QuantityInMT = allocationDetail.AllocatedAmount;
                transaction2.QuantityInUnit = allocationDetail.AllocatedAmount;
                transaction2.LedgerID = Models.Ledger.Constants.PLEDGED_TO_FDP;
                transaction2.CommodityID = allocationDetail.ReliefRequisitionDetail.CommodityID;
                transaction2.FDPID = allocationDetail.ReliefRequisitionDetail.FDPID;
                transaction2.ProgramID = (int)allocationDetail.ReliefRequisitionDetail.ReliefRequisition.ProgramID;
                transaction2.RegionID = allocationDetail.ReliefRequisitionDetail.ReliefRequisition.RegionID;
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
                    var projectCode =
                        _unitOfWork.ProjectCodeRepository.Get(t => t.Value == detail.Code.ToString()).
                            FirstOrDefault();
                    if (projectCode != null) transaction.ProjectCodeID = projectCode.ProjectCodeID;
                }
                _unitOfWork.TransactionRepository.Add(transaction2);
                allocationDetail.TransactionGroupID = transactionGroup;
                _unitOfWork.SIPCAllocationRepository.Edit(allocationDetail);
                //result.Add(transaction);
            }
            ReliefRequisition requisition = _unitOfWork.ReliefRequisitionRepository.FindById(requisitionID);
            requisition.Status = 4;
           _unitOfWork.Save();
              //return result;
            return true;
        }

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
                                     ShippingInstructionID = donationPlanHeader.ShippingInstructionId,
                                     HubID = donationPlanDetail.HubID,
                                     LedgerID = Ledger.Constants.GOODS_PROMISSED_GIFT_CERTIFICATE_COMMITED //good promissed - pledged is not in ledger list // Former LedgerID = 4
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

                        LedgerID = Ledger.Constants.DELIVERY_RECEIPT
                    };

                    _unitOfWork.TransactionRepository.Add(transaction);


                }

                woredaStcokDistribution.TransactionGroupID = transactionGroup;
                _unitOfWork.Save();
            }
            return true;
        }
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
                transaction.LedgerID = Ledger.Constants.GOODS_PROMISSED_GIFT_CERTIFICATE_COMMITED;//Goods Promised - Gift Certificate - Commited not found in ledger list
                transaction.CommodityID = giftCertificateDetail.CommodityID;
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
                transaction.LedgerID = Ledger.Constants.GOODS_PROMISSED_PLEDGE;//Goods Promised - Pledge	 not found in ledger list

                _unitOfWork.TransactionRepository.Add(transaction);



            }
           
            giftCertificate.StatusID = 2;
            giftCertificate.TransactionGroupID = transactionGroup;
            _unitOfWork.Save();
            return true;
        }

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
            //var reliefRequisition = _unitOfWork.ReliefRequisitionRepository.Get(t => t.RequisitionNo == distribution.RequisitionNo).FirstOrDefault();
            if (reliefRequisition != null)
                transaction.ProgramID = reliefRequisition.ProgramID;
            //var orDefault = _unitOfWorkhub.DispatchRepository.Get(t => t.DispatchID == distribution.DispatchID).FirstOrDefault();
            if (orDefault != null)
                transaction.DonorID = orDefault.DispatchAllocation.DonorID;
            transaction.TransactionGroupID = transactionGroup;
            transaction.TransactionDate = transactionDate;
            //var dispatch = _unitOfWorkhub.DispatchRepository.Get(t => t.DispatchID == distribution.DispatchID).FirstOrDefault();
            if (dispatch != null)
                transaction.ShippingInstructionID = dispatch.DispatchAllocation.ShippingInstructionID;

            //transaction.LedgerID = Ledger.Constants.GOODS_IN_TRANSIT;
            //transaction.HubID = delivery.HubID;
            //transaction.FDPID = delivery.FDPID;

            transaction.LedgerID = Models.Ledger.Constants.GOODS_IN_TRANSIT;
            transaction.HubID = deliveryReconcile.HubID;
            transaction.FDPID = deliveryReconcile.FDPID;

            //var firstOrDefault = distribution.DeliveryDetails.FirstOrDefault();
            if (firstOrDefault != null)
            {
                transaction.CommodityID = firstOrDefault.CommodityID;
            }
            transaction.QuantityInMT = deliveryReconcile.ReceivedAmount / 10;
            transaction.QuantityInUnit = deliveryReconcile.ReceivedAmount;
            var @default1 = _unitOfWork.UnitRepository.Get(t => t.Name == "Quintal").FirstOrDefault();
            transaction.UnitID = @default != null ? @default.UnitID : 1;
            _unitOfWork.TransactionRepository.Add(transaction);

            deliveryReconcile.TransactionGroupID = transactionGroup;
            _unitOfWork.Save();
            return true;
        }
        
        public List<ProjectCode> getAllProjectByHubCommodity(int hubId, int commodityId)
        {
            var receiptAllocation = ReceiptAllocationFindBy(t => t.HubID == hubId && t.CommodityID == commodityId);
            var transaction = FindBy(t => t.CommodityID == commodityId && t.HubID == hubId);
            //var projectCode=(from receipt in receiptAllocation
            //                 where receipt.QuantityInMT >= (from t in transaction
            //                                                where receipt.ProjectNumber == _unitOfWork.ProjectCodeRepository.FindById(t.ProjectCodeID).Value
            //                                                        && t.LedgerID == 2
            //                                                        && t.QuantityInMT > 0
            //                                                select t.QuantityInMT).Sum()
            //                 select receipt);
            var projectCodeList = new List<ProjectCode>();
            foreach (var receiv in receiptAllocation)
            {
                int projectCodeID = 0;
                try
                {
                    projectCodeID = _unitOfWork.ProjectCodeRepository.FindBy(t => t.Value == receiv.ProjectNumber).FirstOrDefault().ProjectCodeID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                var transactionQuantity = transaction.FindAll(t => t.ProjectCodeID == projectCodeID && t.LedgerID == 2 && t.QuantityInMT > 0).Sum(t => t.QuantityInMT);
                if (receiv.QuantityInMT > transactionQuantity)
                    projectCodeList.Add(new ProjectCode { ProjectCodeID = projectCodeID, Value = receiv.ProjectNumber });
            }
            return projectCodeList;
        }
        public List<ShippingInstruction> getAllSIByHubCommodity(int hubId, int commodityId)
        {
            var transaction = FindBy(t => t.CommodityID == commodityId && t.HubID == hubId);
            var receiptAllocation = ReceiptAllocationFindBy(t => t.HubID == hubId && t.CommodityID == commodityId);

            var ShippingInstructionList = new List<ShippingInstruction>();
            foreach (var receiv in receiptAllocation)
            {
                int SiId = 0;
                try
                {
                    SiId = _unitOfWork.ShippingInstructionRepository.FindBy(t => t.Value == receiv.SINumber).FirstOrDefault().ShippingInstructionID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                var transactionQuantity = transaction.FindAll(t => t.ProjectCodeID == SiId && t.LedgerID == 2 && t.QuantityInMT > 0).Sum(t => t.QuantityInMT);
                if (receiv.QuantityInMT > transactionQuantity)
                    ShippingInstructionList.Add(new ShippingInstruction { ShippingInstructionID = SiId, Value = receiv.SINumber });
            }
            return ShippingInstructionList;
        }

        public List<ReceiptAllocation> getSIBalance(int hubId, int commodityId)
        {
            var transaction = FindBy(t => t.CommodityID == commodityId && t.HubID == hubId);
            var receiptAllocation = ReceiptAllocationFindBy(t => t.HubID == hubId && t.CommodityID == commodityId);

            var ShippingInstructionList = new List<ReceiptAllocation>();
            decimal balance = 0;
            foreach (var receiv in receiptAllocation)
            {
                int SiId = 0;
                try
                {
                    SiId = _unitOfWork.ShippingInstructionRepository.FindBy(t => t.Value == receiv.SINumber).FirstOrDefault().ShippingInstructionID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                var transactionQuantity = transaction.FindAll(t => t.ProjectCodeID == SiId && t.LedgerID == 2 && t.QuantityInMT > 0).Sum(t => t.QuantityInMT);
                if (receiv.QuantityInMT > transactionQuantity)
                {
                    balance = receiv.QuantityInMT - transactionQuantity;
                    ShippingInstructionList.Add(new ReceiptAllocation { SINumber = receiv.SINumber, QuantityInMT = balance });
                }
                //else
                //{
                //    ShippingInstructionList.Add(new ReceiptAllocation { SINumber = receiv.SINumber, QuantityInMT = 0 });
                //}
            }
            return ShippingInstructionList;
        }
        public List<ReceiptAllocation> getProjectBalance(int hubId, int commodityId)
        {
            var receiptAllocation = ReceiptAllocationFindBy(t => t.HubID == hubId && t.CommodityID == commodityId);
            var transaction = FindBy(t => t.CommodityID == commodityId && t.HubID == hubId);
            var projectCodeList = new List<ReceiptAllocation>();
            decimal balance = 0;
            foreach (var receiv in receiptAllocation)
            {

                int projectCodeID = 0;
                try
                {
                    projectCodeID = _unitOfWork.ProjectCodeRepository.FindBy(t => t.Value == receiv.ProjectNumber).FirstOrDefault().ProjectCodeID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                var transactionQuantity = transaction.FindAll(t => t.ProjectCodeID == projectCodeID && t.LedgerID == 2 && t.QuantityInMT > 0).Sum(t => t.QuantityInMT);
                if (receiv.QuantityInMT > transactionQuantity)
                {
                    balance = receiv.QuantityInMT - transactionQuantity;
                    projectCodeList.Add(new ReceiptAllocation { ProjectNumber = receiv.ProjectNumber, QuantityInMT = balance });
                }
                //else
                //{
                //    projectCodeList.Add(new ReceiptAllocation { ProjectNumber = receiv.ProjectNumber, QuantityInMT = 0 });
                //}
            }

            return projectCodeList;
        }

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