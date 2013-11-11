using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Common
{
   public interface INotificationService
    {
        bool AddNotification(Notification Notification);
        bool DeleteNotification(Notification notification);
        bool DeleteById(int id);
        bool EditNotification(Notification Notification);
        Notification FindById(int id);
        List<Notification> GetAllNotification();
        List<Notification> FindBy(Expression<Func<Notification, bool>> predicate);
       
    }
}
