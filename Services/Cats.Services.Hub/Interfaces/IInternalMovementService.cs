
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;

namespace Cats.Services.Hub
{
    public interface IInternalMovementService
    {

        bool AddInternalMovement(InternalMovement entity);
        bool DeleteInternalMovement(InternalMovement entity);
        bool DeleteById(int id);
        bool EditInternalMovement(InternalMovement entity);
        InternalMovement FindById(int id);
         List<InternalMovementLogViewModel> GetAllInternalMovmentLog();
        List<InternalMovement> FindBy(Expression<Func<InternalMovement, bool>> predicate);

       void AddNewInternalMovement(InternalMovementViewModel viewModel, UserProfile user);
      

    }
}


