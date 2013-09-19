
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{
    public interface IReceiptAllocationService
    {

        bool AddReceiptAllocation(ReceiptAllocation receiptAllocation);
        bool DeleteReceiptAllocation(ReceiptAllocation receiptAllocation);
        bool DeleteById(int id);
        bool EditReceiptAllocation(ReceiptAllocation receiptAllocation);
        ReceiptAllocation FindById(int id);
        List<ReceiptAllocation> GetAllReceiptAllocation();
        List<ReceiptAllocation> FindBy(Expression<Func<ReceiptAllocation, bool>> predicate);
        ReceiptAllocation FindById(Guid id);

        // <summary>
        /// Finds the by SI number.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        List<ReceiptAllocation> FindBySINumber(string SINumber);

        /// <summary>
        /// Gets the uncommited allocation transaction.
        /// </summary>
        /// <param name="CommodityID">The commodity ID.</param>
        /// <param name="ShipingInstructionID">The shiping instruction ID.</param>
        /// <param name="HubID">The hub ID.</param>
        /// <returns></returns>
        Transaction GetUncommitedAllocationTransaction(int CommodityID, int ShipingInstructionID, int HubID);

        ///// <summary>
        ///// Attaches the transaction group.
        ///// </summary>
        ///// <param name="allocation">The allocation.</param>
        ///// <param name="transactionGroupID">The transaction group ID.</param>
        ///// <returns></returns>
        //bool AttachTransactionGroup(ReceiptAllocation allocation, int transactionGroupID);

        /// <summary>
        /// Gets the balance.
        /// </summary>
        /// <param name="siNumber">The SI number.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <returns></returns>
        decimal GetBalance(string siNumber, int commodityId);

        /// <summary>
        /// Gets the balance for SI.
        /// </summary>
        /// <param name="SInumber">The S inumber.</param>
        /// <param name="hubID"> </param>
        /// <returns></returns>
        decimal GetBalanceForSI(string SInumber);

        /// <summary>
        /// Gets the available commodities.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        List<Commodity> GetAvailableCommodities(string SINumber, int hubId);

        /// <summary>
        /// Gets the available commodities from unclosed.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <param name="hubId">The hub id.</param>
        /// <param name="commoditySourceId">The commodity source id.</param>
        /// <returns></returns>
        List<Commodity> GetAvailableCommoditiesFromUnclosed(string SINumber, int hubId, int? commoditySourceId);

        /// <summary>
        /// Gets the S is with out gift certificate.
        /// </summary>
        /// <returns></returns>
        List<string> GetSIsWithOutGiftCertificate();

        /// <summary>
        /// Gets the total allocation.
        /// </summary>
        /// <param name="siNumber">The SI number.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <param name="hubId">The hub id.</param>
        /// <param name="commoditySourceId">The commodity source id.</param>
        /// <returns></returns>
        decimal GetTotalAllocation(string siNumber, int commodityId, int hubId, int? commoditySourceId);

        /// <summary>
        /// Commits the receive allocation.
        /// </summary>
        /// <param name="checkedRecords">The checked records.</param>
        void CommitReceiveAllocation(string[] checkedRecords, UserProfile user);

        // List<ReceiptAllocation> GetUnclosedAllocations(int hubId, int? type);

        /// <summary>
        /// Closes the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        void CloseById(Guid id);

        //List<ReceiptAllocation> GetAllByType(int commoditySourceType);

        /// <summary>
        /// Gets the SIs with out gift certificate.
        /// </summary>
        /// <param name="commoditySoureType">Type of the commodity soure.</param>
        /// <returns></returns>
        List<string> GetSIsWithOutGiftCertificate(int commoditySoureType);

        /// <summary>
        /// Gets the unclosed allocations detached.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <param name="type">The type.</param>
        /// <param name="closedToo">The closed too.</param>
        /// <param name="weightMeasurmentCode">The weight measurment code.</param>
        /// <returns></returns>
        List<ReceiptAllocation> GetUnclosedAllocationsDetached(int hubId, int type, bool? closedToo, string weightMeasurmentCode, int? CommodityType);

        /// <summary>
        /// Gets all by type merged.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns></returns>
        List<ReceiptAllocation> GetAllByTypeMerged(int sourceType);
        /// <summary>
        /// Determines whether [is SIN source] [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="siNumber">The SI number.</param>
        /// <returns>
        ///   <c>true</c> if [is SIN source] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        bool IsSINSource(int source, string siNumber);

        /// <summary>
        /// Gets the received already.
        /// </summary>
        /// <param name="theCurrentAllocation">The current allocation.</param>
        /// <returns></returns>
        decimal GetReceivedAlready(ReceiptAllocation theCurrentAllocation);

        decimal GetReceivedAlreadyInUnit(ReceiptAllocation theCurrentAllocation);

        /// <summary>
        /// Gets the SI balance for commodity.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <returns></returns>
        List<SIBalance> GetSIBalanceForCommodity(int hubID, int commodityId);

        List<SIBalance> GetSIBalanceForCommodityInUnit(int hubID, int commodityId);



        bool DeleteByID(Guid id);

        ReceiptAllocation FindByID(Guid id);
   
    }
}


