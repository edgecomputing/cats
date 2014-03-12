using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Cats.Helpers;
using Cats.Services.Common;
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
            RepositoryDirectory = "Templates";
        }
        #region Template Editor Methods

        private ITemplateService _templateService = new TemplateService();
        private ITemplateTypeTypeService _templateTypeService = new TemplateTypeService();
        private ITemplateFieldsService _templateFieldsService = new TemplateFieldService();
        private ILetterTemplateService _letterTemplateService = new LetterTemplateService();
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
                            FileName = t.FileName,
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
            var directory = ConfigurationSettings.AppSettings["TemplatePath"].ToString(CultureInfo.InvariantCulture);
            string filePath = Path.Combine(RepositoryDirectory, virtualPath);

            if (!File.Exists( directory + "\\" + filePath))
                throw new FileNotFoundException("File was not found", Path.GetFileName(filePath));

           
              FileStream file = File.Open(directory + "\\" + filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
              return file;
           
        }

        /// <summary>
        /// Uploads a file into the repository
        /// </summary>
        public void PutFile(FileUploadMessage msg)
        {
            string filePath = Path.Combine(RepositoryDirectory, msg.VirtualPath);
            if (ConfigurationSettings.AppSettings != null)
            {
                string dir = Path.GetDirectoryName(ConfigurationSettings.AppSettings["TemplatePath"].ToString(CultureInfo.InvariantCulture) + "\\" + filePath);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }

            using (var outputStream = new FileStream(ConfigurationSettings.AppSettings["TemplatePath"].ToString(CultureInfo.InvariantCulture) + "\\" + filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                msg.DataStream.CopyTo(outputStream);
                outputStream.Close();
            }

            SendFileUploaded(ConfigurationSettings.AppSettings["TemplatePath"].ToString(CultureInfo.InvariantCulture) + "\\" + filePath);
        }

        /// <summary>
        /// Deletes a file from the repository
        /// </summary>
        public void DeleteFile(string virtualPath)
        {
            string filePath = Path.Combine(ConfigurationSettings.AppSettings["TemplatePath"].ToString(CultureInfo.InvariantCulture) + RepositoryDirectory, virtualPath);
          

            if (File.Exists(filePath))
            {
                SendFileDeleted(filePath);
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

            DirectoryInfo dirInfo = new DirectoryInfo(ConfigurationSettings.AppSettings["TemplatePath"].ToString(CultureInfo.InvariantCulture) + "\\" + basePath);
            FileInfo[] files = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);

            return (from f in files
                    select new StorageFileInfo()
                    {
                        Size = f.Length,
                        VirtualPath = f.FullName.Substring(f.FullName.IndexOf(RepositoryDirectory) + RepositoryDirectory.Length + 1)
                    }).ToArray();
        }

        public void InsertToLetterTemplate(LetterTemplate letterTemplate)
        {
            try
            {
                var catsletterTemplate = new Models.LetterTemplate
                {
                    LetterTemplateID = letterTemplate.LetterTemplateID,
                    Name = letterTemplate.Name,
                    FileName = letterTemplate.FileName,
                    TemplateType = letterTemplate.TemplateType
                };

                _letterTemplateService.AddLetterTemplate(catsletterTemplate);
            }
            catch (Exception)
            {


            }


        }


        public void InsertToTemplate(Template template)
        {
            try
            {
                var catsletterTemplate = new Models.Template()
                {
                   
                    Name = template.Name,
                    FileName = template.FileName,
                    TemplateType = template.TemplateType
                };

                _templateService.AddTemplate(catsletterTemplate);
            }
            catch (Exception)
            {


            }


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

        public string PreviewTemplate(string fileName)
        {/*
            var path = ConfigurationSettings.AppSettings["TemplatePath"].ToString(CultureInfo.InvariantCulture) + "Templates\\";
            var template = new TemplateHelper();
            string newfilePath = template.GenerateTemplatePreview(132, 1, fileName, path + fileName, path + Guid.NewGuid().ToString());

            return newfilePath;*/
            return "";

        }



        #region Delete Tempalte From Table

       public void DeleteTemplate(string fileName)
       {
           var template = _templateService.FindBy(t => t.FileName == fileName).Single();
           if (template!=null)
           {
               _templateService.DeleteTemplate(template);
           }
       }

        #endregion
    }
}
