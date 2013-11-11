
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
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

        /// <summary>
        /// Gets the monthly summary.
        /// </summary>
        /// <returns></returns>
        ObjectResult<RPT_MonthlyGiftSummary_Result> GetMonthlySummary();
        /// <summary>
        /// Gets the monthly summary ETA.
        /// </summary>
        /// <returns></returns>
        ObjectResult<RPT_MonthlyGiftSummary_Result> GetMonthlySummaryETA();
        /// <summary>
        /// Updates the specified gift certificate model.
        /// </summary>
        /// <param name="giftCertificateModel">The gift certificate model.</param>
        /// <param name="generateGiftCertificate">The generate gift certificate.</param>
        /// <param name="giftCertificateDetails">The gift certificate details.</param>
        /// <param name="list">The list.</param>
        void Update(GiftCertificate giftCertificateModel, List<GiftCertificateDetail> generateGiftCertificate, List<GiftCertificateDetail> giftCertificateDetails, List<GiftCertificateDetail> list);
        /// <summary>
        /// Finds the by SI number.
        /// </summary>
        /// <param name="SInumber">The S inumber.</param>
        /// <returns></returns>
        GiftCertificate FindBySINumber(string SInumber);
        /// <summary>
        /// Gets the SI balances.
        /// </summary>
        /// <returns></returns>
        List<SIBalance> GetSIBalances();
        IEnumerable<GiftCertificate> Get(Expression<Func<GiftCertificate, bool>> filter = null, Func<IQueryable<GiftCertificate>, IOrderedQueryable<GiftCertificate>> orderBy = null, string includeProperties = "");
    }
}


