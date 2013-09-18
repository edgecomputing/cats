
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;
using Cats.Models.Hub.ViewModels;

namespace Cats.Services.Hub
{
    public interface IAdjustmentService
    {

        bool AddAdjustment(Adjustment adjustment);
        bool DeleteAdjustment(Adjustment adjustment);
        bool DeleteById(int id);
        bool EditAdjustment(Adjustment adjustment);
        Adjustment FindById(int id);
        List<Adjustment> GetAllAdjustment();
        List<Adjustment> FindBy(Expression<Func<Adjustment, bool>> predicate);
        List<LossAndAdjustmentLogViewModel> GetAllLossAndAdjustmentLog(int hubId);
        void AddNewLossAndAdjustment(LossesAndAdjustmentsViewModel viewModel, UserProfile user);
    }
}


