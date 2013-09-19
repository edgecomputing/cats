using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models.Security;

namespace Cats.Services.Security
{
   public interface IForgetPasswordRequestService:IDisposable
    {
        bool AddForgetPasswordRequest(ForgetPasswordRequest forgetPasswordRequest);
        bool DeleteForgetPasswordRequest(ForgetPasswordRequest forgetPasswordRequest);
        bool DeleteById(int id);
        bool EditForgetPasswordRequest(ForgetPasswordRequest forgetPasswordRequest);
        ForgetPasswordRequest FindById(int id);
        List<ForgetPasswordRequest> GetAllForgetPasswordRequest();
        List<ForgetPasswordRequest> FindBy(Expression<Func<ForgetPasswordRequest, bool>> predicate);

        /// <summary>
        /// Gets the valid request.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        ForgetPasswordRequest GetValidRequest(string key);
        /// <summary>
        /// Invalidates the request.
        /// </summary>
        /// <param name="reqId">The req id.</param>
        void InvalidateRequest(int reqId);
    }
}
