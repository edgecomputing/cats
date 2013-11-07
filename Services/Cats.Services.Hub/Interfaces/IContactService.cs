
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{
    public interface IContactService
    {

        bool AddContact(Contact contact);
        bool DeleteContact(Contact contact);
        bool DeleteById(int id);
        bool EditContact(Contact contact);
        Contact FindById(int id);
        List<Contact> GetAllContact();
        List<Contact> FindBy(Expression<Func<Contact, bool>> predicate);
        /// <summary>
        /// Gets the List Of contacts by FDP.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        List<Contact> GetByFdp(int id);

    }
}


