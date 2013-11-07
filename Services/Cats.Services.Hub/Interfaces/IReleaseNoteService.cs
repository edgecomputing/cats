
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{
    public interface IReleaseNoteService
    {

        bool AddReleaseNote(ReleaseNote entity);
        bool DeleteReleaseNote(ReleaseNote entity);
        bool DeleteById(int id);
        bool EditReleaseNote(ReleaseNote entity);
        ReleaseNote FindById(int id);
        List<ReleaseNote> GetAllReleaseNote();
        List<ReleaseNote> FindBy(Expression<Func<ReleaseNote, bool>> predicate);


    }
}


      
