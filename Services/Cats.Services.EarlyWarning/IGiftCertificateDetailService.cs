using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IGiftCertificateDetailService
    {

        bool AddGiftCertificateDetail(GiftCertificateDetail giftCertificate);
        bool DeleteGiftCertificateDetail(GiftCertificateDetail giftCertificate);
        bool DeleteById(int id);
        bool EditGiftCertificateDetail(GiftCertificateDetail giftCertificate);
        GiftCertificateDetail FindById(int id);
        List<GiftCertificateDetail> GetAllGiftCertificateDetail();
        List<GiftCertificateDetail> FindBy(Expression<Func<GiftCertificateDetail, bool>> predicate);

        IEnumerable<GiftCertificateDetail> Get(
            Expression<Func<GiftCertificateDetail, bool>> filter = null,
            Func<IQueryable<GiftCertificateDetail>, IOrderedQueryable<GiftCertificateDetail>> orderBy = null,
            string includeProperties = "");


    }
}


