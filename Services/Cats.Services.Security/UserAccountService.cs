using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cats.Data.Security;
using Cats.Models.Security;

namespace Cats.Services.Security
{
    /// <summary>
    /// Implementation for user account management. This service allows the creation and management of
    /// user accounts, authenticates users through username/password combination, encrypts password(s)
    /// and perform user account managment functions (change password, reset password and enable/disable)
    /// user accounts.
    /// </summary>
    public class UserAccountService : IUserAccountService
    {
        #region Private vars and Constructors

        private readonly IUnitOfWork _unitOfWork;

        public UserAccountService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Default Service Implementation

        public bool Add(User entity)
        {
            _unitOfWork.UserRepository.Add(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool Save(User entity)
        {
            _unitOfWork.UserRepository.Edit(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(User entity)
        {
            if (entity == null) return false;
            _unitOfWork.UserRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UserRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UserRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public List<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public User FindById(int id)
        {
            return _unitOfWork.UserRepository.FindById(id);
        }

        public List<User> FindBy(Expression<Func<User, bool>> predicate)
        {
            return _unitOfWork.UserRepository.FindBy(predicate);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        #endregion

        #region Security Module Logic
        public bool Authenticate(User userInfo)
        {
            return Authenticate(userInfo.UserName, userInfo.Password);
        }

        public bool Authenticate(string userName, string password)
        {
            User user = null;

            // Check if the provided user is found in the database. If not tell the user that the user account provided
            // does not exist in the database.
            try
            {
                user = GetUserDetail(userName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("The requested user could not be found.", ex);
            }

            // If the user account is disabled then we dont need to allow login instead we need to throw an exception
            // stating that the account is disabled.
            if (user.Disabled == true)
                throw new ApplicationException(
                    "The user account is currently disabled. Please contact your administrator.");

            // Check if the passwords match
            if (user.Password == HashPassword(password))
            {
                //Add the current Identity and Principal to the current thread.
                var identity = new UserIdentity(user.UserName, this);
                var principal = new UserPrincipal(identity);
                Thread.CurrentPrincipal = principal;
                return true;
            }
            else
            {
                throw new ApplicationException("The supplied user name and password do not match.");
            }
            return false;
        }

        public bool ChangePassword(int userId, string password)
        {
            var user = GetUserDetail(userId);
            return ChangePassword(user, password);
        }

        public bool ChangePassword(string userName, string password)
        {
            var user = GetUserDetail(userName);
            return ChangePassword(user, password);
        }

        public bool ChangePassword(User userInfo, string password)
        {
            try
            {
                var user = _unitOfWork.UserRepository.FindBy(u => u.UserId == userInfo.UserId).SingleOrDefault();
                if (user != null)
                {
                    user.Password = HashPassword(password);
                    _unitOfWork.Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error changing password", e);
            }
            return false;
        }

        public string ResetPassword(User userInfo)
        {

        }

        public string ResetPassword(string userName)
        {
            // Generate a random password
            var random = new Random();
            var randomPassword = GenerateString(random, 8);

            // Reset the current user's password attribute to the new one            
            var user = _unitOfWork.UserRepository.FindBy(u => u.UserName == userName).SingleOrDefault();

            if (user != null)
            {
                user.Password = HashPassword(randomPassword);
                try
                {
                    _unitOfWork.Save();
                    // TODO: Consider sending the new password through email for the user!
                }
                catch (Exception e)
                {
                    throw new ApplicationException(string.Format("Unable to reset password for {0}. \n Error detail: \n {1} ", userInfo.FullName, e.Message), e);
                }
            }
            return randomPassword;
        }

        /// <summary>
        /// Flips/Reverts the status of a user account. If an account is active it will
        /// disable it but if it is already disabled then it will activiate it by setting
        /// its value to 'enabled'.
        /// </summary>
        /// <param name="userName">The account to enable/disable</param>
        /// <returns>boolean value informing the status of the operation</returns>
        public bool DisableAccount(string userName)
        {
            try
            {
                var user = _unitOfWork.UserRepository.FindBy(u => u.UserName == userName).SingleOrDefault();
                if (user != null)
                {
                    user.Disabled = !user.Disabled;
                    _unitOfWork.Save();
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Error disabling/enabling user account", exception);
            }

            return false;
        }

        #endregion

        #region Security Module Helper Methods

        /// <summary>
        /// Encrypts a given string (password) using the SHA1 cryptography algorithm
        /// </summary>
        /// <param name="password">string (passowrd) to encrypt</param>
        /// <returns>Encrypted hash for the supplied string (password)</returns>
        public string HashPassword(string password)
        {
            Byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            SHA256Managed hashProvider = new SHA256Managed();
            hashProvider.Initialize();
            passwordBytes = hashProvider.ComputeHash(passwordBytes);
            hashProvider.Clear();
            return Convert.ToBase64String(passwordBytes);
        }

        /// <summary>
        /// Returns the detail of a given user based on supplied UserId
        /// </summary>
        /// <param name="userId">Unique id identifying the user</param>
        /// <returns>User object corresponding to the user identified by UserId</returns>
        public User GetUserDetail(int userId)
        {
            return _unitOfWork.UserRepository.FindBy(u => u.UserId == userId).SingleOrDefault();
        }

        /// <summary>
        /// Returns the detail of a given user based on supplied userName
        /// </summary>
        /// <param name="userName">User name identifying the user</param>
        /// <returns>User object corresponding to the user identified by UserName</returns>
        public User GetUserDetail(string userName)
        {
            // TODO: Consider adding exception handling here.
            return _unitOfWork.UserRepository.FindBy(u => u.UserName == userName).SingleOrDefault();
        }

        /// <summary>
        /// Retrive a complete Authorization for the current user and populate the string array
        /// from .NetSqlAzMan store 
        /// </summary>
        /// <param name="userName">User name identifying the current user</param>
        /// <returns>Array of strings containing all of the permissions from .NetSqlAzMan store</returns>
        public string[] GetUserPermissions(string userName)
        {
            throw new NotImplementedException();
        }


        public string GenerateString(Random rng, int length)
        {
            var letters = new char[length];
            for (var i = 0; i < length; i++)
            {
                letters[i] = GenerateChar(rng);
            }
            return new string(letters);
        }

        private char GenerateChar(Random rng)
        {
            return (char)(rng.Next('A', 'Z' + 1));
        }

        #endregion

    }
}


