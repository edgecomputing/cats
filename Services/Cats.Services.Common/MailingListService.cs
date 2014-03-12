using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.Common
{
    public class MailingListService:IMailingListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MailingListService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ICollection<Contact> GetContactsForMailingList(int MailingListID)
        {
            List<UserProfile> users= _unitOfWork.UserProfileRepository.FindBy(u => u.MobileNumber != null);

            return (from item in users
                    select new Contact()
                    {
                        ContactID= item.UserProfileID
                        ,FirstName=item.FirstName
                        ,LastName=item.LastName
                        ,Email=item.Email
                        ,MobileNumber=item.MobileNumber
                    }
                    ).ToList();
        }
    }
}
