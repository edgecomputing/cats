using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Data.Hub;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{
   public  class CommonService:ICommonService
    {
       private readonly IUnitOfWork _unitOfWork;

       public CommonService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       public List<Commodity> GetAllParents()
       {
           return _unitOfWork.CommodityRepository.FindBy(t => t.ParentID == null);
       }
       public List<AdminUnit> GetRegions()
       {
           return _unitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitTypeID == 2);
       }
       public List<int?> GetYears()
       {
           return _unitOfWork.PeriodRepository.FindBy(y => y.Year.HasValue).Select(p => p.Year).Distinct().ToList();

       }
       public List<Program> GetAllProgram()
       {
           return _unitOfWork.ProgramRepository.GetAll();
       }
       public List<Unit> GetAllUnit()
       {
           return _unitOfWork.UnitRepository.GetAll();
       }
       public void Dispose()
       {
           _unitOfWork.Dispose();
       }


       public List<Donor> GetAllDonors()
       {
         return   _unitOfWork.DonorRepository.GetAll();
       }

       public List<Commodity> GetAllCommodity()
       {
         return  _unitOfWork.CommodityRepository.GetAll();
       }

       public List<CommodityType> GetAllCommodityType()
       {
         return   _unitOfWork.CommodityTypeRepository.GetAll();
       }
       public List<int?> GetMonths(int year)
       {

           return _unitOfWork.PeriodRepository.FindBy(y => y.Year == year && y.Month.HasValue).Select(p => p.Month).Distinct().ToList();
       }
    }
}
