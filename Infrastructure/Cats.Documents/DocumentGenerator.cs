using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Cats.Documents
{
    public class DocumentGenerator
    {
        // Struct to hold the return value and possible exception
        public struct TemplateGenerationResult
        {
            public bool Value;
            public string Exception;
            public string ResultPath;
        }

        private string templateFolder = ConfigurationManager.AppSettings["templateFolder"];
        private string destinationFolder = ConfigurationManager.AppSettings["destinationFolder"];

        private const string FieldDelimeter = " MERGEFIELD ";

        private string _templateFileName;
        private string _targetFileName;
        private Dictionary<string, string> _values;

        /// <summary>
        /// Constructor for the DocumentGeneration Class
        /// </summary>
        /// <param name="templateFileName">File to base the document off of</param>
        /// <param name="targetPath">File path for the generated document</param>
        /// <param name="values">Key value pair containing items to be replaced in the document</param>
        public DocumentGenerator(string templateFileName, string targetPath, Dictionary<string, string> values)
        {
            _templateFileName = templateFileName;
            _targetFileName = targetPath;
            _values = values;
        }

        /// <summary>
        /// Converts the DOTX to DOCX
        /// </summary>
        /// <returns>True or False (with an exception) if successful in converting the document</returns>
        private TemplateGenerationResult ConvertTemplate()
        {
            try
            {
                MemoryStream msFile = null;

                using (Stream sTemplate = File.Open(_templateFileName, FileMode.Open, FileAccess.Read))
                {
                    msFile = new MemoryStream((int)sTemplate.Length);
                    sTemplate.CopyTo(msFile);
                    msFile.Position = 0L;
                }

                using (WordprocessingDocument wpdFile = WordprocessingDocument.Open(msFile, true))
                {
                    wpdFile.ChangeDocumentType(WordprocessingDocumentType.Document);
                    MainDocumentPart docPart = wpdFile.MainDocumentPart;
                    docPart.AddExternalRelationship("http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate", new Uri(_templateFileName, UriKind.RelativeOrAbsolute));
                    docPart.Document.Save();
                }

                // Flush the MemoryStream to the file
                File.WriteAllBytes(_targetFileName, msFile.ToArray());

                msFile.Close();

                return new TemplateGenerationResult { Value = true };
            }
            catch (Exception ex)
            {
                return new TemplateGenerationResult { Value = false, Exception = "DocumentGeneration::convertTemplate() - " + ex.ToString() };
            }
        }

        /// <summary>
        /// Generates the Word Document and performs the Mail Merge
        /// </summary>
        /// <returns>True or false(with exception) if the generation was successful or not</returns>
        public TemplateGenerationResult GenerateDocument()
        {
            try
            {
                // Don't continue if the template file name is not found
                if (!File.Exists(_templateFileName))
                {
                    throw new Exception(message: "TemplateFileName (" + _templateFileName + ") does not exist");
                }

                // If the file is a DOTX file convert it to docx
                if (_templateFileName.ToUpper().EndsWith("DOTX"))
                {
                    TemplateGenerationResult resultValue = ConvertTemplate();

                    if (!resultValue.Value)
                    {
                        return resultValue;
                    }
                }
                else
                {
                    // Otherwise make a copy of the Word Document to the targetFileName
                    File.Copy(_templateFileName, _targetFileName);
                }

                using (WordprocessingDocument docGenerated = WordprocessingDocument.Open(_targetFileName, true))
                {
                    docGenerated.ChangeDocumentType(WordprocessingDocumentType.Document);

                    foreach (FieldCode field in docGenerated.MainDocumentPart.RootElement.Descendants<FieldCode>())
                    {
                        var fieldNameStart = field.Text.LastIndexOf(FieldDelimeter, System.StringComparison.Ordinal);
                        var fieldname = field.Text.Substring(fieldNameStart + FieldDelimeter.Length).Trim();
                        var fieldValue = _values[fieldname];

                        // Go through all of the Run elements and replace the Text Elements Text Property
                        foreach (Run run in docGenerated.MainDocumentPart.Document.Descendants<Run>())
                        {
                            foreach (Text txtFromRun in run.Descendants<Text>().Where(a => a.Text == "«" + fieldname + "»"))
                            {
                                txtFromRun.Text = fieldValue;
                            }
                        }
                    }

                    // If the Document has settings remove them so the end user doesn't get prompted to use the data source
                    DocumentSettingsPart settingsPart = docGenerated.MainDocumentPart.GetPartsOfType<DocumentSettingsPart>().First();

                    var oxeSettings = settingsPart.Settings.Where(a => a.LocalName == "mailMerge").FirstOrDefault();

                    if (oxeSettings != null)
                    {
                        settingsPart.Settings.RemoveChild(oxeSettings);

                        settingsPart.Settings.Save();
                    }

                    docGenerated.MainDocumentPart.Document.Save();
                }

                return new TemplateGenerationResult { Value = true, ResultPath = _targetFileName};
            }
            catch (Exception ex)
            {
                return new TemplateGenerationResult { Value = false, Exception = "DocumentGeneration::generateDocument() - " + ex.ToString() };
            }
        }
    }
}
