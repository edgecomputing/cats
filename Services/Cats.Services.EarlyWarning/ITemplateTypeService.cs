using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface ITemplateTypeTypeService
    {
        bool AddTemplateType(TemplateType templateType);
        bool DeleteTemplateType(TemplateType templateType);
        bool DeleteById(int id);
        bool EditTemplateType(TemplateType templateType);
        TemplateType FindById(int id);
        List<TemplateType> GetAllTemplateType();
        List<TemplateType> FindBy(Expression<Func<TemplateType, bool>> predicate);
    }
}
