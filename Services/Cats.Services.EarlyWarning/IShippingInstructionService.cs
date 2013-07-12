using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IShippingInstructionService
    {
         bool AddShippingInstruction(ShippingInstruction shippingInstruction);
         bool EditShippingInstruction(ShippingInstruction shippingInstruction);
         bool DeleteShippingInstruction(ShippingInstruction shippingInstruction);
         bool DeleteById(int id);
         List<ShippingInstruction> GetAllShippingInstruction();
         ShippingInstruction FindById(int id);
         List<ShippingInstruction> FindBy(Expression<Func<ShippingInstruction, bool>> predicate);
    }
}
