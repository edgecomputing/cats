// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrintHeaderFooter.cs" company="SemanticArchitecture">
//   http://www.SemanticArchitecture.net pkalkie@gmail.com
// </copyright>
// <summary>
//   This class represents the standard header and footer for a PDF print.
//   application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ReportManagement
{
    using System;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    /// <summary>
    /// This class represents the standard header and footer for a PDF print.
    /// application.
    /// </summary>
    public class PrintHeaderFooter : PdfPageEventHelper
    {
        private PdfContentByte pdfContent;
        private PdfTemplate pageNumberTemplate;
        private BaseFont baseFont;
        private DateTime printTime;

        public string Title { get; set; }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            printTime = DateTime.Now;
            baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            pdfContent = writer.DirectContent;
            pageNumberTemplate = pdfContent.CreateTemplate(50, 50);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);

            Rectangle pageSize = document.PageSize;

            if (Title != string.Empty)
            {
                pdfContent.BeginText();
                pdfContent.SetFontAndSize(baseFont, 11);
                pdfContent.SetRGBColorFill(0, 0, 0);
                pdfContent.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetTop(40));
                pdfContent.ShowText(Title);
                pdfContent.EndText();
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            int pageN = writer.PageNumber;
            string text = pageN + " - ";
            float len = baseFont.GetWidthPoint(text, 8);

            Rectangle pageSize = document.PageSize;
            pdfContent = writer.DirectContent;
            pdfContent.SetRGBColorFill(100, 100, 100);

            pdfContent.BeginText();
            pdfContent.SetFontAndSize(baseFont, 8);
            pdfContent.SetTextMatrix(pageSize.Width / 2, pageSize.GetBottom(30));
            pdfContent.ShowText(text);
            pdfContent.EndText();

            pdfContent.AddTemplate(pageNumberTemplate, (pageSize.Width / 2) + len, pageSize.GetBottom(30));

            pdfContent.BeginText();
            pdfContent.SetFontAndSize(baseFont, 8);
            pdfContent.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, printTime.ToString(), pageSize.GetRight(40), pageSize.GetBottom(30), 0);
            pdfContent.EndText();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            pageNumberTemplate.BeginText();
            pageNumberTemplate.SetFontAndSize(baseFont, 8);
            pageNumberTemplate.SetTextMatrix(0, 0);
            pageNumberTemplate.ShowText(string.Empty + (writer.PageNumber - 1));
            pageNumberTemplate.EndText();
        }
    }
}