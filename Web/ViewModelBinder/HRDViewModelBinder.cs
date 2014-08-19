using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;

namespace Cats.ViewModelBinder
{
    public class HRDViewModelBinder
    {

        public static List<HRDCompareViewModel> BindHRDCompareViewModel(HRD hrdOriginal, HRD hrdRefrence, int filterRegion)
        {
            var hrdCompareViewModels = new List<HRDCompareViewModel>();
            if (hrdOriginal == null) return hrdCompareViewModels;
            if (!hrdOriginal.HRDDetails.Any()) return hrdCompareViewModels;

            foreach (var hrdDetail in hrdOriginal.HRDDetails.Where(t => t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == filterRegion))
            {
                var hrdCompareViewModel = new HRDCompareViewModel();
                hrdCompareViewModel.Year = hrdOriginal.Year;
                hrdCompareViewModel.SeasonId = hrdOriginal.SeasonID.HasValue ? hrdOriginal.SeasonID.Value : 0;
                hrdCompareViewModel.Season = hrdOriginal.SeasonID.HasValue ? hrdOriginal.Season.Name : string.Empty;
                hrdCompareViewModel.RationId = hrdOriginal.RationID;
                hrdCompareViewModel.DurationOfAssistance = hrdDetail.DurationOfAssistance;
                hrdCompareViewModel.BeginingMonth = hrdDetail.StartingMonth;
                hrdCompareViewModel.Beneficiaries = hrdDetail.NumberOfBeneficiaries;
                hrdCompareViewModel.ZoneId = hrdDetail.AdminUnit.AdminUnit2.AdminUnitID;
                hrdCompareViewModel.Zone = hrdDetail.AdminUnit.AdminUnit2.Name;
                hrdCompareViewModel.RegionId = hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID;
                hrdCompareViewModel.Region = hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.Name;
                hrdCompareViewModel.WoredaId = hrdDetail.WoredaID;
                hrdCompareViewModel.Woreda = hrdDetail.AdminUnit.Name;
                hrdCompareViewModel.StartingMonth = RequestHelper.MonthName(hrdDetail.StartingMonth);

                if (hrdRefrence != null)
                {
                    var hrdReferenceDetail =
                        hrdRefrence.HRDDetails.FirstOrDefault(t => t.WoredaID == hrdDetail.WoredaID);
                    if (hrdReferenceDetail != null)
                    {
                        hrdCompareViewModel.RefrenceDuration = hrdReferenceDetail.DurationOfAssistance;
                        hrdCompareViewModel.BeginingMonthReference = hrdReferenceDetail.StartingMonth;
                        hrdCompareViewModel.BeneficiariesRefrence = hrdReferenceDetail.NumberOfBeneficiaries;
                        hrdCompareViewModel.StartingMonthReference = RequestHelper.MonthName(hrdReferenceDetail.StartingMonth);
                    }
                }
                hrdCompareViewModels.Add(hrdCompareViewModel);

            }
            return hrdCompareViewModels;
        }

        public static DataTable TransposeData(IEnumerable<HRDDetail> hrdDetails, IEnumerable<RationDetail> rationDetails,string preferedWeight)
        {
            
            var dt = new DataTable("Transpose");

            var colRegion = new DataColumn("Region", typeof(string));
            colRegion.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colRegion);

            var colZone = new DataColumn("Zone", typeof(string));
            colZone.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colZone);

