

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class GiftCertificateDetailService : IGiftCertificateDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public GiftCertificateDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddGiftCertificateDetail(GiftCertificateDetail giftCertificateDetail)
        {
            _unitOfWork.GiftCertificateDetailRepository.Add(giftCertificateDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditGiftCertificateDetail(GiftCertificateDetail giftCertificateDetail)
        {
            _unitOfWork.GiftCertificateDetailRepository.Edit(giftCertificateDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteGiftCertificateDetail(GiftCertificateDetail giftCertificateDetail)
        {
            if (giftCertificateDetail == null) return false;
            _unitOfWork.GiftCertificateDetailRepository.Delete(giftCertificateDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.GiftCertificateDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.GiftCertificateDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<GiftCertificateDetail> GetAllGiftCertificateDetail()
        {
            return _unitOfWork.GiftCertificateDetailRepository.GetAll();
        }
        public GiftCertificateDetail FindById(int id)
        {
            return _unitOfWork.GiftCertificateDetailRepository.FindById(id);
        }
        public List<GiftCertificateDetail> FindBy(Expression<Func<GiftCertificateDetail, bool>> predicate)
        {
            return _unitOfWork.GiftCertificateDetailRepository.FindBy(predicate);
        }
        #endregion
        public List<string> GetUncommitedSIs()
        {
            var list = _unitOfWork.GiftCertificateDetailRepository.Get(
                p => !(p.ReceiptAllocations.Any())
                    || (p.ReceiptAllocations.Any(x => x.IsCommited == false)))
                    .Select(p => p.GiftCertificate.ShippingInstruction.Value).ToList();

            return list;     //.Union(db.ReceiptAllocations.Where(p=>p.SINumber))

        }


        public bool IsBillOfLoadingDuplicate(string billOfLoading)
        {
            return _unitOfWork.GiftCertificateDetailRepository.Get(p => p.BillOfLoading == billOfLoading).Any();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


