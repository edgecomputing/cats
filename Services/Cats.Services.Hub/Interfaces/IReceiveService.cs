
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;
using Cats.Models.Hubs.ViewModels.Report;


namespace Cats.Services.Hub
{
    public interface IReceiveService : IDisposable
    {

        bool AddReceive(Receive entity);
        bool DeleteReceive(Receive entity);
        bool DeleteById(int id);
        bool EditReceive(Receive entity);
        Receive FindById(int id);
        List<Receive> GetAllReceive();
        List<Receive> FindBy(Expression<Func<Receive, bool>> predicate);
        List<Receive> ByHubId(int hubId);
         List<PortViewModel> GetALlPorts();
        Receive FindById(System.Guid id);
        List<ReceiveViewModelDto> ByHubIdAndAllocationIDetached(int hubId, Guid receiptAllocationId);





    }
}


