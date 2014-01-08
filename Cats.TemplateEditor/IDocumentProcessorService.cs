using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cats.TemplateEditor
{
    public interface IDocumentProcessorService
    {
        void UploadDocument(int templateType, string path);
        string DownloadDocument(ListView fileList);
        void DeleteDocument(string path);

    }
}
