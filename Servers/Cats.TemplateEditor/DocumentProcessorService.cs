using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cats.TemplateEditor.TemplateService;

namespace Cats.TemplateEditor
{
   public class DocumentProcessorService:IDocumentProcessorService
    {
       public void UploadDocument(int templateType, string filepath)
       {
           if (!string.IsNullOrEmpty(filepath))
           {
               string virtualPath = AppendToFileName(templateType, Path.GetFileName(filepath));

               using (Stream uploadStream = new FileStream(filepath, FileMode.Open))
               {
                   using (var client = new TemplateManagerClient())
                   {
                      client.PutFile(new FileUploadMessage() { VirtualPath = virtualPath, DataStream = uploadStream }.VirtualPath, uploadStream);
                   }
               }

               
           }
       }



       public string DownloadDocument(ListView fileList)
       {
           var filePath = string.Empty;
           
               ListViewItem item = fileList.SelectedItems[0];

               // Strip off 'Root' from the full path
               var path = item.SubItems[1].Text;


               filePath = Properties.Settings.Default.DefaultPath.ToString() + path;
               if (!string.IsNullOrEmpty(filePath))
               {
                   // Get the file from the server
                   using (var output = new FileStream(filePath, FileMode.Create))
                   {
                       Stream downloadStream;

                       using (var client = new TemplateManagerClient())
                       {
                           downloadStream = client.GetFile(path);
                       }

                       downloadStream.CopyTo(output);
                   }
                   return filePath;

               }
               return string.Empty;
           
           
       }

       public void DeleteDocument(string path)
       {
           if (File.Exists(path))
           {

               File.Delete(path);
           }
       }



       private static string AppendToFileName(int templateType, string fileName)
       {
           switch (templateType)
           {
               case 1:
                   if (fileName.StartsWith("TRANS-"))
                       return fileName;
                   return "TRANS-" + fileName;
               case 2:
                   if (fileName.StartsWith("GIFT-"))
                       return fileName;
                   return "GIFT-" + fileName;
               default:
                   return fileName;

           }
       }

    }
}
