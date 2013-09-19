using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Microsoft.Office.Interop.Word;
using Cats.Data.UnitWork;
using Cats.Documents;

namespace Cats.Helpers
{
    public class TemplateHelper
    {
        private readonly IUnitOfWork _unitofwork;

        public TemplateHelper(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        public string GenerateTemplate(int id, int templateType, string templateName)
        {
            //string templateName = string.Empty;


            string templatePath = HttpContext.Current.Server.MapPath(string.Format("~/Templates/{0}.dotx", templateName));
            string documentPath =
                HttpContext.Current.Server.MapPath(string.Format("~/Templates/{0}.docx", Guid.NewGuid().ToString()));

            var generator = new DocumentGenerator(templatePath, documentPath, GetTemplateData(templateType, id));

            var result = generator.GenerateDocument();

            return result.Value ? result.ResultPath : string.Empty;
        }

        private Dictionary<string,string> GetTemplateData(int templateType, int recordId)
        {
            var data = new Dictionary<string, string>();
            switch (templateType)
            {
                case 1: // Gift Certificate
                    var giftCertificate = _unitofwork.GiftCertificateRepository.FindById(recordId);
                    data = giftCertificate.ToDictionary();
                    break;
            }

            return data;
        }
    }


}