using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Administration
{
    public interface IWoredaDonorService
    {
        bool AddWoredaDonor(HrdDonorCovarage hrdDonorCovarage);
        bool DeleteWoredaDonor(HrdDonorCovarage hrdDonorCovarage);
        bool DeleteById(int id);
        bool EditWoredaDonor(HrdDonorCovarage hrdDonorCovarage);
        HrdDonorCovarage FindById(int id);
        List<HrdDonorCovarage> GetAllWoredaDonor();
        List<HrdDonorCovarage> FindBy(Expression<Func<HrdDonorCovarage, bool>> predicate);
        List<AdminUnit> GetWoredasNotYetAssigned();
        List<Donor> GetDonors();
    }
}
