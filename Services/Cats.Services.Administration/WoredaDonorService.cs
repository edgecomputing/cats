using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Administration
{
   public class WoredaDonorService:IWoredaDonorService
    {

       private readonly IUnitOfWork _unitOfWork;

       public WoredaDonorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       public bool AddWoredaDonor(HrdDonorCovarage hrdDonorCovarage)
       {
           _unitOfWork.WoredaByDonorRepository.Add(hrdDonorCovarage);
           _unitOfWork.Save();
           return true;
       }

       public bool DeleteWoredaDonor(HrdDonorCovarage hrdDonorCovarage)
       {
           if (hrdDonorCovarage == null) return false;
           _unitOfWork.WoredaByDonorRepository.Delete(hrdDonorCovarage);
           _unitOfWork.Save();
           return true;
       }

       public bool DeleteById(int id)
       {
           var entity = _unitOfWork.WoredaByDonorRepository.FindById(id);
           if (entity == null) return false;
           _unitOfWork.WoredaByDonorRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }

       public bool EditWoredaDonor(HrdDonorCovarage hrdDonorCovarage)
       {
           _unitOfWork.WoredaByDonorRepository.Edit(hrdDonorCovarage);
           _unitOfWork.Save();
           return true;
       }

       public HrdDonorCovarage FindById(int id)
       {
           return _unitOfWork.WoredaByDonorRepository.FindById(id);
       }

       public List<HrdDonorCovarage> GetAllWoredaDonor()
       {
           return _unitOfWork.WoredaByDonorRepository.GetAll();
       }

       public List<HrdDonorCovarage> FindBy(Expression<Func<HrdDonorCovarage, bool>> predicate)
       {
           return _unitOfWork.WoredaByDonorRepository.FindBy(predicate);
       }


       public List<AdminUnit> GetWoredasNotYetAssigned()
       {
           var allWoredas = _unitOfWork.AdminUnitRepository.Get(a => a.AdminUnitTypeID == 4);
           var woredasAlreadyDonorAssigned = GetAllWoredaDonor();
           //var woredas = from woreda in allWoredas
           //              where
           //                  !(woredasAlreadyDonorAssigned.Any(
           //                      woredaAssigned => woredaAssigned.WoredaId == woreda.AdminUnitID))
           //              select woreda;

           return allWoredas.ToList();


       }

       public List<Donor> GetDonors()
       {
           return _unitOfWork.DonorRepository.GetAll();
       }
    }
}
