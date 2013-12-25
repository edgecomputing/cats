using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public partial interface INeedAssessmentService
    {

        bool AddNeedAssessment(NeedAssessment needAssessment);
        bool DeleteNeedAssessment(NeedAssessment needAssessment);
        bool DeleteById(int id);
        bool EditNeedAssessment(NeedAssessment needAssessment);
        NeedAssessment FindById(int id);
        List<NeedAssessment> GetAllNeedAssessment();
        List<NeedAssessment> FindBy(Expression<Func<NeedAssessment, bool>> predicate);
        //IEnumerable<NeedAssessmentHeaderViewModel>  ReturnViewModel();

        //IEnumerable<NeedAssessmentDao> ReturnNeedAssessmentHeaderViewModel(int region);
        //IEnumerable<NeedAssessmentWoredaDao> ReturnNeedAssessmentDetailViewModel(int region);
        //IEnumerable<NeedAssessmentDetail> GetDetail(IEnumerable<NeedAssessmentViewModel> detailViewModel);
        List<string> GetRegionsFromNeedAssessment();
        List<string> GetZonesFromNeedAssessment();
        List<string> GetSeasonFromNeedAssessment();
        List<NeedAssessmentDao> GetListOfZones();
        List<NeedAssessmentWoredaDao> GetListOfWoredas(int zoneId);
         bool GenerateDefefaultData(NeedAssessment needAssessment);
        void AddNeedAssessment(int planID, int regionID, int seasonID, int userID, int needAssessmentTypeID);
        
        IOrderedEnumerable<RegionsViewModel> GetRegions();
        IOrderedEnumerable<RegionsViewModel> GetZoness(int region);
        //IEnumerable<NeedAssessmentHeaderViewModel> ReturnViewModelApproved();
        bool IsNeedAssessmentUsedInHrd(int planId);
    }
}


