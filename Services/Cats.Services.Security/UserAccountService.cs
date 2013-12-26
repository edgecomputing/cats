using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Cats.Data.Security;
using Cats.Models.Security;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.Providers;
using Cats.Models.Security.ViewModels;
using NetSqlAzMan.Cache;
using Cats.Models.Exceptions;


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
        private readonly NetSqlAzManRoleProvider _provider;//= new NetSqlAzManRoleProvider();
        private readonly IAzManStorage _store;//= new NetSqlAzMan.SqlAzManStorage(System.Configuration.ConfigurationManager.ConnectionStrings["SecurityContext"].ConnectionString);

        public UserAccountService(IUnitOfWork unitOfWork, IAzManStorage store, NetSqlAzManRoleProvider provider)
        {
            this._unitOfWork = unitOfWork;
            this._store = store;
            this._provider = provider;
        }

        #endregion

        #region Default Service Implementation

        public bool Add(UserProfile entity, Dictionary<string,List<string>> roles)
        {
            try
            {
                // Add the user account first and latter set default preference and profiles for user
                _unitOfWork.UserProfileRepository.Add(entity);
                _unitOfWork.Save();
                //foreach (var Role in roles)
                //    AddUserToRoles(entity.UserName, Role.Value.ToArray(), "CATS", Role.Key);
                //_unitOfWork.Save();
                return true;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(string.Format("An error occurred while saving. Detail: {0} ", ex.Message));
            }
        }
        
        public bool Add(UserProfile entity, string store, string application)
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

        public bool Save(UserProfile entity)
        {
            _unitOfWork.UserProfileRepository.Edit(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(UserProfile entity)
        {
            if (entity == null) return false;
            _unitOfWork.UserProfileRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UserProfileRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UserProfileRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public List<UserProfile> GetAll()
        {
            return _unitOfWork.UserProfileRepository.GetAll();
        }

        public List<UserInfo> GetUsers()
        {
            return _unitOfWork.UserInfoRepository.GetAll();
        }

        public UserProfile FindById(int id)
        {
            return _unitOfWork.UserProfileRepository.FindById(id);
        }

        public List<UserProfile> FindBy(Expression<Func<UserProfile, bool>> predicate)
        {
            return _unitOfWork.UserProfileRepository.FindBy(predicate);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        #endregion

        #region Security Module Logic

        public bool Authenticate(UserProfile userInfo)
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
                    //throw new ApplicationException("The requested user could not be found.");
                    throw new userNotFoundException();
            }
            catch (Exception ex)
            {
                //throw new ApplicationException("The requested user could not be found.", ex);
                throw new userNotFoundException("",ex);
            }

            // If the user account is disabled then we dont need to allow login instead we need to throw an exception
            // stating that the account is disabled.
            if (user.Disabled == true)
                
                throw new disabledUserException();
                //throw new ApplicationException("The user account is currently disabled. Please contact your administrator.");
                
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
               //throw new ApplicationException("The supplied user name and password do not match.");
               throw new unmatchingUsernameAndPasswordException();
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

        public bool ChangePassword(UserProfile userInfo, string password)
        {
            try
            {
                var user = _unitOfWork.UserProfileRepository.FindBy(u => u.UserProfileID == userInfo.UserProfileID).SingleOrDefault();
                if (user != null)
                {
                    user.Password = HashPassword(password);
                    _unitOfWork.Save();
                    return true;
                }
            }
             
            catch (Exception e)
            {
                //throw new ApplicationException("Error changing password", e);
                throw new passwordChangeException(e);
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
            var user = _unitOfWork.UserProfileRepository.FindBy(u => u.UserName == userName).SingleOrDefault();
            if (user != null)
            {
                info = _unitOfWork.UserInfoRepository.FindBy(u => u.UserProfileID == user.UserProfileID).SingleOrDefault();
                user.Password = HashPassword(randomPassword);
                try
                {
                    _unitOfWork.Save();

                    // TODO: Consider sending the new password through email for the user!
                   // SendPasswordToMail(userName, user.Email);

                }
                catch (Exception e)
                {
                    //throw new ApplicationException(string.Format("Unable to reset password for {0}. \n Error detail: \n {1} ", info.FullName, e.Message), e);
                    throw new unabletoResetPasswordException(info.UserName);
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
                var user = _unitOfWork.UserProfileRepository.FindBy(u => u.UserName == userName).SingleOrDefault();
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
        public UserProfile GetUserDetail(int userId)
        {
            return _unitOfWork.UserProfileRepository.FindBy(u => u.UserProfileID == userId).SingleOrDefault();
        }

        /// <summary>
        /// Returns the detail of a given user based on supplied userName
        /// </summary>
        /// <param name="userName">User name identifying the user</param>
        /// <returns>User object corresponding to the user identified by UserName</returns>
        public UserProfile GetUserDetail(string userName)
        {
            //return _unitOfWork.UserRepository.Get(u => u.UserName == userName, null, "UserProfile,UserPreference").SingleOrDefault();
            return _unitOfWork.UserProfileRepository.Get(u => u.UserName == userName).SingleOrDefault();
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
            return _unitOfWork.UserInfoRepository.FindBy(u => u.UserProfileID == userId).SingleOrDefault();
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
            var apps = new List<Application>();
            try
            {
                _provider.Initialize("AuthorizationRoleProvider", ConfigureAuthorizationRoleProvider("CATS", ""));
                Dictionary<string, IAzManApplication> Application = _provider.GetStorage().Stores["CATS"].Applications;
                foreach (var app in Application)
                {
                    apps.Add(new Application() { ApplicationName = app.Value.Name, Roles = GetUserPermissions(UserName, "CATS", app.Value.Name) });
                }
                return apps;
            }
            catch
            {
                return apps;
            }

        }
        
        /// <summary>
        /// Get all roles associated with the application provided
        /// </summary>
        /// <param name="application"> The application name</param>
        /// <returns>Array of strings containing all of the roles in the application from .NetSqlAzMan store</returns>

        public string[] GetRoles(string application)
        {
          
            _provider.ApplicationName = application;
            return  _provider.GetAllRoles();
        }


        public string[] GetUserRoles(string username)
        {
            

            string[] roles = _provider.GetRolesForUser(username);
            return roles;
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

            _provider.Initialize("AuthorizationRoleProvider", ConfigureAuthorizationRoleProvider(store, ""));
            Dictionary<string, IAzManApplication> Application = _provider.GetStorage().Stores["CATS"].Applications;

            // Get all applications together with their corresponding roles
            foreach (var app in Application)
            {
                // Get all roles for the current application
                apps.Add(new Application { ApplicationName = app.Value.Name, Roles = GetRolesList(app.Value.Name) });
            }
            return apps;
        }
        

        public void AddUserToRoles(string userName, string[] Roles, string store, string application)
        {
            _provider.Initialize("AuthorizationRoleProvider", ConfigureAuthorizationRoleProvider(store, ""));
            string[] UserName = new string[] { userName };
            _provider.ApplicationName = application;
            _provider.AddUsersToRoles(UserName, Roles);
        }

        public void EditUserRole(string owner, string userName, Dictionary<string, List<Role>> applications)
        {
            foreach (var apps in applications)
            {
                List<Role> UserPermissions = GetUserPermissions(_store.GetDBUser(userName).CustomSid.StringValue, "CATS", apps.Key);
                UserPermissions = UserPermissions.Except(apps.Value).ToList();
                foreach (var item in apps.Value.ToArray())
                    _store["CATS"][apps.Key][item.RoleName].CreateAuthorization(_store.GetDBUser(userName).CustomSid, WhereDefined.Database, _store.GetDBUser(userName).CustomSid, WhereDefined.Database, AuthorizationType.Allow, DateTime.Now, DateTime.Now);
                foreach (var permission in UserPermissions)
                    _store["CATS"][apps.Key][permission.RoleName].CreateAuthorization(_store.GetDBUser(userName).CustomSid, WhereDefined.Database, _store.GetDBUser(userName).CustomSid, WhereDefined.Database, AuthorizationType.Deny, DateTime.Now, DateTime.Now);
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

        /// <summary>
        /// Gets list of all user preferences
        /// </summary>
        /// <returns> List of all user preferences </returns>

        public List<UserProfile> GetUserPreferences()
        {
            return _unitOfWork.UserProfileRepository.GetAll();
        }


       

        #endregion

    }
}
