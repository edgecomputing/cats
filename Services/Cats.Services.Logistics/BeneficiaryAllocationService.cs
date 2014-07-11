using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models.ViewModels;

namespace Cats.Services.Logistics
{
    public class BeneficiaryAllocationService : IBeneficiaryAllocationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BeneficiaryAllocationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<BeneficiaryAllocation> GetBenficiaryAllocation(Expression<Func<BeneficiaryAllocation, bool>> predicate = null)
        {
            //TODO:When status implemented 4 means hub and project code assigned
            var requsitions = _unitOfWork.ReliefRequisitionDetailRepository.Get(t => t.ReliefRequisition.Status == 4).ToList();

            var benficiaryAlloation = (from itm in requsitions
                                       select new BeneficiaryAllocation()
                                                  {
                                                      RegionID = itm.ReliefRequisition.RegionID,
                                                      ZoneID = itm.ReliefRequisition.ZoneID,
                                                      WoredaID = itm.FDP.AdminUnit.AdminUnitID,
                                                      Region = itm.ReliefRequisition.RegionID.HasValue ? itm.ReliefRequisition.AdminUnit.Name : string.Empty,
                                                      Zone = itm.ReliefRequisition.RegionID.HasValue ? itm.ReliefRequisition.AdminUnit1.Name : string.Empty,
                                                      Woreda = itm.FDP.AdminUnit.Name,
                                                      FDPID = itm.FDPID,
                                                      FDP = itm.FDP.Name,
                                                      DonorID = itm.DonorID,
                                                      Donor = itm.DonorID.HasValue ? itm.Donor.Name : string.Empty,
                                                      RequisitionID = itm.RequisitionID,
                                                      RequisitionNo = itm.ReliefRequisition.RequisitionNo,
                                                      CommodityID = itm.CommodityID,
                                                      Commodity = itm.Commodity.Name,
                                                      Amount = itm.Amount,
                                                      BeneficiaryNo = itm.BenficiaryNo,
                                                      ProgramID = itm.ReliefRequisition.ProgramID,
                                                      Program = itm.ReliefRequisition.Program.Name,
                                                      Round=itm.ReliefRequisition.Round.HasValue ?itm.ReliefRequisition.Round.Value :0,
                                                      Year=itm.ReliefRequisition.RequestedDate.Value.Year,
                                                      Month =itm.ReliefRequisition.RequestedDate.Value.ToString("MMM")

                                                  });

            IQueryable<BeneficiaryAllocation> query = benficiaryAlloation.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;

        }
        public IEnumerable<BeneficiaryAllocation> GetBenficiaryAllocationPrintOut(int id, Expression<Func<BeneficiaryAllocation, bool>> predicate = null)
        {
            //TODO:When status implemented 4 means hub and project code assigned
            var requsitions = _unitOfWork.ReliefRequisitionDetailRepository.Get(t => t.RequisitionID==id).ToList();

            var benficiaryAlloation = (from itm in requsitions
                                       select new BeneficiaryAllocation()
                                       {
                                           RegionID = itm.ReliefRequisition.RegionID,
                                           ZoneID = itm.ReliefRequisition.ZoneID,
                                           WoredaID = itm.FDP.AdminUnit.AdminUnitID,
                                           Region = itm.ReliefRequisition.RegionID.HasValue ? itm.ReliefRequisition.AdminUnit.Name : string.Empty,
                                           Zone = itm.ReliefRequisition.RegionID.HasValue ? itm.ReliefRequisition.AdminUnit1.Name : string.Empty,
                                           Woreda = itm.FDP.AdminUnit.Name,
                                           FDPID = itm.FDPID,
                                           FDP = itm.FDP.Name,
                                           DonorID = itm.DonorID,
                                           Donor = itm.DonorID.HasValue ? itm.Donor.Name : string.Empty,
                                           RequisitionID = itm.RequisitionID,
                                           RequisitionNo = itm.ReliefRequisition.RequisitionNo,
                                           CommodityID = itm.CommodityID,
                                           Commodity = itm.Commodity.Name,
                                           Amount = itm.Amount,
                                           BeneficiaryNo = itm.BenficiaryNo,
                                           ProgramID = itm.ReliefRequisition.ProgramID,
                                           Program = itm.ReliefRequisition.Program.Name,
                                           Round = itm.ReliefRequisition.Round.HasValue ? itm.ReliefRequisition.Round.Value : 0,
                                           Year = itm.ReliefRequisition.RequestedDate.Value.Year,
                                           Month = itm.ReliefRequisition.RequestedDate.Value.ToString("MMM")

                                       });

            IQueryable<BeneficiaryAllocation> query = benficiaryAlloation.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;

        }
    }
}
