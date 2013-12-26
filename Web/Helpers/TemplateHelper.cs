using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Models;
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

            var generator = new DocumentGenerator(templatePath, documentPath, GetTemplateData(templateType, id), GetTransactionDetails(id));

            var result = generator.GenerateDocument();

            return result.Value ? result.ResultPath : string.Empty;
        }

        //public string OpenDocument(string templateName, byte[] data)
        //{
        //    //string templateName = string.Empty;


        //    string templatePath = HttpContext.Current.Server.MapPath(string.Format("~/Templates/{0}.dotx", templateName));
        //    string documentPath =
        //        HttpContext.Current.Server.MapPath(string.Format("~/Templates/{0}.docx", Guid.NewGuid().ToString()));

        //    var generator = new DocumentGenerator(templatePath, documentPath, GetTemplateData(templateType, id), GetTransactionDetails(id));

        //    var result = generator.GenerateDocument();

        //    return result.Value ? result.ResultPath : string.Empty;
        //}

        private Dictionary<string,string> GetTemplateData(int templateType, int recordId)
        {
            var data = new Dictionary<string, string>();
            switch (templateType)
            {
                case 1: // Gift Certificate
                    var giftCertificate = _unitofwork.GiftCertificateRepository.FindById(recordId);
                    data = giftCertificate.ToDictionary();
                    break;
                case 7: // Transport Agreement
                    var transporter = _unitofwork.TransporterRepository.FindById(recordId);
                    var bidObj =
                        _unitofwork.BidWinnerRepository.Get(t => t.TransporterID == recordId && t.Status == 1).
                            FirstOrDefault();
                    if (bidObj != null)
                    {
                        var transporterAgreementViewModel = new Dictionary<string, string>()
                            {
                                {"TransporterName", transporter.Name.ToString()},
                                {"SubCity", transporter.SubCity.ToString()},
                                {"Kebele", transporter.Kebele.ToString()},
                                {"HouseNo", transporter.HouseNo.ToString()},
                                {"ContractNo", "LTCD/"+ bidObj.BidID.ToString() + "/" + DateTime.Today.Year.ToString() + "/" + transporter.Name},
                                {"ContractDate", DateTime.Now.ToString()},
                            };
                        data = transporterAgreementViewModel;
                    }
                    break;
            }

            return data;
        }

        public List<TransactionDetail> GetTransactionDetails(int recordId)
        {
            var result = new List<TransactionDetail>();
            var transporterObj = _unitofwork.TransporterRepository.FindById(recordId);
            var bidWinnerDestinations =
                _unitofwork.BidWinnerRepository.Get(t => t.TransporterID == transporterObj.TransporterID && t.Status == 1, null,
                                                    "AdminUnit, AdminUnit.AdminUnit2, AdminUnit.AdminUnit2.AdminUnit2, Hub, Bid");
            var existingDestinationList = new List<int>();
            var transactionDetailList = new List<TransactionDetail>();
            foreach (var bidWinnerDestination in bidWinnerDestinations)
            {
                if (!existingDestinationList.Contains(bidWinnerDestination.DestinationID))
                {
                    transactionDetailList.Add(new TransactionDetail
                    {
                        Region = bidWinnerDestination.AdminUnit.AdminUnit2.AdminUnit2.Name,
                        Zone = bidWinnerDestination.AdminUnit.AdminUnit2.Name,
                        Woreda = bidWinnerDestination.AdminUnit.Name,
                        BidNumber = bidWinnerDestination.Bid.BidNumber,
                        BidStratingDate = bidWinnerDestination.Bid.StartDate.ToShortDateString(),
                        DistanceFromOrigin = "0 Km",
                        Warehouse = bidWinnerDestination.Hub.Name,
                        Tariff = Convert.ToDecimal(bidWinnerDestination.Tariff.ToString())
                    });
                }
                existingDestinationList.Add(bidWinnerDestination.DestinationID);
            }
            result = transactionDetailList;

            return result;
        }
    }


}