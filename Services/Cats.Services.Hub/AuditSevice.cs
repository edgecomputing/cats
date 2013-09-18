using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Data.Hub;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{
    public class AuditSevice : IAuditService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditSevice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Implementation of IAuditService

        public bool AddAudit(Audit audit)
        {
            _unitOfWork.AuditRepository.Add(audit);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteAudit(Audit audit)
        {
            if (audit == null) return false;
            _unitOfWork.AuditRepository.Delete(audit);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.AuditRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.AuditRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditAudit(Audit audit)
        {
            _unitOfWork.AuditRepository.Edit(audit);
            _unitOfWork.Save();
            return true;
        }

        public Audit FindById(int id)
        {
            return _unitOfWork.AuditRepository.FindById(id);
        }

        public List<Audit> GetAllAudit()
        {
            return _unitOfWork.AuditRepository.GetAll();
        }

        public List<Audit> FindBy(Expression<Func<Audit, bool>> predicate)
        {
            return _unitOfWork.AuditRepository.FindBy(predicate);
        }

        #endregion

        public List<FieldChange> GetChanges(string table, string property, string foreignTable, string foreignFeildName, string foreignFeildKey, string key)
        {
            var changes = (from audit in _unitOfWork.AuditRepository.GetAll()
                           where audit.TableName == table && audit.PrimaryKey == key && audit.NewValue.Contains(property)
                           orderby audit.DateTime descending
                           select audit);

            List<FieldChange> filedsList = new List<FieldChange>();
            foreach (Audit a in changes)
            {
                if (foreignTable != null && foreignFeildName != null)
                {
                    filedsList.Add(new FieldChange(a, property, foreignTable, foreignFeildName, foreignFeildKey));
                }
                else
                {
                    filedsList.Add(new FieldChange(a, property));
                }
            }
            return filedsList;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
