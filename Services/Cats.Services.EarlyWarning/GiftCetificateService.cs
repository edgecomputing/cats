using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{

    public class GiftCertificateService : IGiftCertificateService
    {
        private readonly IUnitOfWork _unitOfWork;


        public GiftCertificateService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddGiftCertificate(GiftCertificate giftCertificate)
        {
            _unitOfWork.GiftCertificateRepository.Add(giftCertificate);
            _unitOfWork.Save();
            return true;

        }
        public bool EditGiftCertificate(GiftCertificate giftCertificate)
        {
            _unitOfWork.GiftCertificateRepository.Edit(giftCertificate);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteGiftCertificate(GiftCertificate giftCertificate)
        {
            if (giftCertificate == null) return false;
            _unitOfWork.GiftCertificateRepository.Delete(giftCertificate);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.GiftCertificateRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.GiftCertificateRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<GiftCertificate> GetAllGiftCertificate()
        {
            return _unitOfWork.GiftCertificateRepository.GetAll();
        }
        public GiftCertificate FindById(int id)
        {
            return _unitOfWork.GiftCertificateRepository.FindById(id);
        }
        public List<GiftCertificate> FindBy(Expression<Func<GiftCertificate, bool>> predicate)
        {
            return _unitOfWork.GiftCertificateRepository.FindBy(predicate);
        }
        #endregion


        

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}

 
      
