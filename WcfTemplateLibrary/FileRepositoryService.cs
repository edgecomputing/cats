using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cats.Service.TemplateEditor
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true,
         InstanceContextMode = InstanceContextMode.Single)]
    public class FileRepositoryService : IFileRepositoryService
    {

        #region Events

        public event FileEventHandler FileRequested;
        public event FileEventHandler FileUploaded;
        public event FileEventHandler FileDeleted;

        #endregion

        #region IFileRepositoryService Members

        /// <summary>
        /// Gets or sets the repository directory.
        /// </summary>
        public string RepositoryDirectory { get; set; }

        /// <summary>
        /// Gets a file from the repository
        /// </summary>
        public Stream GetFile(string virtualPath)
        {
            string filePath = Path.Combine(RepositoryDirectory, virtualPath);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File was not found", Path.GetFileName(filePath));

            SendFileRequested(virtualPath);

            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }

        /// <summary>
        /// Uploads a file into the repository
        /// </summary>
        public void PutFile(FileUploadMessage msg)
        {
            string filePath = Path.Combine(RepositoryDirectory, msg.VirtualPath);
            string dir = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (var outputStream = new FileStream(filePath, FileMode.Create))
            {
                msg.DataStream.CopyTo(outputStream);
            }

            SendFileUploaded(filePath);
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

            DirectoryInfo dirInfo = new DirectoryInfo(basePath);
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
