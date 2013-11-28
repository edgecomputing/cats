using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;

namespace Cats.ViewModelBinder
{
    public class RequestViewModelBinder
    {


        public static IEnumerable<RegionalRequestViewModel> BindRegionalRequestListViewModel(
          IEnumerable<RegionalRequest> requests, List<WorkflowStatus> statuses, string userPreference)
        {
            var requestsViewModel = new List<RegionalRequestViewModel>();
            foreach (var regionalRequest in requests)
            {
                requestsViewModel.Add(BindRegionalRequestViewModel(regionalRequest, statuses, userPreference));
            }

            return requestsViewModel;
        }

        public static RegionalRequestViewModel BindRegionalRequestViewModel(RegionalRequest regionalRequest, List<WorkflowStatus> statuses, string userPrefrence)
        {
            var regionalRequestViewModel = new RegionalRequestViewModel();


            regionalRequestViewModel.ProgramId = regionalRequest.ProgramId;
            regionalRequestViewModel.Program = regionalRequest.Program.Name;
            regionalRequestViewModel.Region = regionalRequest.AdminUnit.Name;
            regionalRequestViewModel.ReferenceNumber = regionalRequest.ReferenceNumber;
            regionalRequestViewModel.RegionID = regionalRequest.RegionID;
            regionalRequestViewModel.RegionalRequestID = regionalRequest.RegionalRequestID;
            regionalRequestViewModel.Remark = regionalRequest.Remark;
            regionalRequestViewModel.RequestDate = regionalRequest.RequistionDate.ToCTSPreferedDateFormat(userPrefrence);
            regionalRequestViewModel.Round = regionalRequest.Round;

            //RequestDateEt = EthiopianDate.GregorianToEthiopian(regionalRequest.RequistionDate);
            regionalRequestViewModel.MonthName = RequestHelper.GetMonthList().Find(t => t.Id == regionalRequest.Month).Name;
            regionalRequest.Month = regionalRequest.Month;
            regionalRequestViewModel.Status = statuses.Find(t => t.WorkflowID == (int)WORKFLOW.REGIONAL_REQUEST && t.StatusID == regionalRequest.Status).Description;
            // regionalRequestViewModel. RequistionDate = regionalRequest.RequistionDate;
            regionalRequestViewModel.StatusID = regionalRequest.Status;
            regionalRequestViewModel.Ration = regionalRequest.Ration.RefrenceNumber;
            regionalRequestViewModel.RationID = regionalRequest.RationID;
            regionalRequestViewModel.Year = regionalRequest.Year;

            return regionalRequestViewModel;
        }

        public static RegionalRequest BindRegionalRequest(RegionalRequestViewModel regionalRequestViewModel, RegionalRequest request = null)
        {
            if (request == null) request = new RegionalRequest();

            request.ProgramId = regionalRequestViewModel.ProgramId;
            request.ReferenceNumber = regionalRequestViewModel.ReferenceNumber;
            request.RegionID = regionalRequestViewModel.RegionID;
            request.RegionalRequestID = regionalRequestViewModel.RegionalRequestID;
            request.Remark = regionalRequestViewModel.Remark;
            request.Month = regionalRequestViewModel.Month;
            request.RequistionDate = regionalRequestViewModel.RequistionDate;
            request.Round = regionalRequestViewModel.Round;
            request.Status = regionalRequestViewModel.StatusID;
            request.Year = regionalRequestViewModel.Year;
            //  request.DonorID=
            return request;
        }
        
        public static RegionalRequest BindRegionalRequest(RegionalRequest origin, RegionalRequest request = null)
        {
            if (request == null) request = new RegionalRequest();

            request.ProgramId = origin.ProgramId;
            request.ReferenceNumber = origin.ReferenceNumber;
            request.RegionID = origin.RegionID;
            request.RegionalRequestID = origin.RegionalRequestID;
            request.Remark = origin.Remark;
            request.Month = origin.Month;
            request.RequistionDate = origin.RequistionDate;
            request.Status = origin.Status;
            request.Year = origin.Year;
            request.RationID = origin.RationID;
            request.Round = origin.Round;
            return request;
        }

        public static DataTable TransposeDataNew(IEnumerable<PLANWithRegionalRequestViewModel> woredaRequestDetail)
        {
            var dt = new DataTable("Transpose");

            var colZone = new DataColumn("Zone", typeof(string));
            colZone.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colZone);

            var colWoreda = new DataColumn("Woreda", typeof(string));
            colWoreda.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colWoreda);

