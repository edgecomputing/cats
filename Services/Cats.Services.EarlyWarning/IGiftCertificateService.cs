using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IGiftCertificateService:IDisposable
    {

        bool AddGiftCertificate(GiftCertificate giftCertificate);
        bool DeleteGiftCertificate(GiftCertificate giftCertificate);
        bool DeleteById(int id);
        bool EditGiftCertificate(GiftCertificate giftCertificate);
        GiftCertificate FindById(int id);
        List<GiftCertificate> GetAllGiftCertificate();
        List<GiftCertificate> FindBy(Expression<Func<GiftCertificate, bool>> predicate);

        GiftCertificate FindBySINumber(string siNumber);
        bool IsSINumberNewOrEdit(string siNumber, int giftCertificateId);

        IEnumerable<GiftCertificate> Get(Expression<Func<GiftCertificate, bool>> filter = null,
                                         Func<IQueryable<GiftCertificate>, IOrderedQueryable<GiftCertificate>> orderBy = null,
                                         string includeProperties = "");


    }
}


