using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
namespace Cats.Services.Common
{
    public interface IMailingListService
    {
        ICollection<Contact> GetContactsForMailingList(int MailingListID);
    }
}
