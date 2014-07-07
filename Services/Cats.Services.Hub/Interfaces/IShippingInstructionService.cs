
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Common;

namespace Cats.Services.Hub
{
    public interface IShippingInstructionService:IDisposable
    {

        bool AddShippingInstruction(ShippingInstruction shippingInstruction);
        bool DeleteShippingInstruction(ShippingInstruction shippingInstruction);
        bool DeleteById(int id);
        bool EditShippingInstruction(ShippingInstruction shippingInstruction);
        ShippingInstruction FindById(int id);
        List<ShippingInstruction> GetAllShippingInstruction();
        List<ShippingInstruction> FindBy(Expression<Func<ShippingInstruction, bool>> predicate);

        /// <summary>
        /// Gets the shiping instruction id.
        /// If the shipping instruction id is not found in the database, this will not create it,
        /// </summary>
        /// <param name="si">The SI.</param>
        /// <returns></returns>
        int GetShipingInstructionId(string si);

        /// <summary>
        /// Determines whether the specified SI number has balance.
        /// </summary>
        /// <param name="SiNumber">The SI number.</param>
        /// <param name="FDPID">The FDPID.</param>
        /// <returns>
        ///   <c>true</c> if the specified SI number has balance; otherwise, <c>false</c>.
        /// </returns>
        bool HasBalance(string SiNumber, int FDPID);

        /// <summary>
        /// Gets the SI number id with create.
        /// Note: if the SI Number doesn't exist, this will create it.
        /// </summary>
        /// <param name="SiNumber">The SI number.</param>
        /// <returns></returns>
        ShippingInstruction GetSINumberIdWithCreate(string SiNumber);

        /// <summary>
        /// Gets all shipping instruction for report.
        /// </summary>
        /// <returns></returns>
        List<ShippingInstructionViewModel> GetAllShippingInstructionForReport();

        /// <summary>
        /// Gets the shipping instructions for project code.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <param name="projectCodeId">The project code id.</param>
        /// <returns></returns>
        List<ShippingInstructionViewModel> GetShippingInstructionsForProjectCode(int hubId, int projectCodeId);

        /// <summary>
        /// Gets the shipping instructions with balance.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <returns></returns>
        List<ShippingInstruction> GetShippingInstructionsWithBalance(int hubID, int commodityId);

        /// <summary>
        /// Gets the balance.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <param name="shippingInstructionID">The shipping instruction ID.</param>
        /// <returns></returns>
        SIBalance GetBalance(int hubID, int commodityId, int shippingInstructionID);

        SIBalance GetBalanceInUnit(int hubID, int commodityId, int shippingInstructionID);
        List<ShippingInstructionViewModel> GetShippingInstructionsForProjectCode(int hubId, int projectCodeId, int parentCommodityID);

    }
}


