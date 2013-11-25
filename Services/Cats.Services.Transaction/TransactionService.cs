using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
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
                    foreach (RationDetail rd in ration.RationDetails)
                    {
                        decimal amount = rd.Amount * noben;

                        Models.Transaction entry2 = new Models.Transaction
                        {
                            RegionID = RegionID,
                            CommodityID = rd.CommodityID,
                            Round = r + 1,
                            ProgramID = 2,
                            QuantityInUnit = -amount,
                            UnitID = 1,
                            QuantityInMT = -amount,
                            LedgerID = 200
                        };
                        Models.Transaction entry1 = new Models.Transaction
                        {
                            RegionID = RegionID,
                            CommodityID = rd.CommodityID,
                            Round = r + 1,
                            ProgramID = 2,
                            QuantityInUnit = amount,
                            UnitID = 1,
                            QuantityInMT = amount,
                            LedgerID = 100
                        };
                        entries.Add(entry1);
                        entries.Add(entry2);

                    }
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
                    foreach (RationDetail rd in ration.RationDetails)
                    {
                        decimal amount = rd.Amount * noben;

                        Models.Transaction entry2 = new Models.Transaction
                        {
                            RegionID = plan.RegionID,
                            CommodityID = rd.CommodityID,
                            Round = r + 1,
                            ProgramID = 2,
                            QuantityInUnit = -amount,
                            UnitID=1,
                            QuantityInMT = -amount,
                            LedgerID = 200
                        };
                        Models.Transaction entry1 = new Models.Transaction
                        {
                            RegionID = plan.RegionID,
                            CommodityID = rd.CommodityID,
                            Round = r + 1,
                            ProgramID = 2,
                            QuantityInUnit = amount,
                            UnitID = 1,
                            QuantityInMT = amount,
                            LedgerID = 100
                        };
                        entries.Add(entry1);
                        entries.Add(entry2);

                    }
                }
            }
            PostTransaction(entries);
            return entries;
            
        }



        public bool PostGiftCertificate(int giftCertificateId)
        {
            var giftCertificate = _unitOfWork.GiftCertificateRepository.Get(t => t.GiftCertificateID == giftCertificateId, null,"GiftCertificateDetails").FirstOrDefault();
           if(giftCertificate==null) return false;

            var transactionGroup = Guid.NewGuid();
            var transactionDate = DateTime.Now;
            _unitOfWork.TransactionGroupRepository.Add(new TransactionGroup() { PartitionID = 0, TransactionGroupID = transactionGroup });
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
                transaction.LedgerID = 5;
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
                transaction.LedgerID = 4;

                _unitOfWork.TransactionRepository.Add(transaction);



            }
           
            giftCertificate.StatusID = 2;
            giftCertificate.TransactionGroupID = transactionGroup;
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