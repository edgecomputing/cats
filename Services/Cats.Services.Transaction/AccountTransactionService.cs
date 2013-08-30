using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Transaction
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountTransactionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddAccountTransaction(AccountTransaction item)
        {
            _unitOfWork.AccountTransactionRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateAccountTransaction(AccountTransaction item)
        {
            if (item == null) return false;
            _unitOfWork.AccountTransactionRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteAccountTransaction(AccountTransaction item)
        {
            if (item == null) return false;
            _unitOfWork.AccountTransactionRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(Guid id)
        {
            var item = _unitOfWork.AccountTransactionRepository.FindById(id);
            return DeleteAccountTransaction(item);
        }
        public AccountTransaction FindById(Guid id)
        {
            return _unitOfWork.AccountTransactionRepository.FindById(id);
        }
        public List<AccountTransaction> GetAllAccountTransaction()
        {
            return _unitOfWork.AccountTransactionRepository.GetAll();

        }
        public List<AccountTransaction> FindBy(Expression<Func<AccountTransaction, bool>> predicate)
        {
            return _unitOfWork.AccountTransactionRepository.FindBy(predicate);

        }
        public IEnumerable<AccountTransaction> PostTransaction(IEnumerable<AccountTransaction> entries)
        {
            Guid transactionGroupID = Guid.NewGuid();
            DateTime TransactionDate = DateTime.Now;
            foreach (AccountTransaction entry in entries)
            {
                entry.TransactionDate = TransactionDate;
                entry.TransactionGroupID = transactionGroupID;
                entry.AccountTransactionID = Guid.NewGuid();
                _unitOfWork.AccountTransactionRepository.Add(entry);
                
            }
            _unitOfWork.Save();
            return entries;
        }

        public List<AccountTransaction> PostHRDPlan(HRD hrd, Ration ration)
        {
            List<AccountTransaction> entries = new List<AccountTransaction>();
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

                        AccountTransaction entry2 = new AccountTransaction
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
                        AccountTransaction entry1 = new AccountTransaction
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
        public List<AccountTransaction> PostPSNPPlan(RegionalPSNPPlan plan, Ration ration)
        {
            List<AccountTransaction> entries = new List<AccountTransaction>();
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
                       
                        AccountTransaction entry2 = new AccountTransaction
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
                        AccountTransaction entry1 = new AccountTransaction
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
            foreach (var giftCertificateDetail in giftCertificate.GiftCertificateDetails)
            {
                var transaction = new AccountTransaction();
                transaction.AccountTransactionID = Guid.NewGuid();
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
                _unitOfWork.AccountTransactionRepository.Add(transaction);

                transaction = new AccountTransaction();
                transaction.AccountTransactionID = Guid.NewGuid();
                transaction.ProgramID = giftCertificate.ProgramID;
                transaction.DonorID = giftCertificate.DonorID;
                transaction.QuantityInMT = -giftCertificateDetail.WeightInMT;
                transaction.TransactionGroupID = transactionGroup;
                transaction.TransactionDate = transactionDate;
                transaction.QuantityInUnit = giftCertificateDetail.WeightInMT;
                transaction.UnitID = 1;
                transaction.LedgerID = 4;

                _unitOfWork.AccountTransactionRepository.Add(transaction);



            }

            giftCertificate.StatusID = 2;
            _unitOfWork.Save();
            return true;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}