using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Transaction
{
    public interface ITransactionService:IDisposable
    {
        bool AddTransaction(Models.Transaction item);
        bool UpdateTransaction(Models.Transaction item);

        bool DeleteTransaction(Models.Transaction item);
        bool DeleteById(Guid id);

        Models.Transaction FindById(Guid id);
        List<Models.Transaction> GetAllTransaction();
        List<Models.Transaction> FindBy(Expression<Func<Models.Transaction, bool>> predicate);
        List<Models.Transaction> PostPSNPPlan(RegionalPSNPPlan plan, Ration ration);
        List<Models.Transaction> PostHRDPlan(HRD plan, Ration ration);
        bool PostGiftCertificate(int giftCertificateId);
        bool PostDeliveryReconcileReceipt(int deliveryID);
        List<ProjectCode> getAllProjectByHubCommodity(int hubId, int commodityId);
        List<ShippingInstruction> getAllSIByHubCommodity(int hubId, int commodityId);
        List<ReceiptAllocation> getSIBalance(int hubId, int commodityId);
        List<ReceiptAllocation> getProjectBalance(int hubId, int commodityId);
        bool PostRequestAllocation(int requestId);
        bool PostSIAllocation(int requisitionID);
        bool PostDonationPlan(DonationPlanHeader donationPlanDetail);
        bool PostDistribution(int distributionId);
    }
}