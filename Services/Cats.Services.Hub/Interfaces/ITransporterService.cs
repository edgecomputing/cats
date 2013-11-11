using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{
    public interface ITransporterService:IDisposable
    {

        bool AddTransporter(Transporter entity);
        bool DeleteTransporter(Transporter entity);
        bool DeleteById(int id);
        bool EditTransporter(Transporter entity);
        Transporter FindById(int id);
        List<Transporter> GetAllTransporter();
        List<Transporter> FindBy(Expression<Func<Transporter, bool>> predicate);

        bool IsNameValid(int? transporterID, string name);
       
    }
}


      

      
