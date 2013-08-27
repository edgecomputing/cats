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
        public static List<GiftCertificateViewModel> BindListGiftCertificateViewModel(List<GiftCertificate> giftCertificates)
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
            //giftCertificateViewModel.GiftCertificateDetails =
            //   BindListOfGiftCertificateDetailsViewModel(
            //       giftCertificateModel.GiftCertificateDetails.ToList());


            return giftCertificateViewModel;
        }

        public static GiftCertificate BindGiftCertificate(GiftCertificateViewModel giftCertificateViewModel)
        {

            return BindGiftCertificate(new GiftCertificate(), giftCertificateViewModel);

        }
        public static GiftCertificate BindGiftCertificate(GiftCertificate giftCertificate, GiftCertificateViewModel giftCertificateViewModel)
        {

            giftCertificate.GiftCertificateID = giftCertificateViewModel.GiftCertificateID;
            giftCertificate.GiftDate = giftCertificateViewModel.GiftDate;
            giftCertificate.SINumber = giftCertificateViewModel.SINumber;
            giftCertificate.DonorID = giftCertificateViewModel.DonorID;
            giftCertificate.ReferenceNo = giftCertificateViewModel.ReferenceNo;
            giftCertificate.Vessel = giftCertificateViewModel.Vessel;
            giftCertificate.ETA = giftCertificateViewModel.ETA;
            giftCertificate.IsPrinted = giftCertificateViewModel.IsPrinted;
            giftCertificate.DModeOfTransport = giftCertificateViewModel.DModeOfTransport;
            giftCertificate.ProgramID = giftCertificateViewModel.ProgramID;
            giftCertificate.PortName = giftCertificateViewModel.PortName;

            return giftCertificate;
        }

        public static List<GiftCertificateDetail> BindListGiftCertificateDetail(List<GiftCertificateDetailsViewModel> giftCertificateDetailsViewModel)
        {
            return giftCertificateDetailsViewModel.Select(BindGiftCertificateDetail).ToList();
        }

        public static GiftCertificateDetail BindGiftCertificateDetail(GiftCertificateDetailsViewModel giftCertificateDetailsViewModel)
        {

            return BindGiftCertificateDetail(new GiftCertificateDetail(),giftCertificateDetailsViewModel);
        }
        public static GiftCertificateDetail BindGiftCertificateDetail(GiftCertificateDetail giftCertificateDetail, GiftCertificateDetailsViewModel giftCertificateDetailsViewModel)
        {


            giftCertificateDetail.CommodityID = giftCertificateDetailsViewModel.CommodityID;
            giftCertificateDetail.BillOfLoading = giftCertificateDetailsViewModel.BillOfLoading;
            giftCertificateDetail.YearPurchased = giftCertificateDetailsViewModel.YearPurchased;
            giftCertificateDetail.AccountNumber = giftCertificateDetailsViewModel.AccountNumber;
            giftCertificateDetail.WeightInMT = giftCertificateDetailsViewModel.WeightInMT;
            giftCertificateDetail.EstimatedPrice = giftCertificateDetailsViewModel.EstimatedPrice;
            giftCertificateDetail.EstimatedTax = giftCertificateDetailsViewModel.EstimatedTax;
            giftCertificateDetail.DCurrencyID = giftCertificateDetailsViewModel.DCurrencyID;
            giftCertificateDetail.DFundSourceID = giftCertificateDetailsViewModel.DFundSourceID;
            giftCertificateDetail.DBudgetTypeID = giftCertificateDetailsViewModel.DBudgetTypeID;
            giftCertificateDetail.GiftCertificateDetailID = giftCertificateDetailsViewModel.GiftCertificateDetailID;
            giftCertificateDetail.GiftCertificateDetailID = giftCertificateDetailsViewModel.GiftCertificateDetailID;
            giftCertificateDetail.GiftCertificateID = giftCertificateDetailsViewModel.GiftCertificateID;
            giftCertificateDetail.TransactionGroupID = giftCertificateDetailsViewModel.TransactionGroupID;

            return giftCertificateDetail;
        }

        public static List<GiftCertificateDetailsViewModel> BindListOfGiftCertificateDetailsViewModel(List<GiftCertificateDetail> giftCertificateDetails)
        {
            return giftCertificateDetails.Select(BindGiftCertificateDetailsViewModel).ToList();
        }

        public static GiftCertificateDetailsViewModel BindGiftCertificateDetailsViewModel(GiftCertificateDetail giftCertificateDetail)
        {
            var model = new GiftCertificateDetailsViewModel();

            model.GiftCertificateID = giftCertificateDetail.GiftCertificateID;
            model.GiftCertificateDetailID = giftCertificateDetail.GiftCertificateDetailID;
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