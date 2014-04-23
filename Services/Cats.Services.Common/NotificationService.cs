using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;

namespace Cats.Services.Common
{
   public class NotificationService:INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        #region Default Service Implementation
        public bool AddNotification(Notification notification)
        {
            _unitOfWork.NotificationRepository.Add(notification);
            _unitOfWork.Save();
            return true;

        }
        public bool EditNotification(Notification notification)
        {
            _unitOfWork.NotificationRepository.Edit(notification);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteNotification(Notification notification)
        {
            if (notification == null) return false;
            _unitOfWork.NotificationRepository.Delete(notification);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.NotificationRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.NotificationRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Notification> GetAllNotification()
        {
            return _unitOfWork.NotificationRepository.GetAll();
        }
        public Notification FindById(int id)
        {
            return _unitOfWork.NotificationRepository.FindById(id);
        }
        public List<Notification> FindBy(Expression<Func<Notification, bool>> predicate)
        {
            return _unitOfWork.NotificationRepository.FindBy(predicate);
        }

      
        #endregion

        #region notification for hub managers

        public bool AddNotificationForHubManagersFromTransportOrder(string destinationUrl,int transportOrderId, string transportOrderNo)
        {
            try
            {
                
                var notification = new Notification
                {
                    Text = "Transport Order No:" + transportOrderNo,
                    CreatedDate = DateTime.Now.Date,
                    IsRead = false,
                    Id = 1,
                    RecordId = transportOrderId,
                    Url = destinationUrl,
                    TypeOfNotification = "New Transport Order",
                    Application = Application.HUB
                };
                AddNotification(notification);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

        #region notification for procurement"

        public bool AddNotificationForProcurementFromLogistics(string destinationURl, TransportRequisition transportRequisition)
        {
            try
            {
                
                var notification = new Notification
                {
                    Text = "Transport Requisition No:" + transportRequisition.TransportRequisitionNo,
                    CreatedDate = DateTime.Now.Date,
                    IsRead = false,
                    Id = 1,
                    RecordId = transportRequisition.TransportRequisitionID,
                    Url = destinationURl,
                    TypeOfNotification = "New Transport Requisition",
                    Application = Application.PROCUREMENT
                };
                AddNotification(notification);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

        #region notification for Logistics"

        public bool AddNotificationForLogistcisFromEarlyWaring(string destinationURl,int requisitionID, int regionId, string requisitioNo)
        {
            try
            {
                    var notification = new Notification
                                           {
                                               Text = "Approved Requistion" + requisitioNo,
                                               CreatedDate = DateTime.Now.Date,
                                               IsRead = false,
                                               Id = 1,
                                               RecordId = requisitionID,
                                               Url = destinationURl,
                                               TypeOfNotification = "Requisition Approval",
                                               Application = Application.LOGISTICS
                                           };

                    AddNotification(notification);
                    return true;
               
            }
            catch (Exception)
            {
                return false;

            }

        }
        #endregion

        #region notification for Procurment GRN Discripancy"

        public bool AddNotificationForProcurmentForGRNDiscripancy(string destinationURl, int transportOrderId,string transportOrderNo)
        {
            try
            {
                var notification = new Notification
                {
                    Text = "GRN with loss from transport order " + transportOrderNo,
                    CreatedDate = DateTime.Now.Date,
                    IsRead = false,
                    Id = 1,
                    RecordId = transportOrderId,
                    Url = destinationURl,
                    TypeOfNotification = "GRN With loss",
                    Application = Application.TRANSPORT_ORDER_CREATER
                };

                AddNotification(notification);
                return true;

            }
            catch (Exception)
            {
                return false;

            }

        }
        #endregion





        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    
    }
}
