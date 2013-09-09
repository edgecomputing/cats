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
          IEnumerable<RegionalRequest> requests,List<WorkflowStatus> statuses )
        {
            var requestsViewModel = new List<RegionalRequestViewModel>();
            foreach (var regionalRequest in requests)
            {
                requestsViewModel.Add(BindRegionalRequestViewModel(regionalRequest,statuses));
            }

            return requestsViewModel;
        }
      
        public static RegionalRequestViewModel BindRegionalRequestViewModel(RegionalRequest regionalRequest,List<WorkflowStatus> statuses )
        {

            return new RegionalRequestViewModel()
            {

                ProgramId = regionalRequest.ProgramId,
                Program = regionalRequest.Program.Name,
                Region = regionalRequest.AdminUnit.Name,
                ReferenceNumber = regionalRequest.ReferenceNumber,
                RegionID = regionalRequest.RegionID,
                RegionalRequestID = regionalRequest.RegionalRequestID,
                Remark = regionalRequest.Remark,
                RequestDateEt = EthiopianDate.GregorianToEthiopian(regionalRequest.RequistionDate),
                Month = regionalRequest.Month,
                Status = statuses.Find(t=>t.WorkflowID==(int)WORKFLOW.REGIONAL_REQUEST && t.StatusID==regionalRequest.Status).Description,
                RequistionDate = regionalRequest.RequistionDate,
                StatusID = regionalRequest.Status,
                Ration = regionalRequest.Ration.RefrenceNumber,
                RationID = regionalRequest.RationID,
                Year = regionalRequest.Year,
            };

        }
        public static RegionalRequest BindRegionalRequest(RegionalRequestViewModel regionalRequestViewModel,RegionalRequest request =null)
        {
            if(request==null) request = new RegionalRequest();

            request.ProgramId = regionalRequestViewModel.ProgramId;
            request.ReferenceNumber = regionalRequestViewModel.ReferenceNumber;
            request.RegionID = regionalRequestViewModel.RegionID;
            request.RegionalRequestID = regionalRequestViewModel.RegionalRequestID;
            request.Remark = regionalRequestViewModel.Remark;
            request.Month = regionalRequestViewModel.Month;
            request.RequistionDate = regionalRequestViewModel.RequistionDate;
            request.Status = regionalRequestViewModel.StatusID;
            request.Year = regionalRequestViewModel.Year;
          //  request.DonorID=
            return request;
        }
                public static RegionalRequest BindRegionalRequest(RegionalRequest origin,RegionalRequest request =null)
        {
            if(request==null) request = new RegionalRequest();

            request.ProgramId = origin.ProgramId;
            request.ReferenceNumber = origin.ReferenceNumber;
            request.RegionID = origin.RegionID;
            request.RegionalRequestID = origin.RegionalRequestID;
            request.Remark = origin.Remark;
            request.Month = origin.Month;
            request.RequistionDate = origin.RequistionDate;
            request.Status = origin.Status;
            request.Year = origin.Year;
            return request;
        }

                public static DataTable TransposeData(IEnumerable<RegionalRequestDetail> requestDetails)
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

                    var colNoBeneficiary = new DataColumn("NoBeneficiary", typeof(decimal));
                    colNoBeneficiary.ExtendedProperties["ID"] = -1;
                    dt.Columns.Add(colNoBeneficiary);
                    var requestdetail = requestDetails.FirstOrDefault();
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
                                    dr[col.ColumnName] = requestDetailCommodity.Amount;

                                }
                            }
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