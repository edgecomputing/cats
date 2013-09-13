using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Cats.Data.Security;
using Cats.Models.Security;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.Providers;
using Cats.Models.Security.ViewModels;
using NetSqlAzMan.Cache;

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
        private NetSqlAzManRoleProvider provider = new NetSqlAzManRoleProvider();
        private IAzManStorage Store = new NetSqlAzMan.SqlAzManStorage(System.Configuration.ConfigurationManager.ConnectionStrings["SecurityContext"].ConnectionString);

        public UserAccountService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Default Service Implementation

        public bool Add(UserAccount entity, Dictionary<string,List<string>> roles)
        {
            try
            {
                // Add the user account first and latter set default preference and profiles for user
                _unitOfWork.UserRepository.Add(entity);
                _unitOfWork.Save();
                foreach (var Role in roles)
                    AddUserToRoles(entity.UserName, Role.Value.ToArray(), "CATS", Role.Key);
                _unitOfWork.Save();
                return true;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(string.Format("An error occurred while saving. Detail: {0} ", ex.Message));
            }
        }

        
        public bool Add(UserAccount entity, string store, string application)
        {
            try
            {
                AddUserToRoles(entity.UserName, entity.Roles, store, application);
                _unitOfWork.Save();
                return true;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(string.Format("An error occurred while saving. Detail: {0} ", ex.Message));
            }
        }

        public bool Save(UserAccount entity)
        {
            _unitOfWork.UserRepository.Edit(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(UserAccount entity)
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

        public List<UserAccount> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public List<UserInfo> GetUsers()
        {
            return _unitOfWork.UserInfoRepository.GetAll();
        }

        public UserAccount FindById(int id)
        {
            return _unitOfWork.UserRepository.FindById(id);
        }

        public List<UserAccount> FindBy(Expression<Func<UserAccount, bool>> predicate)
        {
            return _unitOfWork.UserRepository.FindBy(predicate);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        #endregion

        #region Security Module Logic

        public bool Authenticate(UserAccount userInfo)
        {
            return Authenticate(userInfo.UserName, userInfo.Password);
        }

        public bool Authenticate(UserInfo info)
        {
            return Authenticate(info.UserName, info.Password);
        }

        public bool Authenticate(string userName, string password)
        {
            UserInfo user = GetUserInfo(userName);

            // Check if the provided user is found in the database. If not tell the user that the user account provided
            // does not exist in the database.
            try
            {
                user = GetUserInfo(userName);

                if (null == user)
                    throw new ApplicationException("The requested user could not be found.");
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
                var identity = new UserIdentity(user);
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

        public bool ChangePassword(UserAccount userInfo, string password)
        {
            try
            {
                var user = _unitOfWork.UserRepository.FindBy(u => u.UserAccountId == userInfo.UserAccountId).SingleOrDefault();
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

        public string ResetPassword(UserInfo userInfo)
        {
            return ResetPassword(userInfo.UserName);
        }

        public string ResetPassword(string userName)
        {
            var info = new UserInfo();

            // Generate a random password
            var random = new Random();
            var randomPassword = GenerateString(random, 8);

            // Reset the current user's password attribute to the new one            
            var user = _unitOfWork.UserRepository.FindBy(u => u.UserName == userName).SingleOrDefault();
            if (user != null)
            {
                info = _unitOfWork.UserInfoRepository.FindBy(u => u.UserAccountId == user.UserAccountId).SingleOrDefault();
                user.Password = HashPassword(randomPassword);
                try
                {
                    _unitOfWork.Save();
                    // TODO: Consider sending the new password through email for the user!
                }
                catch (Exception e)
                {
                    throw new ApplicationException(string.Format("Unable to reset password for {0}. \n Error detail: \n {1} ", info.FullName, e.Message), e);
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
        public UserAccount GetUserDetail(int userId)
        {
            return _unitOfWork.UserRepository.FindBy(u => u.UserAccountId == userId).SingleOrDefault();
        }

        /// <summary>
        /// Returns the detail of a given user based on supplied userName
        /// </summary>
        /// <param name="userName">User name identifying the user</param>
        /// <returns>User object corresponding to the user identified by UserName</returns>
        public UserAccount GetUserDetail(string userName)
        {
            return _unitOfWork.UserRepository.FindBy(u => u.UserName == userName).SingleOrDefault();
        }

        /// <summary>
        /// Returns the user info based on supplied username
        /// </summary>
        /// <param name="userName"> User name identifying the user</param>
        /// <returns>UserInfo object corrensponding to the user identified by username</returns>
        public UserInfo GetUserInfo(string userName)
        {
            return _unitOfWork.UserInfoRepository.FindBy(u => u.UserName == userName).Single();
        }

        public UserInfo GetUserInfo(int userId)
        {
            return _unitOfWork.UserInfoRepository.FindBy(u => u.UserAccountId == userId).SingleOrDefault();
        }
        /// <summary>
        /// Retrive a complete Authorization for the current user and populate the string array
        /// from .NetSqlAzMan store 
        /// </summary>
        /// <param name="userName">User name identifying the current user</param>
        /// <returns>Array of strings containing all of the permissions from .NetSqlAzMan store</returns>
        public List<Role> GetUserPermissions(string userName, string store, string application)
        {
            // throw new NotImplementedException();
            //string userSid = userId.ToString("X");
            //string zeroes = string.Empty;
            //for (int start = 0; start < 8 - userSid.Length; start++)
            //    zeroes += "0";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SecurityContext"].ConnectionString;
            IAzManStorage AzManStore = new NetSqlAzMan.SqlAzManStorage(connectionString);
            NetSqlAzMan.Cache.StorageCache storage = new NetSqlAzMan.Cache.StorageCache(connectionString);
            storage.BuildStorageCache(store, application);
            NetSqlAzMan.Cache.AuthorizedItem[] items = storage.GetAuthorizedItems(store, application, AzManStore.GetDBUser(userName).CustomSid.StringValue, DateTime.Now);

            return (from t in items where t.Authorization == AuthorizationType.Allow select new Role { RoleName = t.Name }).ToList();
        }

        public List<Application> GetUserPermissions(string UserName)
        {
            var apps = new List<Cats.Models.Security.ViewModels.Application>();

            Dictionary<string, IAzManApplication> Application = provider.GetStorage().Stores["CATS"].Applications;
            foreach (var app in Application)
            {
                apps.Add(new Application() { ApplicationName = app.Value.Name, Roles = GetUserPermissions(UserName, "CATS", app.Value.Name) });
            }
            return apps;
        }
        
        /// <summary>
        /// Get all roles associated with the application provided
        /// </summary>
        /// <param name="application"> The application name</param>
        /// <returns>Array of strings containing all of the roles in the application from .NetSqlAzMan store</returns>

        public string[] GetRoles(string application)
        {
            provider.ApplicationName = application;
            return  provider.GetAllRoles();
        }

        public List<Role> GetRolesList(string application)
        {
            var roles = new List<Role>();
            foreach (string role in GetRoles(application))
            {
                roles.Add(new Role() { RoleName = role });
            }
            return roles;
        }


        public List<Application> GetApplications(string store)
        {
            var apps = new List<Cats.Models.Security.ViewModels.Application>();

            provider.Initialize("AuthorizationRoleProvider", ConfigureAuthorizationRoleProvider(store, ""));
            Dictionary<string, IAzManApplication> Application = provider.GetStorage().Stores["CATS"].Applications;

            // Get all applications together with their corresponding roles
            foreach (var app in Application)
            {
                // Get all roles for the current application
                apps.Add(new Models.Security.ViewModels.Application { ApplicationName = app.Value.Name, Roles = GetRolesList(app.Value.Name) });
            }

            return apps;
        }

        

        public void AddUserToRoles(string userName, string[] Roles, string store, string application)
        {
            string[] UserName = new string[] { userName };
            provider.ApplicationName = application;
            provider.AddUsersToRoles(UserName, Roles);
        }

        public void EditUserRole(string owner, string userName, Dictionary<string, List<Role>> applications)
        {
            foreach (var apps in applications)
            {
                List<Role> UserPermissions = GetUserPermissions(Store.GetDBUser(userName).CustomSid.StringValue, "CATS", apps.Key);
                UserPermissions = UserPermissions.Except(apps.Value).ToList();
                foreach (var item in apps.Value.ToArray())
                    Store["CATS"][apps.Key][item.RoleName].CreateAuthorization(Store.GetDBUser(userName).CustomSid, WhereDefined.Database, Store.GetDBUser(userName).CustomSid, WhereDefined.Database, AuthorizationType.Allow, DateTime.Now, DateTime.Now);
                foreach (var permission in UserPermissions)
                    Store["CATS"][apps.Key][permission.RoleName].CreateAuthorization(Store.GetDBUser(userName).CustomSid, WhereDefined.Database, Store.GetDBUser(userName).CustomSid, WhereDefined.Database, AuthorizationType.Deny, DateTime.Now, DateTime.Now);
            }
        }

        private System.Collections.Specialized.NameValueCollection ConfigureAuthorizationRoleProvider(string store, string application)
        {
            var config = new System.Collections.Specialized.NameValueCollection();

            config["connectionStringName"] = "SecurityContext";
            config["storeName"] = store;
            config["applicationName"] = application;
            config["userLookupType"] = "LDAP";
            config["defaultDomain"] = "";
            config["UseWCFCacheService"] = "false";

            return config;
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


