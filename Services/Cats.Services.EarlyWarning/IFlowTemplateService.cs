using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;
namespace Cats.Services.EarlyWarning
{
    public interface IFlowTemplateService
    {
        bool Add(FlowTemplate item);
        bool Update(FlowTemplate item);
        bool Delete(FlowTemplate item);
        bool DeleteById(int id);
        FlowTemplate FindById(int id);
        List<FlowTemplate> GetAll();
        List<FlowTemplate> FindBy(Expression<Func<FlowTemplate, bool>> predicate);
    }
}