using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.Common
{
    public class NeedAssessmentSummaryService: INeedAssessmentSummaryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NeedAssessmentSummaryService()
        {
            this._unitOfWork = new UnitOfWork();
        }

        public List<NeedAssessmentSummary> NeedAssessmentByRegion(string regionName)
        {
            var regionNeedAssessment = _unitOfWork.NeedAssessmetSummaryRepository.FindBy(r => r.RegionName == regionName);
            //return regionNeedAssessment.ToList();
            return (from _need in regionNeedAssessment orderby _need.Year select _need).ToList();
        }

        public List<NeedASummary> NeedAssessmentByYear(int year)
        {
            var yearNeedAssessment = _unitOfWork.NeedAssessmetSummaryRepository.FindBy(r => r.Year == year );

            var q = from y in yearNeedAssessment
                    orderby y.RegionName
                    group y by y.RegionName into No
                    select new NeedASummary()
                        {
                            RegionName = No.Key,                          //from r in y  
                            Belg_Beneficiaries = No.Where(t => t.Season == "Belg").Sum(t => t.TotalBeneficiaries),
                            Meher_Beneficiaries = No.Where(t => t.Season == "Meher").Sum(t => t.TotalBeneficiaries)
                        };

            return q.ToList();
        }

        public List<int> GetYears() {

            var years = _unitOfWork.NeedAssessmetSummaryRepository.GetAll();
            return (from year in years
                    orderby year.Year descending
                    select year.Year).Distinct().ToList();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}