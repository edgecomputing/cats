using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cats.TemplateServer.Dto;

namespace Cats.TemplateServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITemplateManager" in both code and config file together.
    [ServiceContract]
    public interface ITemplateManager
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        string TemplateTypes();

        [OperationContract]
        List<TemplateType> GetTemplateTypes();

        [OperationContract]
        List<Template> GetAllTemplates();

        [OperationContract]
        List<Template> GetTemplates(int templateType);

        [OperationContract]
        Template GetTemplateById(int templateId);

        [OperationContract]
        Template GetTemplateByName(string templateName);

        [OperationContract]
        List<TemplateFields> GetFieldsByTemplateId(int templateId);


        [OperationContract]
        Boolean Authenticate(string userName, string passWord);


        [OperationContract]
        Stream GetFile(string virtualPath);

        [OperationContract]
        void PutFile(FileUploadMessage msg);

        [OperationContract]
        void DeleteFile(string virtualPath);

        [OperationContract]
        StorageFileInfo[] List(string virtualPath);

        [OperationContract]
        void InsertToLetterTemplate(LetterTemplate letterTemplate);

        [OperationContract]
        string PreviewTemplate(string filePath);

    }
}
