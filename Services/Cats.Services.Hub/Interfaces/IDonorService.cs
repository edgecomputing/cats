
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Report;

namespace Cats.Services.Hub
{
    public interface IDonorService
    {

        bool AddDonor(Donor donor);
        bool DeleteDonor(Donor donor);
        bool DeleteById(int id);
        bool EditDonor(Donor donor);
        Donor FindById(int id);
        List<Donor> GetAllDonor();
        List<Donor> FindBy(Expression<Func<Donor, bool>> predicate);

        /// <summary>
        /// Gets all source donor for report.
        /// </summary>
        /// <returns></returns>
        List<DonorViewModel> GetAllSourceDonorForReport();

        /// <summary>
        /// Gets all responsible donor for report.
        /// </summary>
        /// <returns></returns>
        List<DonorViewModel> GetAllResponsibleDonorForReport();

        /// <summary>
        /// Determines whether [is code valid] [the specified donor code].
        /// </summary>
        /// <param name="DonorCode">The donor code.</param>
        /// <param name="DonorID">The donor ID.</param>
        /// <returns></returns>
        bool IsCodeValid(string DonorCode, int? DonorID);
    }
}


