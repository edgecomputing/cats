using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    
    public interface IRequisitionDetailService
    {
        bool AddRequisitionDetail(ReliefRequisitionDetail reliefRequisitionDetail);
        bool UpdateRequisitionDetail(ReliefRequisitionDetail reliefRequisitionDetail);
        bool DeleteRequisitionDetail(ReliefRequisitionDetail   reliefRequistionDetail);
        bool DeleteRequisitionDetail(int id);
        List<ReliefRequisitionDetail> GetAllReliefRequistion();
        ReliefRequisitionDetail GetReliefRequisitionDetail(int reliefRequisitionDetailId);
        
    }
}
