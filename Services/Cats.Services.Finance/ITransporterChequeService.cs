using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Finance
{
    public interface ITransporterChequeService
    {

        bool AddTransporterCheque(TransporterCheque transporterCheque);
        bool DeleteTransporterCheque(TransporterCheque transporterCheque);
        bool DeleteById(int id);
        bool EditTransporterCheque(TransporterCheque transporterCheque);
        TransporterCheque FindById(int id);
        List<TransporterCheque> GetAllTransporterCheque();
        List<TransporterCheque> FindBy(Expression<Func<TransporterCheque, bool>> predicate);


    }
}


      
