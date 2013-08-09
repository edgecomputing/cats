using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IGiftCertificateService
    {

        bool AddGiftCertificate(GiftCertificate giftCertificate);
        bool DeleteGiftCertificate(GiftCertificate giftCertificate);
        bool DeleteById(int id);
        bool EditGiftCertificate(GiftCertificate giftCertificate);
        GiftCertificate FindById(int id);
        List<GiftCertificate> GetAllGiftCertificate();
        List<GiftCertificate> FindBy(Expression<Func<GiftCertificate, bool>> predicate);


    }
}


