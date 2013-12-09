using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Administration
{
    public interface IWoredaDonorService
    {
        bool AddWoredaDonor(WoredasByDonor woredasByDonor);
        bool DeleteWoredaDonor(WoredasByDonor woredasByDonor);
        bool DeleteById(int id);
        bool EditWoredaDonor(WoredasByDonor woredasByDonor);
        WoredasByDonor FindById(int id);
        List<WoredasByDonor> GetAllWoredaDonor();
        List<WoredasByDonor> FindBy(Expression<Func<WoredasByDonor, bool>> predicate);
        List<AdminUnit> GetWoredasNotYetAssigned();
        List<Donor> GetDonors();
    }
}
