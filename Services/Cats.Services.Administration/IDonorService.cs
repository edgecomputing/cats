using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Administration
{
   public interface IDonorService:IDisposable
    {
        bool AddDonor(Donor donor);
        bool DeleteDonor(Donor donor);
        bool DeleteById(int id);
        bool EditDonor(Donor donor);
        Donor FindById(int id);
        List<Donor> GetAllDonor();
        List<Donor> FindBy(Expression<Func<Donor, bool>> predicate);
    }
}
