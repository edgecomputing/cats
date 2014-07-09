using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Cats.Helpers;
using Cats.Models;

namespace Cats.ViewModelBinder
{
    public class PSNPPlanViewModelBinder
    {
        public static DataTable TransposeData(IEnumerable<RegionalPSNPPlanDetail> planDetails, IEnumerable<RationDetail> rationDetails, string preferedWeight)
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

            var colFoodRation = new DataColumn("Food Ration", typeof(int));
            colFoodRation.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colFoodRation);

            var colCashRation = new DataColumn("Cash Ration", typeof(int));
            colCashRation.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colCashRation);

            var colStartingMonth = new DataColumn("Starting Month", typeof(string));
            colStartingMonth.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colStartingMonth);

            

            if (rationDetails != null)
            {
                var woredaContingency = new DataColumn("Woreda Contingency (5%)", typeof(decimal));
                woredaContingency.ExtendedProperties.Add("ID", "WoredaContingency");

                var regionContingency = new DataColumn("Regional  Contingency (15%)", typeof(decimal));
                regionContingency.ExtendedProperties.Add("ID", "RegionalContingency");

                foreach (var ds in rationDetails)
                {
                    var col = new DataColumn(ds.Commodity.Name.Trim()+ " in " + preferedWeight.ToUpper().Trim(), typeof(decimal));
                    
                    col.ExtendedProperties.Add("ID", ds.CommodityID);
                    dt.Columns.Add(col);

                   
                    dt.Columns.Add(woredaContingency);

                   
                    dt.Columns.Add(regionContingency);
                   
                }

              
                var col1 = new DataColumn("Total", typeof(decimal));
                col1.ExtendedProperties.Add("ID", "Total");
                dt.Columns.Add(col1);
                //int rowID = 0;
                //bool addRow = false;
                //var rowGroups = (from item in mydata select item.MyClassID).Distinct().ToList();
                foreach (var psnpPlan in planDetails)
                {
                    var dr = dt.NewRow();
                    //dr[colRequstDetailID] = requestDetail.RegionalRequestDetailID;
                    dr[colRegion] = psnpPlan.PlanedWoreda.AdminUnit2.AdminUnit2.Name;
                    dr[colZone] = psnpPlan.PlanedWoreda.AdminUnit2.Name;
                    dr[colWoreda] = psnpPlan.PlanedWoreda.Name;
                    dr[colNoBeneficiary] = psnpPlan.BeneficiaryCount;
                    dr[colFoodRation] = psnpPlan.FoodRatio;
                    dr[colCashRation] = psnpPlan.CashRatio;
                    dr[colStartingMonth] = RequestHelper.MonthName(psnpPlan.StartingMonth);
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
                                ration = rationDetail.Amount / 1000;
                            else
                                ration = rationDetail.Amount / 100;


                            total += ration * psnpPlan.BeneficiaryCount * psnpPlan.FoodRatio;
                            dr[col.ColumnName] = ration * psnpPlan.BeneficiaryCount * psnpPlan.FoodRatio;
                            dr[woredaContingency] = ration * psnpPlan.BeneficiaryCount * psnpPlan.FoodRatio * (decimal)0.05;
                            dr[regionContingency.ColumnName] = ration * psnpPlan.BeneficiaryCount * psnpPlan.FoodRatio * (decimal)0.15;

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
    }
}