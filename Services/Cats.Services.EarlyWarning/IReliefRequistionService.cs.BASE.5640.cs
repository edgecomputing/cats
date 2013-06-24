

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IReliefRequistionService
    {
        bool AddReliefRequistion(ReliefRequistion reliefRequistion);
        bool UpdateReliefRequistion(ReliefRequistion reliefRequistion);
        bool DeleteReliefRequistion(ReliefRequistion reliefRequistion);
        bool DeleteReliefRequistion(int id);
        List<ReliefRequistion> GetAllReliefRequistion();
        ReliefRequistion GetReliefRequistion(int reliefRequistionId);
    }
}
