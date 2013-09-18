using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Data.Hub;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{
    public class TransporterService : ITransporterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransporterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AddTransporter(Transporter entity)
       {
           _unitOfWork.TransporterRepository.Add(entity);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditTransporter(Transporter entity)
       {
           _unitOfWork.TransporterRepository.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteTransporter(Transporter entity)
        {
             if(entity==null) return false;
           _unitOfWork.TransporterRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.TransporterRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.TransporterRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<Transporter> GetAllTransporter()
       {
           return _unitOfWork.TransporterRepository.GetAll();
       } 
       public Transporter FindById(int id)
       {
           return _unitOfWork.TransporterRepository.FindById(id);
       }
       public List<Transporter> FindBy(Expression<Func<Transporter, bool>> predicate)
       {
           return _unitOfWork.TransporterRepository.FindBy(predicate);
       }
       
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }

        public bool IsNameValid(int? transporterID, string name)
        {
             
       
           var trans = _unitOfWork.TransporterRepository.FindBy(t=>t.Name == name && t.TransporterID!=transporterID).Any();
           if (trans == false) return false;
           return true;

       
        }

       
    }
       
}




      
      