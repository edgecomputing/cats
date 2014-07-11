using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public class HrdDonorCoverageService:IHrdDonorCoverageService
    {
        
       private readonly IUnitOfWork _unitOfWork;

       public HrdDonorCoverageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       public bool AddHrdDonorCoverage(Models.HrdDonorCoverage hrdDonorCoverage)
       {
           _unitOfWork.HrdDonorCoverageRepository.Add(hrdDonorCoverage);
           _unitOfWork.Save();
           return true;
       }

        public bool DeleteHrdDonorCoverage(Models.HrdDonorCoverage hrdDonorCoverage)
        {
            if (hrdDonorCoverage == null) return false;
            _unitOfWork.HrdDonorCoverageRepository.Delete(hrdDonorCoverage);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HrdDonorCoverageRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HrdDonorCoverageRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditHrdDonorCoverage(Models.HrdDonorCoverage hrdDonorCoverage)
        {
            _unitOfWork.HrdDonorCoverageRepository.Edit(hrdDonorCoverage);
            _unitOfWork.Save();
            return true;
        }

        public Models.HrdDonorCoverage FindById(int id)
        {
            return _unitOfWork.HrdDonorCoverageRepository.FindById(id);
        }

        public List<Models.HrdDonorCoverage> GetAllHrdDonorCoverage()
        {
            return _unitOfWork.HrdDonorCoverageRepository.GetAll();
        }

        public List<Models.HrdDonorCoverage> FindBy(System.Linq.Expressions.Expression<Func<Models.HrdDonorCoverage, bool>> predicate)
        {
            return _unitOfWork.HrdDonorCoverageRepository.FindBy(predicate);
        }

        public IEnumerable<Models.HrdDonorCoverage> Get(System.Linq.Expressions.Expression<Func<Models.HrdDonorCoverage, bool>> filter = null, Func<IQueryable<Models.HrdDonorCoverage>, IOrderedQueryable<Models.HrdDonorCoverage>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.HrdDonorCoverageRepository.Get(filter, orderBy, includeProperties);
        }
        public int NumberOfCoveredWoredas(int donorCoverageID)
        {
            var donorCoverageDetail =_unitOfWork.HrdDonorCoverageDetailRepository.FindBy(m => m.HRDDonorCoverageID == donorCoverageID);
            return donorCoverageDetail.Count;
        }
        public  DataTable TransposeData(IEnumerable<HrdDonorCoverageDetail> donorCoverageDetails, IEnumerable<RationDetail> rationDetails,int hrdID, string preferedWeight)
        {

            var dt = new DataTable("Transpose");

            var colRegion = new DataColumn("Region", typeof(string));
            colRegion.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colRegion);

            var colZone = new DataColumn("Zone", typeof(string));
            colZone.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colZone);

            var colWoreda = new DataColumn("Woreda", typeof(string));
            colWoreda.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colWoreda);


            var colNoBeneficiary = new DataColumn("NoBeneficiary", typeof(int));
            colNoBeneficiary.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colNoBeneficiary);


            var colDuration = new DataColumn("Duration", typeof(int));
            colDuration.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colDuration);

            //var colStartingMonth = new DataColumn("Starting Month", typeof(string));
            //colStartingMonth.ExtendedProperties["ID"] = -1;
            //dt.Columns.Add(colStartingMonth);
            var HRDID = hrdID;
            if (rationDetails != null)
            {
                foreach (var ds in rationDetails)
                {
                    var col = new DataColumn(ds.Commodity.Name.Trim() + "(" + preferedWeight.ToUpper().Trim() +")", typeof(decimal));
                    col.ExtendedProperties.Add("ID", ds.CommodityID);
                    dt.Columns.Add(col);
                }

                var col1 = new DataColumn("Total" + "(" + preferedWeight.ToUpper().Trim() + ")", typeof(decimal));
                col1.ExtendedProperties.Add("ID", "Total");
                dt.Columns.Add(col1);
                //int rowID = 0;
                //bool addRow = false;
                //var rowGroups = (from item in mydata select item.MyClassID).Distinct().ToList();
                foreach (var donorcoverageDetail in donorCoverageDetails)
                {
                    var dr = dt.NewRow();
                    //dr[colRequstDetailID] = requestDetail.RegionalRequestDetailID;
                    var durationAndBeneficary = GetWoredaBeneficiaryNumber(HRDID, donorcoverageDetail.WoredaID);
                    dr[colRegion] = donorcoverageDetail.AdminUnit.AdminUnit2.AdminUnit2.Name;
                    dr[colZone] = donorcoverageDetail.AdminUnit.AdminUnit2.Name;
                    dr[colWoreda] = donorcoverageDetail.AdminUnit.Name;
                    dr[colNoBeneficiary] = durationAndBeneficary.NumberOfBeneficiaries;
                    dr[colDuration] = durationAndBeneficary.DurationOfAssistance;
                    //dr[colStartingMonth] = RequestHelper.MonthName(hrdDetail.StartingMonth);
                    decimal total = 0;
                    decimal ration = 0;



                    var currentUnit = preferedWeight; ;


                    foreach (var rationDetail in rationDetails)
                    {

                        DataColumn col = null;
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (rationDetail.CommodityID.ToString() ==
                                column.ExtendedProperties["ID"].ToString())
                            {
                                col = column;
                                break;
                            }
                        }
                        if (col != null)
                        {
                            var currentUnitUpper = currentUnit.ToUpper().Trim();
                            if (currentUnitUpper == "MT")
                                ration = rationDetail.Amount / 1000;
                            else
                                ration = rationDetail.Amount / 100;


                            total += ration * durationAndBeneficary.NumberOfBeneficiaries
                                            * durationAndBeneficary.DurationOfAssistance;
                            dr[col.ColumnName] = ration*durationAndBeneficary.NumberOfBeneficiaries
                                                 *durationAndBeneficary.DurationOfAssistance;

                        }
                    }
                    dr[col1] = total;
                    dt.Rows.Add(dr);
                }
            }
            //var dta = (from DataRow row in dt.Rows select new
            //                                                  {

            //                                                  }).ToList();

            return dt;
        }
        public HRDDetail GetWoredaBeneficiaryNumber(int hrdID, int woredaID)
        {
            var hrdDetail = _unitOfWork.HRDDetailRepository.FindBy(m => m.HRDID == hrdID && m.WoredaID == woredaID).FirstOrDefault();
            if (hrdDetail != null)
            {
                return hrdDetail;
            }
            return null;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }


       
    }
}
