using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cats.Service.TemplateEditor
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFileRepositoryService" in both code and config file together.
    [ServiceContract]
    public interface IFileRepositoryService
    {
        [OperationContract]
        Stream GetFile(string virtualPath);

        [OperationContract]
        void PutFile(FileUploadMessage msg);

        [OperationContract]
        void DeleteFile(string virtualPath);

        [OperationContract]
        StorageFileInfo[] List(string virtualPath);
    }
}
