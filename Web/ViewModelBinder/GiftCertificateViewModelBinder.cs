using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Areas.GiftCertificate.Models;
using Cats.Models;

namespace Cats.ViewModelBinder
{
    public class GiftCertificateViewModelBinder
    {
       public static  List<GiftCertificateViewModel> BindListGiftCertificateViewModel(List<GiftCertificate> giftCertificates)
       {
           return giftCertificates.Select(BindGiftCertificateViewModel).ToList();
       }
        public static GiftCertificateViewModel BindGiftCertificateViewModel(GiftCertificate giftCertificateModel)
        {
            var giftCertificateViewModel = new GiftCertificateViewModel();

            giftCertificateViewModel.GiftCertificateID = giftCertificateModel.GiftCertificateID;
            giftCertificateViewModel.GiftDate = giftCertificateModel.GiftDate;
            giftCertificateViewModel.DonorID = giftCertificateModel.DonorID;
            giftCertificateViewModel.SINumber = giftCertificateModel.SINumber;
            giftCertificateViewModel.ReferenceNo = giftCertificateModel.ReferenceNo;
            giftCertificateViewModel.Vessel = giftCertificateModel.Vessel;
            giftCertificateViewModel.ETA = giftCertificateModel.ETA;
            giftCertificateViewModel.ProgramID = giftCertificateModel.ProgramID;
            giftCertificateViewModel.PortName = giftCertificateModel.PortName;
            giftCertificateViewModel.DModeOfTransport = giftCertificateModel.DModeOfTransport;
            giftCertificateViewModel.Donor = giftCertificateModel.Donor.Name;
            
            var giftCertificateDetail = giftCertificateModel.GiftCertificateDetails.FirstOrDefault();
            if (giftCertificateDetail != null)
                giftCertificateViewModel.CommodityTypeID = giftCertificateDetail.Commodity.CommodityTypeID;
            else
                giftCertificateViewModel.CommodityTypeID = 1;//by default 'food' 
            giftCertificateViewModel.GiftCertificateDetails =
               BindListOfGiftCertificateDetailsViewModel(
                   giftCertificateModel.GiftCertificateDetails.ToList());


            return giftCertificateViewModel;
        }

        public static GiftCertificate BindGiftCertificate(GiftCertificateViewModel giftCertificateViewModel)
        {
            GiftCertificate giftCertificate = new GiftCertificate()
            {
                GiftCertificateID = giftCertificateViewModel.GiftCertificateID,
                GiftDate = giftCertificateViewModel.GiftDate,
                SINumber = giftCertificateViewModel.SINumber,
                DonorID = giftCertificateViewModel.DonorID,
                ReferenceNo = giftCertificateViewModel.ReferenceNo,
                Vessel = giftCertificateViewModel.Vessel,
                ETA = giftCertificateViewModel.ETA,
                IsPrinted = giftCertificateViewModel.IsPrinted,
                DModeOfTransport = giftCertificateViewModel.DModeOfTransport,
                ProgramID = giftCertificateViewModel.ProgramID,
                PortName = giftCertificateViewModel.PortName
            };
            return giftCertificate;

        }

        public static List<GiftCertificateDetail> BindListGiftCertificateDetail(List<GiftCertificateDetailsViewModel> giftCertificateDetailsViewModel)
        {
            return giftCertificateDetailsViewModel.Select(BindGiftCertificateDetail).ToList();
        }

        public static GiftCertificateDetail BindGiftCertificateDetail(GiftCertificateDetailsViewModel giftCertificateDetailsViewModel)
        {

            return new GiftCertificateDetail()
                       {
                           CommodityID = giftCertificateDetailsViewModel.CommodityID,
                           BillOfLoading = giftCertificateDetailsViewModel.BillOfLoading,
                           YearPurchased = giftCertificateDetailsViewModel.YearPurchased,
                           AccountNumber = giftCertificateDetailsViewModel.AccountNumber,
                           WeightInMT = giftCertificateDetailsViewModel.WeightInMT,
                           EstimatedPrice = giftCertificateDetailsViewModel.EstimatedPrice,
                           EstimatedTax = giftCertificateDetailsViewModel.EstimatedTax,
                           DCurrencyID = giftCertificateDetailsViewModel.DCurrencyID,
                           DFundSourceID = giftCertificateDetailsViewModel.DFundSourceID,
                           DBudgetTypeID = giftCertificateDetailsViewModel.DBudgetTypeID,
                           GiftCertificateDetailID = giftCertificateDetailsViewModel.GiftCertificateDetailID,

                       };
        }
       
        public static List<GiftCertificateDetailsViewModel> BindListOfGiftCertificateDetailsViewModel(List<GiftCertificateDetail> giftCertificateDetails)
        {
            return giftCertificateDetails.Select(BindGiftCertificateDetailsViewModel).ToList();
        }

        public static GiftCertificateDetailsViewModel BindGiftCertificateDetailsViewModel(GiftCertificateDetail giftCertificateDetail)
        {
            var model = new GiftCertificateDetailsViewModel();

            model.GiftCertificateID = giftCertificateDetail.GiftCertificateID;
            model.CommodityID = giftCertificateDetail.CommodityID;
            model.BillOfLoading = giftCertificateDetail.BillOfLoading;
            model.YearPurchased = giftCertificateDetail.YearPurchased;
            model.AccountNumber = giftCertificateDetail.AccountNumber;
            model.WeightInMT = giftCertificateDetail.WeightInMT;
            model.EstimatedPrice = giftCertificateDetail.EstimatedPrice;
            model.EstimatedTax = giftCertificateDetail.EstimatedTax;
            model.DBudgetTypeID = giftCertificateDetail.DBudgetTypeID;
            model.DFundSourceID = giftCertificateDetail.DFundSourceID;
            model.DCurrencyID = giftCertificateDetail.DCurrencyID;
            model.ExpiryDate = giftCertificateDetail.ExpiryDate;
            model.FundSource = giftCertificateDetail.Detail.Name;
            model.CommodityName = giftCertificateDetail.Commodity.Name;

            return model;
        }
    }
}