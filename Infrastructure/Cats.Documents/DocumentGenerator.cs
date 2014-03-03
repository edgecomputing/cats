using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Areas.Procurement.Models;
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
        private List<TransactionDetail> _transactionDetailsList;
        private Dictionary<string, string> _values;

        /// <summary>
        /// Constructor for the DocumentGeneration Class
        /// </summary>
        /// <param name="templateFileName">File to base the document off of</param>
        /// <param name="targetPath">File path for the generated document</param>
        /// <param name="values">Key value pair containing items to be replaced in the document</param>
        /// <param name="transactionDetailsList"> </param>
      
        public DocumentGenerator(string templateFileName, string targetPath, Dictionary<string, string> values, List<TransactionDetail> transactionDetailsList)
        {
            _templateFileName = templateFileName;
            _targetFileName = targetPath;
            _values = values;
            _transactionDetailsList = transactionDetailsList;
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
                System.IO.File.AppendAllText(@"c:\temp\errors.txt", "In DocumentGeneration::convertTemplate():   " + ex.Message.ToString(CultureInfo.InvariantCulture));
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
                    System.IO.File.AppendAllText(@"c:\temp\errors.txt","In GenerateDocument:   " +  _templateFileName);
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
                        //var index = field.Text.LastIndexOf(" ", System.StringComparison.Ordinal) + 1;
                        var fieldname = field.Text.Substring(fieldNameStart + FieldDelimeter.Length).Trim();
                        fieldname = fieldname.Replace("\\* MERGEFORMAT", "").Trim();
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

                    if(_transactionDetailsList.Any())
                    {
                        var table = new Table();

                        var props = new TableProperties(
                            new TableBorders(
                            new TopBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 6,
                                Color = "grey"
                            },
                            new BottomBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 6,
                                Color = "grey"
                            },
                            new LeftBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 6,
                                Color = "grey"
                            },
                            new RightBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 6,
                                Color = "grey"
                            },
                            new InsideHorizontalBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 6,
                                Color = "grey"
                            },
                            new InsideVerticalBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 6,
                                Color = "grey"
                            }));

                        //table.AppendChild<TableProperties>(props);
                        var uniqueRegions = _transactionDetailsList.Select(t => t.Region).Distinct();
                        var enumerable = uniqueRegions as List<string> ?? uniqueRegions.ToList();
                        foreach (var uniqueRegion in uniqueRegions)
                        {
                            var transactionDetailsListOfARegion = _transactionDetailsList.Where(t => t.Region == uniqueRegion);
                            var detailsListOfARegion = transactionDetailsListOfARegion as List<TransactionDetail> ?? transactionDetailsListOfARegion.ToList();
                            var firstOrDefault = detailsListOfARegion.FirstOrDefault();

                            var regionTr = new TableRow();
                            var regionTc = new TableCell();
                            var regionTcContent = new TableCell();
                            regionTc.Append(new Paragraph(new Run(new Text("Region: "))));
                            regionTcContent.Append(new Paragraph(new Run(new Text(uniqueRegion))));
                            regionTr.Append(regionTc);
                            regionTr.Append(regionTcContent);
                            table.Append(regionTr);

                            var bidNumberTr = new TableRow();
                            var bidNumberTc = new TableCell();
                            var bidNumberTcContent = new TableCell();
                            bidNumberTc.Append(new Paragraph(new Run(new Text("Bid Contract: "))));
                            if (firstOrDefault != null)
                                bidNumberTcContent.Append(new Paragraph(new Run(new Text(firstOrDefault.BidNumber))));
                            bidNumberTr.Append(bidNumberTc);
                            bidNumberTr.Append(bidNumberTcContent);
                            table.Append(bidNumberTr);

                            var bidDateTr = new TableRow();
                            var bidDateTc = new TableCell();
                            var bidDateTcContent = new TableCell();
                            bidDateTc.Append(new Paragraph(new Run(new Text("Bid Date: "))));
                            if (firstOrDefault != null)
                                bidDateTcContent.Append(new Paragraph(new Run(new Text(firstOrDefault.BidStratingDate))));
                            bidDateTr.Append(bidDateTc);
                            bidDateTr.Append(bidDateTcContent);
                            table.Append(bidDateTr);

                            var transactionTh = new TableRow();

                            var rowNoTh = new TableCell();
                            rowNoTh.Append(new Paragraph(new Run(new Text("No."))));
                            //zoneTh.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto}));
                            transactionTh.Append(rowNoTh);

                            var zoneTh = new TableCell();
                            zoneTh.Append(new Paragraph(new Run(new Text("Zone"))));
                            //zoneTh.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto}));
                            transactionTh.Append(zoneTh);

                            var woredaTh = new TableCell();
                            woredaTh.Append(new Paragraph(new Run(new Text("Woreda"))));
                            //woredaTh.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto}));
                            transactionTh.Append(woredaTh);

                            var warehouseTh = new TableCell();
                            warehouseTh.Append(new Paragraph(new Run(new Text("Warehouse (Origin)"))));
                            //warehouseTh.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto}));
                            transactionTh.Append(warehouseTh);

                            var distanceTh = new TableCell();
                            distanceTh.Append(new Paragraph(new Run(new Text("Distance From Origin"))));
                            //warehouseTh.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto}));
                            transactionTh.Append(distanceTh);

                            var tariffTh = new TableCell();
                            tariffTh.Append(new Paragraph(new Run(new Text("Tariff/Qtl (in birr)"))));
                            //tariffTh.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto}));
                            transactionTh.Append(tariffTh);
                            table.Append(transactionTh);
                            var rowNoCount = 1;
                            foreach (var transaction in detailsListOfARegion)
                            {
                                var transactionTr = new TableRow();

                                var rowNoTc = new TableCell();
                                rowNoTc.Append(new Paragraph(new Run(new Text(rowNoCount.ToString()))));
                                //zoneTc.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                                transactionTr.Append(rowNoTc);

                                var zoneTc = new TableCell();
                                zoneTc.Append(new Paragraph(new Run(new Text(transaction.Zone))));
                                //zoneTc.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                                transactionTr.Append(zoneTc);

                                var woredaTc = new TableCell();
                                woredaTc.Append(new Paragraph(new Run(new Text(transaction.Woreda))));
                                //woredaTc.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                                transactionTr.Append(woredaTc);
                                
                                var warehouseTc = new TableCell();
                                warehouseTc.Append(new Paragraph(new Run(new Text(transaction.Warehouse))));
                                //warehouseTc.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                                transactionTr.Append(warehouseTc);

                                var distanceTc = new TableCell();
                                distanceTc.Append(new Paragraph(new Run(new Text(transaction.DistanceFromOrigin))));
                                //warehouseTc.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                                transactionTr.Append(distanceTc);

                                var tariffTc = new TableCell();
                                tariffTc.Append(new Paragraph(new Run(new Text(transaction.Tariff.ToString()))));
                                //tariffTc.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                                transactionTr.Append(tariffTc);
                                table.Append(transactionTr);

                                rowNoCount++;
                            }
                            var spacingTr = new TableRow();
                            var spacingTc = new TableCell();
                            spacingTc.Append(new Paragraph(new Run(new Text(" "))));
                            spacingTr.Append(spacingTc);
                            table.Append(spacingTr);
                            //table.Append(spacingTr);
                        }
                        docGenerated.MainDocumentPart.Document.Append(table);
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
                System.IO.File.AppendAllText(@"c:\temp\errors.txt", "In DocumentGeneration::generateDocument():   " + ex.Message.ToString(CultureInfo.InvariantCulture));
                return new TemplateGenerationResult { Value = false, Exception = "DocumentGeneration::generateDocument() - " + ex.ToString() };
            }
        }

        

        //public static void GenerateWord(DataTable dtSource, WordprocessingDocument docGenerated)
        //{
        //    var sbDocBody = new StringBuilder(); ;
        //    try
        //    {
        //        // Declare Styles
        //        sbDocBody.Append("<style>");
        //        sbDocBody.Append(".Header {  background-color:Navy; color:#ffffff; font-weight:bold;font-family:Verdana; font-size:12px;}");
        //        sbDocBody.Append(".SectionHeader { background-color:#8080aa; color:#ffffff; font-family:Verdana; font-size:12px;font-weight:bold;}");
        //        sbDocBody.Append(".Content { background-color:#ccccff; color:#000000; font-family:Verdana; font-size:12px;text-align:left}");
        //        sbDocBody.Append(".Label { background-color:#ccccee; color:#000000; font-family:Verdana; font-size:12px; text-align:right;}");
        //        sbDocBody.Append("</style>");
        //        //
        //        var sbContent = new StringBuilder(); ;
        //        sbDocBody.Append("<br><table align=\"center\" cellpadding=1 cellspacing=0 style=\"background-color:#000000;\">");
        //        sbDocBody.Append("<tr><td width=\"500\">");
        //        sbDocBody.Append("<table width=\"100%\" cellpadding=1 cellspacing=2 style=\"background-color:#ffffff;\">");
        //        //
        //        if (dtSource.Rows.Count > 0)
        //        {
        //            sbDocBody.Append("<tr><td>");
        //            sbDocBody.Append("<table width=\"600\" cellpadding=\"0\" cellspacing=\"2\"><tr><td>");
        //            //
        //            // Add Column Headers
        //            sbDocBody.Append("<tr><td width=\"25\"> </td></tr>");
        //            sbDocBody.Append("<tr>");
        //            sbDocBody.Append("<td> </td>");
        //            for (int i = 0; i < dtSource.Columns.Count; i++)
        //            {
        //                sbDocBody.Append("<td class=\"Header\" width=\"120\">" + dtSource.Columns[i].ToString().Replace(".", "<br>") + "</td>");
        //            }
        //            sbDocBody.Append("</tr>");
        //            //
        //            // Add Data Rows
        //            for (int i = 0; i < dtSource.Rows.Count; i++)
        //            {
        //                sbDocBody.Append("<tr>");
        //                sbDocBody.Append("<td> </td>");
        //                for (int j = 0; j < dtSource.Columns.Count; j++)
        //                {
        //                    sbDocBody.Append("<td class=\"Content\">" + dtSource.Rows[i][j].ToString() + "</td>");
        //                }
        //                sbDocBody.Append("</tr>");
        //            }
        //            sbDocBody.Append("</table>");
        //            sbDocBody.Append("</td></tr></table>");
        //            sbDocBody.Append("</td></tr></table>");
        //        }
        //        //
        //        HttpContext.Current.Response.Clear();
        //        HttpContext.Current.Response.Buffer = true;
        //        //
        //        HttpContext.Current.Response.AppendHeader("Content-Type", "application/msword");
        //        HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment; filename=EmployeeDetails.doc");
        //        HttpContext.Current.Response.Write(sbDocBody.ToString());
        //        HttpContext.Current.Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Ignore this error as this is caused due to termination of the Response Stream.
        //    }
        //}
    }
}