            var colNoBeneficiary = new DataColumn("Beneficiaries", typeof(int));
            colNoBeneficiary.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colNoBeneficiary);

            var colNoPlannedBeneficiary = new DataColumn("Planned Beneficiaries", typeof(int));
            colNoPlannedBeneficiary.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colNoPlannedBeneficiary);

            var colDifference = new DataColumn("Difference", typeof(int));
            colDifference.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colDifference);

            //var requestdetail = requestDetails.FirstOrDefault();
            //woredaRequestDetail.First().
            
            foreach (var requestDetail in woredaRequestDetail)
            {
                var dr = dt.NewRow();
                
                dr[colZone] = requestDetail.zone;
                dr[colWoreda] = requestDetail.Woreda;
                dr[colNoBeneficiary] = requestDetail.RequestedBeneficiaryNo;
                dr[colNoPlannedBeneficiary] = requestDetail.PlannedBeneficaryNo;
                dr[colDifference] = requestDetail.Difference;
                
                dt.Rows.Add(dr);
            }

            //foreach (var ds in requestdetail.RequestDetailCommodities)
            //{
            //    var col = new DataColumn(ds.Commodity.Name.Trim(), typeof(decimal));
            //    col.ExtendedProperties.Add("ID", ds.CommodityID);
            //    dt.Columns.Add(col);
            //}

            return dt;
           
		    /*
            if (requestdetail != null)
            {
                foreach (var ds in requestdetail.RequestDetailCommodities)
                {
                    var col = new DataColumn(ds.Commodity.Name.Trim(), typeof(decimal));
                    col.ExtendedProperties.Add("ID", ds.CommodityID);
                    dt.Columns.Add(col);
                }

                //int rowID = 0;
                //bool addRow = false;
                //var rowGroups = (from item in mydata select item.MyClassID).Distinct().ToList();
                foreach (var requestDetail in requestDetails)
                {
                    var dr = dt.NewRow();
                    //dr[colRequstDetailID] = requestDetail.RegionalRequestDetailID;
                    dr[colNoBeneficiary] = requestDetail.Beneficiaries;
                    dr[colZone] = requestDetail.Fdp.AdminUnit.AdminUnit2.Name;
                    dr[colWoreda] = requestDetail.Fdp.AdminUnit.Name;
                    //dr[colFDP] = requestDetail.Fdp.Name;

                    foreach (var requestDetailCommodity in requestDetail.RequestDetailCommodities)
                    {
                        DataColumn col = null;
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (requestDetailCommodity.CommodityID.ToString() == column.ExtendedProperties["ID"].ToString())
                            {
                                col = column;
                                break;
                            }
                        }

                        if (col != null)
                        {
                            dr[col.ColumnName] = requestDetailCommodity.Amount.ToPreferedWeightUnit();

                        }
                    }
                    dt.Rows.Add(dr);
                }
            }*/
           // return dt;
        }

        public static  DataTable TransposeData(IEnumerable<RegionalRequestDetail> requestDetails )
        {
            var dt = new DataTable("Transpose");
            //var colRequstDetailID = new DataColumn("RequstDetailID", typeof(int));
            //colRequstDetailID.ExtendedProperties["ID"] = -1;
            //dt.Columns.Add(colRequstDetailID);

            var colZone = new DataColumn("Zone", typeof(string));
            colZone.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colZone);

            var colWoreda = new DataColumn("Woreda", typeof(string));
            colWoreda.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colWoreda);

            var colFDP = new DataColumn("FDP", typeof(string));
            colFDP.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colFDP);

            var colNoBeneficiary = new DataColumn("NoBeneficiary", typeof(int));
            colNoBeneficiary.ExtendedProperties["ID"] = -1;
            dt.Columns.Add(colNoBeneficiary);

            var requestdetail = requestDetails.FirstOrDefault();

            //requestdetail.

            if (requestdetail != null)
            {
                foreach (var ds in requestdetail.RequestDetailCommodities)
                {
                    var col = new DataColumn(ds.Commodity.Name.Trim(), typeof(decimal));
                    col.ExtendedProperties.Add("ID", ds.CommodityID);
                    dt.Columns.Add(col);
                }

                //int rowID = 0; 
                //bool addRow = false;
                //var rowGroups = (from item in mydata select item.MyClassID).Distinct().ToList();
                var groupedByWoreda = (
                                        from regionalRequestDetail in requestDetails
                                        group regionalRequestDetail by regionalRequestDetail.Fdp.AdminUnit
                                        into g
                                        select g
                                      );

                foreach (var requestDetail in requestDetails)
                {
                    var dr = dt.NewRow();
                    //dr[colRequstDetailID] = requestDetail.RegionalRequestDetailID;
                    dr[colNoBeneficiary] = requestDetail.Beneficiaries;
                    dr[colZone] = requestDetail.Fdp.AdminUnit.AdminUnit2.Name;
                    dr[colWoreda] = requestDetail.Fdp.AdminUnit.Name;
                    dr[colFDP] = requestDetail.Fdp.Name;

                    foreach (var requestDetailCommodity in requestDetail.RequestDetailCommodities)
                    {
                        DataColumn col = null;
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (requestDetailCommodity.CommodityID.ToString() ==
                                column.ExtendedProperties["ID"].ToString())
                            {
                                col = column;
                                break;
                            }
                        }

                        if (col != null)
                        {
                            dr[col.ColumnName] = requestDetailCommodity.Amount.ToPreferedWeightUnit();
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}