using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public interface ITemplateService
    {
        bool AddTemplate(Template Template);
        bool DeleteTemplate(Template Template);
        bool DeleteById(int id);
        bool EditTemplate(Template Template);
        Template FindById(int id);
        List<Template> GetAllTemplate();
        List<Template> FindBy(Expression<Func<Template, bool>> predicate);
       
    }
}
