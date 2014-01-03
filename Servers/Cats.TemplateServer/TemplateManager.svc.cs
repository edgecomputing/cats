using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.TemplateServer.Dto;

namespace Cats.TemplateServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TemplateManager" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TemplateManager.svc or TemplateManager.svc.cs at the Solution Explorer and start debugging.
    public class TemplateManager : ITemplateManager
    {
        /// <summary>
        /// Gets or sets the repository directory.
        /// </summary>
        public string RepositoryDirectory { get; set; }

        public void DoWork()
        {
        }

        TemplateManager()
        {
            RepositoryDirectory = "CatsTemplates";
        }
        #region Template Editor Methods

        private ITemplateService _templateService = new TemplateService();
        private ITemplateTypeTypeService _templateTypeService = new TemplateTypeService();
        private ITemplateFieldsService _templateFieldsService = new TemplateFieldService();
        IUserAccountService _userAccountService = new UserAccountService();


        public string TemplateTypes()
        {
            return "this is a sample string ";
        }

        public List<TemplateType> GetTemplateTypes()
        {
            var templatesTypes = _templateTypeService.GetAllTemplateType();
            var dtos = from t in templatesTypes
                       select new TemplateType
                       {
                           TemplateTypeId = t.TemplateTypeId,
                           TemplateObject = t.TemplateObject,
                           Remark = t.Remark
                       };
            return dtos.ToList();
        }

        public List<Template> GetAllTemplates()
        {
            var templates = _templateService.GetAllTemplate();
            var dtos = from t in templates
                       select new Template
                       {
                           TemplateId = t.TemplateId,
                           Name = t.Name,
                           TemplateType = t.TemplateType1.TemplateTypeId,
                           Remark = t.Remark
                       };
            return dtos.ToList();
        }

        public List<Template> GetTemplates(int templateId)
        {
            var templates = _templateService.GetAllTemplate();
            var dtos = (from t in templates
                        where t.TemplateType1.TemplateTypeId == templateId
                        select new Template
                        {
                            TemplateId = t.TemplateId,
                            Name = t.Name,
                            TemplateType = t.TemplateType1.TemplateTypeId,
                            Remark = t.Remark
                        });
            return dtos.ToList();



        }

        public Template GetTemplateById(int templateId)
        {
            var templates = _templateService.FindById(templateId);
            var dtoTemplate = new Template()
            {
                TemplateId = templates.TemplateId,
                Name = templates.Name,
                TemplateType = templates.TemplateType1.TemplateTypeId,
                Remark = templates.Remark
            };

            return dtoTemplate;
        }

        public Template GetTemplateByName(string templateName)
        {
            throw new NotImplementedException();
        }

        public List<TemplateFields> GetFieldsByTemplateId(int templateId)
        {
            var templateFields = _templateFieldsService.GetAllTemplateField();
            var templateFieldsDto = from field in templateFields
                                    where field.TemplateId == templateId
                                    select new TemplateFields
                                    {
                                        TemplateFieldId = field.TemplateFieldId,
                                        Field = field.Field,
                                        TemplateId = field.TemplateId,
                                    };
            return templateFieldsDto.ToList();


        }

        #endregion

        #region Authentication Methods

        public bool Authenticate(string userName, string passWord)
        {
            return _userAccountService.Authenticate(userName, passWord);
        }

        #endregion


        #region Events

        public event FileEventHandler FileRequested;
        public event FileEventHandler FileUploaded;
        public event FileEventHandler FileDeleted;

        #endregion

        #region IFileRepositoryService Members

        
        
        /// <summary>
        /// Gets a file from the repository
        /// </summary>
        public Stream GetFile(string virtualPath)
        {
            
                string filePath = Path.Combine(RepositoryDirectory, virtualPath);

                if (!File.Exists(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + filePath))
                    throw new FileNotFoundException("File was not found", Path.GetFileName(filePath));

                SendFileRequested(virtualPath);

                return new FileStream(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + filePath, FileMode.Open, FileAccess.Read);
            
        }

        /// <summary>
        /// Uploads a file into the repository
        /// </summary>
        public void PutFile(FileUploadMessage msg)
        {
            string filePath = Path.Combine(RepositoryDirectory, msg.VirtualPath);
            string dir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + filePath);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (var outputStream = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + filePath, FileMode.Create))
            {
                msg.DataStream.CopyTo(outputStream);
            }

            SendFileUploaded(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + filePath);
        }

        /// <summary>
        /// Deletes a file from the repository
        /// </summary>
        public void DeleteFile(string virtualPath)
        {
            string filePath = Path.Combine(RepositoryDirectory, virtualPath);

            if (File.Exists(filePath))
            {
                SendFileDeleted(virtualPath);
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Lists files from the repository at the specified virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path. This can be null to list files from the root of
        /// the repository.</param>
        public StorageFileInfo[] List(string virtualPath)
        {
            string basePath = RepositoryDirectory;

            if (!string.IsNullOrEmpty(virtualPath))
                basePath = Path.Combine(RepositoryDirectory, virtualPath);

            DirectoryInfo dirInfo = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + basePath);
            FileInfo[] files = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);

            return (from f in files
                    select new StorageFileInfo()
                    {
                        Size = f.Length,
                        VirtualPath = f.FullName.Substring(f.FullName.IndexOf(RepositoryDirectory) + RepositoryDirectory.Length + 1)
                    }).ToArray();
        }

        #endregion


        #region Events

        /// <summary>
        /// Raises the FileRequested event.
        /// </summary>
        protected void SendFileRequested(string vPath)
        {
            if (FileRequested != null)
                FileRequested(this, new FileEventArgs(vPath));
        }

        /// <summary>
        /// Raises the FileUploaded event
        /// </summary>
        protected void SendFileUploaded(string vPath)
        {
            if (FileUploaded != null)
                FileUploaded(this, new FileEventArgs(vPath));
        }

        /// <summary>
        /// Raises the FileDeleted event.
        /// </summary>
        protected void SendFileDeleted(string vPath)
        {
            if (FileDeleted != null)
                FileDeleted(this, new FileEventArgs(vPath));
        }

        #endregion
    }
}
