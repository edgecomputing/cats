

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ContactService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation

        public bool AddContact(Contact contact)
        {
            _unitOfWork.ContactRepository.Add(contact);
            _unitOfWork.Save();
            return true;

        }
        public bool EditContact(Contact contact)
        {
            _unitOfWork.ContactRepository.Edit(contact);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteContact(Contact contact)
        {
            if (contact == null) return false;
            _unitOfWork.ContactRepository.Delete(contact);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ContactRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ContactRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Contact> GetAllContact()
        {
            return _unitOfWork.ContactRepository.GetAll();
        }
        public Contact FindById(int id)
        {
            return _unitOfWork.ContactRepository.FindById(id);
        }
        public List<Contact> FindBy(Expression<Func<Contact, bool>> predicate)
        {
            return _unitOfWork.ContactRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        /// <summary>
        /// Gets the list of contacts by FDP.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public List<Contact> GetByFdp(int id)
        {
            return _unitOfWork.ContactRepository.FindBy(t => t.FDPID == id);
        }

    }
}


