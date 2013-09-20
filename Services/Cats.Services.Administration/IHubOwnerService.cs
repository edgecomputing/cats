using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;


namespace Cats.Services.Administration
{
    public interface IHubOwnerService
    {

        bool AddHubOwner(HubOwner hubOwner);
        bool DeleteHubOwner(HubOwner hubOwner);
        bool DeleteById(int id);
        bool EditHubOwner(HubOwner hubOwner);
        HubOwner FindById(int id);
        List<HubOwner> GetAllHubOwner();
        List<HubOwner> FindBy(Expression<Func<HubOwner, bool>> predicate);
         

    }
}