            var colWoreda = new DataColumn("Woreda", typeof(string));
            colWoreda.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colWoreda);


            var colNoBeneficiary = new DataColumn("NoBeneficiary", typeof(int));
            colNoBeneficiary.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colNoBeneficiary);


            var colDuration = new DataColumn("Duration", typeof(int));
            colDuration.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colDuration);

            var colStartingMonth = new DataColumn("Starting Month", typeof(string));
            colStartingMonth.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colStartingMonth);

            if (rationDetails != null)
            {
                foreach (var ds in rationDetails)
                {
                    var col = new DataColumn(ds.Commodity.Name.Trim(), typeof(decimal));
                    col.ExtendedProperties.Add("ID", ds.CommodityID);
                    dt.Columns.Add(col);
                }

                var col1 = new DataColumn("Total", typeof(decimal));
                col1.ExtendedProperties.Add("ID", "Total");
                dt.Columns.Add(col1);
                //int rowID = 0;
                //bool addRow = false;
                //var rowGroups = (from item in mydata select item.MyClassID).Distinct().ToList();
                foreach (var hrdDetail in hrdDetails)
                {
                    var dr = dt.NewRow();
                    //dr[colRequstDetailID] = requestDetail.RegionalRequestDetailID;
                    dr[colRegion] = hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.Name;
                    dr[colZone] = hrdDetail.AdminUnit.AdminUnit2.Name;
                    dr[colWoreda] = hrdDetail.AdminUnit.Name;
                    dr[colNoBeneficiary] = hrdDetail.NumberOfBeneficiaries;
                    dr[colDuration] = hrdDetail.DurationOfAssistance;
                    dr[colStartingMonth] = RequestHelper.MonthName(hrdDetail.StartingMonth);
                    decimal total = 0;
                    decimal ration = 0;



                    var currentUnit = preferedWeight; ;

                   
                    foreach (var rationDetail in rationDetails)
                    {

                        DataColumn col = null;
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (rationDetail.CommodityID.ToString() ==
                                column.ExtendedProperties["ID"].ToString())
                            {
                                col = column;
                                break;
                            }
                        }
                        if (col != null)
                        {
                            var currentUnitUpper = currentUnit.ToUpper().Trim();
                            if (currentUnitUpper == "MT")
                                ration = rationDetail.Amount/1000;
                            else
                                ration = rationDetail.Amount/100;


                            total += ration * hrdDetail.NumberOfBeneficiaries * hrdDetail.DurationOfAssistance;
                            dr[col.ColumnName] = ration * hrdDetail.NumberOfBeneficiaries * hrdDetail.DurationOfAssistance;

                        }
                    }
                    dr[col1] = total;
                    dt.Rows.Add(dr);
                }
            }
            //var dta = (from DataRow row in dt.Rows select new
            //                                                  {

            //                                                  }).ToList();

            return dt;
        }
        public static DataTable TransposeDataSummary(IEnumerable<HRDDetail> hrdDetails, IEnumerable<RationDetail> rationDetails, string preferedWeight)
        {
            var dt = new DataTable("Transpose");

            var colRegion = new DataColumn("Region", typeof(string));
            colRegion.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colRegion);

            var colNoBeneficiary = new DataColumn("No. Beneficiary", typeof(int));
            colNoBeneficiary.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colNoBeneficiary);


            if (rationDetails != null)
            {
                foreach (var ds in rationDetails)
                {
                    var col = new DataColumn(ds.Commodity.Name.Trim() + " (" + preferedWeight.ToUpper().Trim()+")", typeof(decimal));
                    col.ExtendedProperties.Add("ID", ds.CommodityID);
                    dt.Columns.Add(col);
                }

                var colTotal = new DataColumn("Total"+ " (" + preferedWeight.ToUpper().Trim()+")", typeof(decimal));
                colTotal.ExtendedProperties.Add("ID", "Total");
                dt.Columns.Add(colTotal);


                var regions =
                    (from item in hrdDetails
                     select new
                         {
                             item.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID,
                             item.AdminUnit.AdminUnit2.AdminUnit2.Name
                         }
            ).Distinct().ToList();
                foreach (var region in regions)
                {
                    var dr = dt.NewRow();
                    Decimal total = 0;
                    dr[colRegion] = region.Name;
                    int nobenfi =
                        hrdDetails.Where(t => t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == region.AdminUnitID).Sum(
                            t => t.NumberOfBeneficiaries);
                    dr[colNoBeneficiary] = nobenfi;

                    foreach (var rationDetail in rationDetails)
                    {
                        decimal ratio = 0;
                        preferedWeight = preferedWeight.ToUpper();
                        if (preferedWeight.Trim() == "MT")
                            ratio = rationDetail.Amount / 1000;
                        else ratio = rationDetail.Amount/100;

                        DataColumn col = null;
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (rationDetail.CommodityID.ToString() ==
                                column.ExtendedProperties["ID"].ToString())
                            {
                                col = column;
                                break;
                            }
                        }
                        if (col != null)
                        {
                            var regionSum = hrdDetails.Where(t => t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == region.AdminUnitID).Sum(t => t.NumberOfBeneficiaries * t.DurationOfAssistance * ratio);

                            total += regionSum;
                            dr[col.ColumnName] = regionSum;

                        }
                    }
                    dr[colTotal] = total;
                    dt.Rows.Add(dr);
                    
                }

            }
            return dt;
        }



    }
}
