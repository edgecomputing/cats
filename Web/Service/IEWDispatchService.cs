using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cats.Models;


namespace Cats.Service
{
    public interface IEWDispatchService
    {
        
        IEnumerable<AllocationModelDetail> GetAllocationModelDetail();
    }
}