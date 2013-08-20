using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;
namespace Cats.Services.EarlyWarning
{
    public interface IProcessTemplateService
    {
        bool Add(ProcessTemplate item);
        bool Update(ProcessTemplate item);
        bool Delete(ProcessTemplate item);
        bool DeleteById(int id);
        ProcessTemplate FindById(int id);
        List<ProcessTemplate> GetAll();
        List<ProcessTemplate> FindBy(Expression<Func<ProcessTemplate, bool>> predicate);
    }
}