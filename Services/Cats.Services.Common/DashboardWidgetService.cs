using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Common
{
    public class DashboardWidgetService : IDashboardWidgetService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DashboardWidgetService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        #endregion

        #region Implementation of IDashboardWidgetService

        public bool AddDashboardWidget(DashboardWidget dashboardWidget)
        {
            _unitOfWork.DashboardWidgetRepository.Add(dashboardWidget);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteDashboardWidget(DashboardWidget dashboardWidget)
        {
            if (dashboardWidget == null) return false;
            _unitOfWork.DashboardWidgetRepository.Delete(dashboardWidget);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DashboardWidgetRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DashboardWidgetRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditDashboardWidget(DashboardWidget dashboardWidget)
        {
            _unitOfWork.DashboardWidgetRepository.Edit(dashboardWidget);
            _unitOfWork.Save();
            return true;
        }

        public DashboardWidget FindById(int id)
        {
            return _unitOfWork.DashboardWidgetRepository.FindById(id);
        }

        public List<DashboardWidget> GetAllDashboardWidget()
        {
            return _unitOfWork.DashboardWidgetRepository.GetAll();
        }

        public List<DashboardWidget> FindBy(Expression<Func<DashboardWidget, bool>> predicate)
        {
            return _unitOfWork.DashboardWidgetRepository.FindBy(predicate);
        }

        public IEnumerable<DashboardWidget> Get(Expression<Func<DashboardWidget, bool>> filter = null, Func<IQueryable<DashboardWidget>, IOrderedQueryable<DashboardWidget>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.DashboardWidgetRepository.Get(filter, orderBy, includeProperties);
        }

        #endregion
    }
}
