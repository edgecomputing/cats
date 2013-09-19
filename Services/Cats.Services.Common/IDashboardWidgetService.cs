using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Common
{
    public interface IDashboardWidgetService :IDisposable
    {
        bool AddDashboardWidget(DashboardWidget dashboardWidget);
        bool DeleteDashboardWidget(DashboardWidget dashboardWidget);
        bool DeleteById(int id);
        bool EditDashboardWidget(DashboardWidget dashboardWidget);
        DashboardWidget FindById(int id);
        List<DashboardWidget> GetAllDashboardWidget();
        List<DashboardWidget> FindBy(Expression<Func<DashboardWidget, bool>> predicate);
        IEnumerable<DashboardWidget> Get(
                   Expression<Func<DashboardWidget, bool>> filter = null,
                   Func<IQueryable<DashboardWidget>, IOrderedQueryable<DashboardWidget>> orderBy = null,
                   string includeProperties = "");
    }
}
