using System;
namespace Cats.Security
{
    #region EarlyWarning Security Constants

    public class EarlyWarningConstants
    {
        #region Methods

        /// <summary>
        /// Retrieve Item name from a Role Enum.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The Role Name.</returns>
        public virtual string ItemName(Role role)
        {
            if ((role == Role.EW_Coordinator))
            {
                return "EW Coordinator";
            }
            if ((role == Role.EW_Custom_Officer))
            {
                return "EW-Custom Officer";
            }
            if ((role == Role.EW_Director__EWRD))
            {
                return "EW-Director (EWRD)";
            }
            if ((role == Role.EW_Experts))
            {
                return "EW-Experts";
            }
            if ((role == Role.EW_Logistics_Planner))
            {
                return "EW-Logistics Planner";
            }
            if ((role == Role.EW_Minister__MoA))
            {
                return "EW-Minister (MoA)";
            }
            if ((role == Role.EW_National_DPPC))
            {
                return "EW-National DPPC";
            }
            if ((role == Role.EW_Other_Stakeholders))
            {
                return "EW-Other Stakeholders";
            }
            if ((role == Role.EW_Regional_DPPBs))
            {
                return "EW-Regional DPPBs";
            }
            if ((role == Role.EW_Resource_Mobilization_Expert))
            {
                return "EW-Resource Mobilization Expert";
            }
            if ((role == Role.EW_State_Minster__DRMFSS))
            {
                return "EW-State Minster (DRMFSS)";
            }
            throw new System.ArgumentException("Unknown Role name", "role");
        }
        /// <summary>
        /// Retrieve Item name from a Task Enum.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The Task Name.</returns>
        public virtual string ItemName(Task task)
        {
            if ((task == Task.Manage_Gift_Certificate))
            {
                return "Manage Gift Certificate";
            }
            if ((task == Task.Manage_HRD))
            {
                return "Manage HRD";
            }
            if ((task == Task.Manage_Monthly_Requests))
            {
                return "Manage Monthly Requests";
            }
            if ((task == Task.Manage_Ration))
            {
                return "Manage Ration";
            }
            if ((task == Task.Manage_requisition))
            {
                return "Manage requisition";
            }
            if ((task == Task.Modify_Allocation))
            {
                return "Modify Allocation";
            }
            if ((task == Task.Needs_assessment))
            {
                return "Needs assessment";
            }
            throw new System.ArgumentException("Unknown Task name", "task");
        }
        /// <summary>
        /// Retrieve Item name from a Operation Enum.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The Operation Name.</returns>
        public virtual string ItemName(Operation operation)
        {
            if ((operation == Operation.Add_new_commodity_allocation))
            {
                return "Add new commodity allocation";
            }
            if ((operation == Operation.Add_new_ration))
            {
                return "Add new ration";
            }
            if ((operation == Operation.Add_new_request))
            {
                return "Add new request";
            }
            if ((operation == Operation.Approve_Gift_Certeficate))
            {
                return "Approve Gift Certeficate";
            }
            if ((operation == Operation.Approve_HRD))
            {
                return "Approve HRD";
            }
            if ((operation == Operation.Approve_Need_Assessment))
            {
                return "Approve Need Assessment";
            }
            if ((operation == Operation.Approve_Request))
            {
                return "Approve Request";
            }
            if ((operation == Operation.Approve_Requisition))
            {
                return "Approve Requisition";
            }
            if ((operation == Operation.Commodity_Allocation))
            {
                return "Commodity Allocation";
            }
            if ((operation == Operation.Compare_HRD))
            {
                return "Compare HRD";
            }
            if ((operation == Operation.Create_new_needs_assessment))
            {
                return "Create new needs assessment";
            }
            if ((operation == Operation.Delete_Gift_Certificate))
            {
                return "Delete Gift Certificate";
            }
            if ((operation == Operation.Delete_needs_assessment))
            {
                return "Delete needs assessment";
            }
            if ((operation == Operation.Edit_Gift_Certificate))
            {
                return "Edit Gift Certificate";
            }
            if ((operation == Operation.Edit_needs_assessment))
            {
                return "Edit needs assessment";
            }
            if ((operation == Operation.Edit_ration))
            {
                return "Edit ration";
            }
            if ((operation == Operation.Edit_request))
            {
                return "Edit request";
            }
            if ((operation == Operation.Edit_requisition))
            {
                return "Edit requisition";
            }
            if ((operation == Operation.Export_Gift_Certificate))
            {
                return "Export Gift Certificate";
            }
            if ((operation == Operation.Export_HRD))
            {
                return "Export HRD";
            }
            if ((operation == Operation.Export_needs_assessment))
            {
                return "Export needs assessment";
            }
            if ((operation == Operation.Export_ration))
            {
                return "Export ration";
            }
            if ((operation == Operation.Export_requests))
            {
                return "Export requests";
            }
            if ((operation == Operation.Export_requisitions))
            {
                return "Export requisitions";
            }
            if ((operation == Operation.Followup_requisitions))
            {
                return "Followup requisitions";
            }
            if ((operation == Operation.Generate_Gift_Certificate_Template))
            {
                return "Generate Gift Certificate Template";
            }
            if ((operation == Operation.Gift_Add_new_item))
            {
                return "Gift-Add new item";
            }
            if ((operation == Operation.HRD_Summary))
            {
                return "HRD Summary";
            }
            if ((operation == Operation.Modify_HRD))
            {
                return "Modify HRD";
            }
            if ((operation == Operation.New_Gift_Certificate))
            {
                return "New Gift Certificate";
            }
            if ((operation == Operation.New_HRD))
            {
                return "New HRD";
            }
            if ((operation == Operation.Number_of_Beneficiaries))
            {
                return "Number of Beneficiaries";
            }
            if ((operation == Operation.Print_Gift_Certificate))
            {
                return "Print Gift Certificate";
            }
            if ((operation == Operation.Print_HRD))
            {
                return "Print HRD";
            }
            if ((operation == Operation.Print_needs_assessment))
            {
                return "Print needs assessment";
            }
            if ((operation == Operation.Print_ration))
            {
                return "Print ration";
            }
            if ((operation == Operation.Print_request))
            {
                return "Print request";
            }
            if ((operation == Operation.Print_requisitions))
            {
                return "Print requisitions";
            }
            if ((operation == Operation.Ration__Add_new_item))
            {
                return "Ration- Add new item";
            }
            if ((operation == Operation.Request_Allocation))
            {
                return "Request Allocation";
            }
            if ((operation == Operation.Requisition_exceptions))
            {
                return "Requisition exceptions";
            }
            if ((operation == Operation.Set_Default_Ration))
            {
                return "Set Default Ration";
            }
            if ((operation == Operation.View_Approved_Gift_Certificate))
            {
                return "View Approved Gift Certificate";
            }
            if ((operation == Operation.View_Approved_HRD))
            {
                return "View Approved HRD";
            }
            if ((operation == Operation.View_approved_needs_assesment))
            {
                return "View approved needs assesment";
            }
            if ((operation == Operation.View_Approved_Request))
            {
                return "View Approved Request";
            }
            if ((operation == Operation.View_approved_requisitions))
            {
                return "View approved requisitions";
            }
            if ((operation == Operation.View_Beneficiary_no_and_duration_of_assisstance))
            {
                return "View Beneficiary no and duration of assisstance";
            }
            if ((operation == Operation.View_Current_HRD))
            {
                return "View Current HRD";
            }
            if ((operation == Operation.View_Draft_Needs_Assessment))
            {
                return "View Draft Needs Assessment";
            }
            if ((operation == Operation.View_Draft_requests))
            {
                return "View Draft requests";
            }
            if ((operation == Operation.View_draft_requisition))
            {
                return "View draft requisition";
            }
            if ((operation == Operation.View_Gift_Certificate_list))
            {
                return "View Gift Certificate list";
            }
            if ((operation == Operation.View_HRD_Detail))
            {
                return "View HRD Detail";
            }
            if ((operation == Operation.View_HRD_list))
            {
                return "View HRD list";
            }
            if ((operation == Operation.View_Hub_Assigned_Requisition))
            {
                return "View Hub Assigned Requisition";
            }
            if ((operation == Operation.View_Need_Assessment_Detail))
            {
                return "View Need Assessment Detail";
            }
            if ((operation == Operation.View_PC_SI_Assigned_Requisition))
            {
                return "View PC/SI Assigned Requisition";
            }
            if ((operation == Operation.View_Ration_List))
            {
                return "View Ration List";
            }
            if ((operation == Operation.View_request))
            {
                return "View request";
            }
            if ((operation == Operation.View_Requisition))
            {
                return "View Requisition";
            }
            if ((operation == Operation.View_submitted_requests))
            {
                return "View submitted requests";
            }
            throw new System.ArgumentException("Unknown Operation name", "operation");
        }
        #endregion

        #region Enums
        /// <summary>
        /// Roles Enumeration
        /// </summary>
        public enum Role
        {
            /// <summary>
            /// Role EW Coordinator
            /// </summary>
            EW_Coordinator,
            /// <summary>
            /// Role EW-Custom Officer
            /// </summary>
            EW_Custom_Officer,
            /// <summary>
            /// Role EW-Director (EWRD)
            /// </summary>
            EW_Director__EWRD,
            /// <summary>
            /// Role EW-Experts
            /// </summary>
            EW_Experts,
            /// <summary>
            /// Role EW-Logistics Planner
            /// </summary>
            EW_Logistics_Planner,
            /// <summary>
            /// Role EW-Minister (MoA)
            /// </summary>
            EW_Minister__MoA,
            /// <summary>
            /// Role EW-National DPPC
            /// </summary>
            EW_National_DPPC,
            /// <summary>
            /// Role EW-Other Stakeholders
            /// </summary>
            EW_Other_Stakeholders,
            /// <summary>
            /// Role EW-Regional DPPBs
            /// </summary>
            EW_Regional_DPPBs,
            /// <summary>
            /// Role EW-Resource Mobilization Expert
            /// </summary>
            EW_Resource_Mobilization_Expert,
            /// <summary>
            /// Role EW-State Minster (DRMFSS)
            /// </summary>
            EW_State_Minster__DRMFSS,
        }
        /// <summary>
        /// Tasks Enumeration
        /// </summary>
        public enum Task
        {
            /// <summary>
            /// Task Manage Gift Certificate
            /// </summary>
            Manage_Gift_Certificate,
            /// <summary>
            /// Task Manage HRD
            /// </summary>
            Manage_HRD,
            /// <summary>
            /// Task Manage Monthly Requests
            /// </summary>
            Manage_Monthly_Requests,
            /// <summary>
            /// Task Manage Ration
            /// </summary>
            Manage_Ration,
            /// <summary>
            /// Task Manage requisition
            /// </summary>
            Manage_requisition,
            /// <summary>
            /// Task Modify Allocation
            /// </summary>
            Modify_Allocation,
            /// <summary>
            /// Task Needs assessment
            /// </summary>
            Needs_assessment,
        }
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Add new commodity allocation
            /// </summary>
            Add_new_commodity_allocation,
            /// <summary>
            /// Operation Add new ration
            /// </summary>
            Add_new_ration,
            /// <summary>
            /// Operation Add new request
            /// </summary>
            Add_new_request,
            /// <summary>
            /// Operation Approve Gift Certeficate
            /// </summary>
            Approve_Gift_Certeficate,
            /// <summary>
            /// Operation Approve HRD
            /// </summary>
            Approve_HRD,
            /// <summary>
            /// Operation Approve Need Assessment
            /// </summary>
            Approve_Need_Assessment,
            /// <summary>
            /// Operation Approve Request
            /// </summary>
            Approve_Request,
            /// <summary>
            /// Operation Approve Requisition
            /// </summary>
            Approve_Requisition,
            /// <summary>
            /// Operation Commodity Allocation
            /// </summary>
            Commodity_Allocation,
            /// <summary>
            /// Operation Compare HRD
            /// </summary>
            Compare_HRD,
            /// <summary>
            /// Operation Create new needs assessment
            /// </summary>
            Create_new_needs_assessment,
            /// <summary>
            /// Operation Delete Gift Certificate
            /// </summary>
            Delete_Gift_Certificate,
            /// <summary>
            /// Operation Delete needs assessment
            /// </summary>
            Delete_needs_assessment,
            /// <summary>
            /// Operation Edit Gift Certificate
            /// </summary>
            Edit_Gift_Certificate,
            /// <summary>
            /// Operation Edit needs assessment
            /// </summary>
            Edit_needs_assessment,
            /// <summary>
            /// Operation Edit ration
            /// </summary>
            Edit_ration,
            /// <summary>
            /// Operation Edit request
            /// </summary>
            Edit_request,
            /// <summary>
            /// Operation Edit requisition
            /// </summary>
            Edit_requisition,
            /// <summary>
            /// Operation Export Gift Certificate
            /// </summary>
            Export_Gift_Certificate,
            /// <summary>
            /// Operation Export HRD
            /// </summary>
            Export_HRD,
            /// <summary>
            /// Operation Export needs assessment
            /// </summary>
            Export_needs_assessment,
            /// <summary>
            /// Operation Export ration
            /// </summary>
            Export_ration,
            /// <summary>
            /// Operation Export requests
            /// </summary>
            Export_requests,
            /// <summary>
            /// Operation Export requisitions
            /// </summary>
            Export_requisitions,
            /// <summary>
            /// Operation Followup requisitions
            /// </summary>
            Followup_requisitions,
            /// <summary>
            /// Operation Generate Gift Certificate Template
            /// </summary>
            Generate_Gift_Certificate_Template,
            /// <summary>
            /// Operation Gift-Add new item
            /// </summary>
            Gift_Add_new_item,
            /// <summary>
            /// Operation HRD Summary
            /// </summary>
            HRD_Summary,
            /// <summary>
            /// Operation Modify HRD
            /// </summary>
            Modify_HRD,
            /// <summary>
            /// Operation New Gift Certificate
            /// </summary>
            New_Gift_Certificate,
            /// <summary>
            /// Operation New HRD
            /// </summary>
            New_HRD,
            /// <summary>
            /// Operation Number of Beneficiaries
            /// </summary>
            Number_of_Beneficiaries,
            /// <summary>
            /// Operation Print Gift Certificate
            /// </summary>
            Print_Gift_Certificate,
            /// <summary>
            /// Operation Print HRD
            /// </summary>
            Print_HRD,
            /// <summary>
            /// Operation Print needs assessment
            /// </summary>
            Print_needs_assessment,
            /// <summary>
            /// Operation Print ration
            /// </summary>
            Print_ration,
            /// <summary>
            /// Operation Print request
            /// </summary>
            Print_request,
            /// <summary>
            /// Operation Print requisitions
            /// </summary>
            Print_requisitions,
            /// <summary>
            /// Operation Ration- Add new item
            /// </summary>
            Ration__Add_new_item,
            /// <summary>
            /// Operation Request Allocation
            /// </summary>
            Request_Allocation,
            /// <summary>
            /// Operation Requisition exceptions
            /// </summary>
            Requisition_exceptions,
            /// <summary>
            /// Operation Set Default Ration
            /// </summary>
            Set_Default_Ration,
            /// <summary>
            /// Operation View Approved Gift Certificate
            /// </summary>
            View_Approved_Gift_Certificate,
            /// <summary>
            /// Operation View Approved HRD
            /// </summary>
            View_Approved_HRD,
            /// <summary>
            /// Operation View approved needs assesment
            /// </summary>
            View_approved_needs_assesment,
            /// <summary>
            /// Operation View Approved Request
            /// </summary>
            View_Approved_Request,
            /// <summary>
            /// Operation View approved requisitions
            /// </summary>
            View_approved_requisitions,
            /// <summary>
            /// Operation View Beneficiary no and duration of assisstance
            /// </summary>
            View_Beneficiary_no_and_duration_of_assisstance,
            /// <summary>
            /// Operation View Current HRD
            /// </summary>
            View_Current_HRD,
            /// <summary>
            /// Operation View Draft Needs Assessment
            /// </summary>
            View_Draft_Needs_Assessment,
            /// <summary>
            /// Operation View Draft requests
            /// </summary>
            View_Draft_requests,
            /// <summary>
            /// Operation View draft requisition
            /// </summary>
            View_draft_requisition,
            /// <summary>
            /// Operation View Gift Certificate list
            /// </summary>
            View_Gift_Certificate_list,
            /// <summary>
            /// Operation View HRD Detail
            /// </summary>
            View_HRD_Detail,
            /// <summary>
            /// Operation View HRD list
            /// </summary>
            View_HRD_list,
            /// <summary>
            /// Operation View Hub Assigned Requisition
            /// </summary>
            View_Hub_Assigned_Requisition,
            /// <summary>
            /// Operation View Need Assessment Detail
            /// </summary>
            View_Need_Assessment_Detail,
            /// <summary>
            /// Operation View PC/SI Assigned Requisition
            /// </summary>
            View_PC_SI_Assigned_Requisition,
            /// <summary>
            /// Operation View Ration List
            /// </summary>
            View_Ration_List,
            /// <summary>
            /// Operation View request
            /// </summary>
            View_request,
            /// <summary>
            /// Operation View Requisition
            /// </summary>
            View_Requisition,
            /// <summary>
            /// Operation View submitted requests
            /// </summary>
            View_submitted_requests,
        }

        #endregion

    }
    #endregion
    #region PSNP Security Constants
    public class PsnpConstants
    {
        #region Methods

        /// <summary>
        /// Retrieve Item name from a Role Enum.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The Role Name.</returns>
        public virtual string ItemName(Role role)
        {
            if ((role == Role.PSNP_Case_Team_Coordinator))
            {
                return "PSNP Case Team Coordinator";
            }
            if ((role == Role.PSNP_FIC_Expert))
            {
                return "PSNP-FIC Expert";
            }
            if ((role == Role.PSNP_Food_Security_Director))
            {
                return "PSNP-Food Security Director";
            }
            if ((role == Role.PSNP_FSCD_Staffs))
            {
                return "PSNP-FSCD Staffs";
            }
            if ((role == Role.PSNP_M_E_Expert))
            {
                return "PSNP-M&E Expert";
            }
            if ((role == Role.PSNP_Resource_Mobilization___Planning_Expert))
            {
                return "PSNP-Resource Mobilization & Planning Expert";
            }
            if ((role == Role.PSNP_RIC_Expert))
            {
                return "PSNP-RIC Expert";
            }
            throw new System.ArgumentException("Unknown Role name", "role");
        }
        /// <summary>
        /// Retrieve Item name from a Task Enum.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The Task Name.</returns>
        public virtual string ItemName(Task task)
        {
            if ((task == Task.Approve_annual_plan))
            {
                return "Approve annual plan";
            }
            if ((task == Task.Manage_annual_plan))
            {
                return "Manage annual plan";
            }
            throw new System.ArgumentException("Unknown Task name", "task");
        }
        /// <summary>
        /// Retrieve Item name from a Operation Enum.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The Operation Name.</returns>
        public virtual string ItemName(Operation operation)
        {
            if ((operation == Operation.Ask_approval))
            {
                return "Ask approval";
            }
            if ((operation == Operation.Complete_annual_plan))
            {
                return "Complete annual plan";
            }
            if ((operation == Operation.Edit_annual_plan))
            {
                return "Edit annual plan";
            }
            if ((operation == Operation.Export_annual_plan))
            {
                return "Export annual plan";
            }
            if ((operation == Operation.New_annual_plan))
            {
                return "New annual plan";
            }
            if ((operation == Operation.Print_annual_plan))
            {
                return "Print annual plan";
            }
            if ((operation == Operation.Regional_PSNP_Plan))
            {
                return "Regional PSNP Plan";
            }
            if ((operation == Operation.Request_plan_revision))
            {
                return "Request plan revision";
            }
            if ((operation == Operation.Submit_plan_revision))
            {
                return "Submit plan revision";
            }
            if ((operation == Operation.View_annual_plan_list))
            {
                return "View annual plan list";
            }
            if ((operation == Operation.View_Ration_List))
            {
                return "View Ration List";
            }
            if ((operation == Operation.View_request))
            {
                return "View request";
            }
            if ((operation == Operation.View_Requisition))
            {
                return "View Requisition";
            }
            throw new System.ArgumentException("Unknown Operation name", "operation");
        }

        #endregion
        #region Enums
        /// <summary>
        /// Roles Enumeration
        /// </summary>
        public enum Role
        {
            /// <summary>
            /// Role PSNP Case Team Coordinator
            /// </summary>
            PSNP_Case_Team_Coordinator,
            /// <summary>
            /// Role PSNP-FIC Expert
            /// </summary>
            PSNP_FIC_Expert,
            /// <summary>
            /// Role PSNP-Food Security Director
            /// </summary>
            PSNP_Food_Security_Director,
            /// <summary>
            /// Role PSNP-FSCD Staffs
            /// </summary>
            PSNP_FSCD_Staffs,
            /// <summary>
            /// Role PSNP-M&E Expert
            /// </summary>
            PSNP_M_E_Expert,
            /// <summary>
            /// Role PSNP-Resource Mobilization & Planning Expert
            /// </summary>
            PSNP_Resource_Mobilization___Planning_Expert,
            /// <summary>
            /// Role PSNP-RIC Expert
            /// </summary>
            PSNP_RIC_Expert,
        }
        /// <summary>
        /// Tasks Enumeration
        /// </summary>
        public enum Task
        {
            /// <summary>
            /// Task Approve annual plan
            /// </summary>
            Approve_annual_plan,
            /// <summary>
            /// Task Manage annual plan
            /// </summary>
            Manage_annual_plan,
        }
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Ask approval
            /// </summary>
            Ask_approval,
            /// <summary>
            /// Operation Complete annual plan
            /// </summary>
            Complete_annual_plan,
            /// <summary>
            /// Operation Edit annual plan
            /// </summary>
            Edit_annual_plan,
            /// <summary>
            /// Operation Export annual plan
            /// </summary>
            Export_annual_plan,
            /// <summary>
            /// Operation New annual plan
            /// </summary>
            New_annual_plan,
            /// <summary>
            /// Operation Print annual plan
            /// </summary>
            Print_annual_plan,
            /// <summary>
            /// Operation Regional PSNP Plan
            /// </summary>
            Regional_PSNP_Plan,
            /// <summary>
            /// Operation Request plan revision
            /// </summary>
            Request_plan_revision,
            /// <summary>
            /// Operation Submit plan revision
            /// </summary>
            Submit_plan_revision,
            /// <summary>
            /// Operation View annual plan list
            /// </summary>
            View_annual_plan_list,
            /// <summary>
            /// Operation View Ration List
            /// </summary>
            View_Ration_List,
            /// <summary>
            /// Operation View request
            /// </summary>
            View_request,
            /// <summary>
            /// Operation View Requisition
            /// </summary>
            View_Requisition,
        }
        #endregion
    }
    #endregion
    #region Logistics Security Constants
    public class LogisticsConstants
    {
        #region Methods
        /// <summary>
        /// Retrieve Item name from a Role Enum.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The Role Name.</returns>
        public virtual string ItemName(Role role)
        {
            if ((role == Role.LG_Account_Verification_Expert_issue))
            {
                return "LG-Account Verification Expert(issue)";
            }
            if ((role == Role.LG_Addis_Storage___Distribution))
            {
                return "LG-Addis Storage & Distribution";
            }
            if ((role == Role.LG_Agreement___Processor_issue))
            {
                return "LG-Agreement & Processor(issue)";
            }
            if ((role == Role.LG_Allocation_Award___Followup))
            {
                return "LG-Allocation Award & Followup";
            }
            if ((role == Role.LG_Coordinator))
            {
                return "LG-Coordinator";
            }
            if ((role == Role.LG_Coordinator_issue))
            {
                return "LG-Coordinator(issue)";
            }
            if ((role == Role.LG_Data_Encoder_issue))
            {
                return "LG-Data Encoder(issue)";
            }
            if ((role == Role.LG_Evaluation))
            {
                return "LG-Evaluation";
            }
            if ((role == Role.LG_Followup_Expert_issue))
            {
                return "LG-Followup Expert(issue)";
            }
            if ((role == Role.LG_Legal_Officer))
            {
                return "LG-Legal Officer";
            }
            if ((role == Role.LG_Monitoring___Evaluation_utilization))
            {
                return "LG-Monitoring & Evaluation(utilization)";
            }
            if ((role == Role.LG_Performance_Evaluator))
            {
                return "LG-Performance Evaluator";
            }
            if ((role == Role.LG_Pipeline_Officer))
            {
                return "LG-Pipeline Officer";
            }
            if ((role == Role.LG_Report_Compilation))
            {
                return "LG-Report Compilation";
            }
            if ((role == Role.LG_Secretory))
            {
                return "LG-Secretory";
            }
            if ((role == Role.LG_Storage___Distribution_Expert_issue))
            {
                return "LG-Storage & Distribution Expert(issue)";
            }
            if ((role == Role.LG_Store_Keeper))
            {
                return "LG-Store Keeper";
            }
            if ((role == Role.LG_Transport_Officer))
            {
                return "LG-Transport Officer";
            }
            if ((role == Role.LG_Transport_Order_Issue_issue))
            {
                return "LG-Transport Order Issue(issue)";
            }
            throw new System.ArgumentException("Unknown Role name", "role");
        }
        /// <summary>
        /// Retrieve Item name from a Task Enum.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The Task Name.</returns>
        public virtual string ItemName(Task task)
        {
            if ((task == Task.Approve_bid))
            {
                return "Approve bid";
            }
            if ((task == Task.Bid_winner_transporters))
            {
                return "Bid winner transporters";
            }
            if ((task == Task.Dispatch_Allocation))
            {
                return "Dispatch Allocation";
            }
            if ((task == Task.Logistic_Dashboard))
            {
                return "Logistic Dashboard";
            }
            if ((task == Task.Manage_RFQ))
            {
                return "Manage RFQ";
            }
            if ((task == Task.Manage_transport_suppliers))
            {
                return "Manage transport suppliers";
            }
            if ((task == Task.Resource_Allocation))
            {
                return "Resource Allocation";
            }
            if ((task == Task.Stock))
            {
                return "Stock";
            }
            if ((task == Task.Transport_Order))
            {
                return "Transport Order";
            }
            if ((task == Task.Transport_Requisition))
            {
                return "Transport Requisition";
            }
            throw new System.ArgumentException("Unknown Task name", "task");
        }
        /// <summary>
        /// Retrieve Item name from a Operation Enum.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The Operation Name.</returns>
        public virtual string ItemName(Operation operation)
        {
            if ((operation == Operation.Assign_Hub))
            {
                return "Assign Hub";
            }
            if ((operation == Operation.Delete_transporter))
            {
                return "Delete transporter";
            }
            if ((operation == Operation.Edit__transport_order))
            {
                return "Edit  transport order";
            }
            if ((operation == Operation.Edit_transport_supplier))
            {
                return "Edit transport supplier";
            }
            if ((operation == Operation.Export_bid_winner_list))
            {
                return "Export bid winner list";
            }
            if ((operation == Operation.Export_hub_allocation))
            {
                return "Export hub allocation";
            }
            if ((operation == Operation.Export_transport_order))
            {
                return "Export transport order";
            }
            if ((operation == Operation.Export_transporters_list))
            {
                return "Export transporters list";
            }
            if ((operation == Operation.Generate_TR))
            {
                return "Generate TR";
            }
            if ((operation == Operation.Hub_Allocation))
            {
                return "Hub Allocation";
            }
            if ((operation == Operation.New_transport_order))
            {
                return "New transport order";
            }
            if ((operation == Operation.New_transport_supplier))
            {
                return "New transport supplier";
            }
            if ((operation == Operation.PC_SI_Code_Allocation))
            {
                return "PC/SI Code Allocation";
            }
            if ((operation == Operation.Price_quatation_data_entry))
            {
                return "Price quatation data entry";
            }
            if ((operation == Operation.Print_bid_winners_list))
            {
                return "Print bid winners list";
            }
            if ((operation == Operation.Print_hub_allocation))
            {
                return "Print hub allocation";
            }
            if ((operation == Operation.Print_RFQ))
            {
                return "Print RFQ";
            }
            if ((operation == Operation.Print_transport_order))
            {
                return "Print transport order";
            }
            if ((operation == Operation.Print_transporters_list))
            {
                return "Print transporters list";
            }
            if ((operation == Operation.View_active_contracts))
            {
                return "View active contracts";
            }
            if ((operation == Operation.View_allocated_hubs))
            {
                return "View allocated hubs";
            }
            if ((operation == Operation.View_bid_winner_transporters))
            {
                return "View bid winner transporters";
            }
            if ((operation == Operation.View_contract_history))
            {
                return "View contract history";
            }
            if ((operation == Operation.View_Destinations))
            {
                return "View Destinations";
            }
            if ((operation == Operation.View_Dispatch_Allocation))
            {
                return "View Dispatch Allocation";
            }
            if ((operation == Operation.View_draft_hub_allocation))
            {
                return "View draft hub allocation";
            }
            if ((operation == Operation.View_Transport_Requisition))
            {
                return "View Transport Requisition";
            }
            if ((operation == Operation.View_Transport_Requisition_Detail))
            {
                return "View Transport Requisition Detail";
            }
            if ((operation == Operation.View_transporter_list))
            {
                return "View transporter list";
            }
            throw new System.ArgumentException("Unknown Operation name", "operation");
        }
        
        #endregion
        
        #region Enums
        /// <summary>
        /// Roles Enumeration
        /// </summary>
        public enum Role
        {
            /// <summary>
            /// Role LG-Account Verification Expert(issue)
            /// </summary>
            LG_Account_Verification_Expert_issue,
            /// <summary>
            /// Role LG-Addis Storage & Distribution
            /// </summary>
            LG_Addis_Storage___Distribution,
            /// <summary>
            /// Role LG-Agreement & Processor(issue)
            /// </summary>
            LG_Agreement___Processor_issue,
            /// <summary>
            /// Role LG-Allocation Award & Followup
            /// </summary>
            LG_Allocation_Award___Followup,
            /// <summary>
            /// Role LG-Coordinator
            /// </summary>
            LG_Coordinator,
            /// <summary>
            /// Role LG-Coordinator(issue)
            /// </summary>
            LG_Coordinator_issue,
            /// <summary>
            /// Role LG-Data Encoder(issue)
            /// </summary>
            LG_Data_Encoder_issue,
            /// <summary>
            /// Role LG-Evaluation
            /// </summary>
            LG_Evaluation,
            /// <summary>
            /// Role LG-Followup Expert(issue)
            /// </summary>
            LG_Followup_Expert_issue,
            /// <summary>
            /// Role LG-Legal Officer
            /// </summary>
            LG_Legal_Officer,
            /// <summary>
            /// Role LG-Monitoring & Evaluation(utilization)
            /// </summary>
            LG_Monitoring___Evaluation_utilization,
            /// <summary>
            /// Role LG-Performance Evaluator
            /// </summary>
            LG_Performance_Evaluator,
            /// <summary>
            /// Role LG-Pipeline Officer
            /// </summary>
            LG_Pipeline_Officer,
            /// <summary>
            /// Role LG-Report Compilation
            /// </summary>
            LG_Report_Compilation,
            /// <summary>
            /// Role LG-Secretory
            /// </summary>
            LG_Secretory,
            /// <summary>
            /// Role LG-Storage & Distribution Expert(issue)
            /// </summary>
            LG_Storage___Distribution_Expert_issue,
            /// <summary>
            /// Role LG-Store Keeper
            /// </summary>
            LG_Store_Keeper,
            /// <summary>
            /// Role LG-Transport Officer
            /// </summary>
            LG_Transport_Officer,
            /// <summary>
            /// Role LG-Transport Order Issue(issue)
            /// </summary>
            LG_Transport_Order_Issue_issue,
        }
        /// <summary>
        /// Tasks Enumeration
        /// </summary>
        public enum Task
        {
            /// <summary>
            /// Task Approve bid
            /// </summary>
            Approve_bid,
            /// <summary>
            /// Task Bid winner transporters
            /// </summary>
            Bid_winner_transporters,
            /// <summary>
            /// Task Dispatch Allocation
            /// </summary>
            Dispatch_Allocation,
            /// <summary>
            /// Task Logistic Dashboard
            /// </summary>
            Logistic_Dashboard,
            /// <summary>
            /// Task Manage RFQ
            /// </summary>
            Manage_RFQ,
            /// <summary>
            /// Task Manage transport suppliers
            /// </summary>
            Manage_transport_suppliers,
            /// <summary>
            /// Task Resource Allocation
            /// </summary>
            Resource_Allocation,
            /// <summary>
            /// Task Stock
            /// </summary>
            Stock,
            /// <summary>
            /// Task Transport Order
            /// </summary>
            Transport_Order,
            /// <summary>
            /// Task Transport Requisition
            /// </summary>
            Transport_Requisition,
        }
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Assign Hub
            /// </summary>
            Assign_Hub,
            /// <summary>
            /// Operation Delete transporter
            /// </summary>
            Delete_transporter,
            /// <summary>
            /// Operation Edit  transport order
            /// </summary>
            Edit__transport_order,
            /// <summary>
            /// Operation Edit transport supplier
            /// </summary>
            Edit_transport_supplier,
            /// <summary>
            /// Operation Export bid winner list
            /// </summary>
            Export_bid_winner_list,
            /// <summary>
            /// Operation Export hub allocation
            /// </summary>
            Export_hub_allocation,
            /// <summary>
            /// Operation Export transport order
            /// </summary>
            Export_transport_order,
            /// <summary>
            /// Operation Export transporters list
            /// </summary>
            Export_transporters_list,
            /// <summary>
            /// Operation Generate TR
            /// </summary>
            Generate_TR,
            /// <summary>
            /// Operation Hub Allocation
            /// </summary>
            Hub_Allocation,
            /// <summary>
            /// Operation New transport order
            /// </summary>
            New_transport_order,
            /// <summary>
            /// Operation New transport supplier
            /// </summary>
            New_transport_supplier,
            /// <summary>
            /// Operation PC/SI Code Allocation
            /// </summary>
            PC_SI_Code_Allocation,
            /// <summary>
            /// Operation Price quatation data entry
            /// </summary>
            Price_quatation_data_entry,
            /// <summary>
            /// Operation Print bid winners list
            /// </summary>
            Print_bid_winners_list,
            /// <summary>
            /// Operation Print hub allocation
            /// </summary>
            Print_hub_allocation,
            /// <summary>
            /// Operation Print RFQ
            /// </summary>
            Print_RFQ,
            /// <summary>
            /// Operation Print transport order
            /// </summary>
            Print_transport_order,
            /// <summary>
            /// Operation Print transporters list
            /// </summary>
            Print_transporters_list,
            /// <summary>
            /// Operation View active contracts
            /// </summary>
            View_active_contracts,
            /// <summary>
            /// Operation View allocated hubs
            /// </summary>
            View_allocated_hubs,
            /// <summary>
            /// Operation View bid winner transporters
            /// </summary>
            View_bid_winner_transporters,
            /// <summary>
            /// Operation View contract history
            /// </summary>
            View_contract_history,
            /// <summary>
            /// Operation View Destinations
            /// </summary>
            View_Destinations,
            /// <summary>
            /// Operation View Dispatch Allocation
            /// </summary>
            View_Dispatch_Allocation,
            /// <summary>
            /// Operation View draft hub allocation
            /// </summary>
            View_draft_hub_allocation,
            /// <summary>
            /// Operation View Transport Requisition
            /// </summary>
            View_Transport_Requisition,
            /// <summary>
            /// Operation View Transport Requisition Detail
            /// </summary>
            View_Transport_Requisition_Detail,
            /// <summary>
            /// Operation View transporter list
            /// </summary>
            View_transporter_list,
        }
        #endregion
    
    }
    #endregion
    #region Procurement Security Constants
    public class ProcurementConstants
    {
        #region Methods
        /// <summary>
        /// Retrieve Item name from a Role Enum.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The Role Name.</returns>
        public virtual string ItemName(Role role)
        {
            if ((role == Role.Procurement_Unit_Coordinator))
            {
                return "Procurement Unit Coordinator";
            }
            if ((role == Role.Procurement_Data_Encoder))
            {
                return "Procurement-Data Encoder";
            }
            if ((role == Role.Procurement_Purchaser))
            {
                return "Procurement-Purchaser";
            }
            throw new System.ArgumentException("Unknown Role name", "role");
        }
        /// <summary>
        /// Retrieve Item name from a Task Enum.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The Task Name.</returns>
        public virtual string ItemName(Task task)
        {
            if ((task == Task.Approve_Bid_Proposals))
            {
                return "Approve Bid Proposals";
            }
            if ((task == Task.Bid_admin))
            {
                return "Bid admin";
            }
            if ((task == Task.Bid_plan))
            {
                return "Bid plan";
            }
            if ((task == Task.Contract_Admin))
            {
                return "Contract Admin";
            }
            if ((task == Task.Generate_Winners))
            {
                return "Generate Winners";
            }
            if ((task == Task.Manage_bid))
            {
                return "Manage bid";
            }
            if ((task == Task.Payment_Request))
            {
                return "Payment Request";
            }
            if ((task == Task.Price_Quotation_Data_Entries))
            {
                return "Price Quotation Data Entries";
            }
            if ((task == Task.Request_For_Quotation__RFQ))
            {
                return "Request For Quotation (RFQ)";
            }
            if ((task == Task.Transport_Order))
            {
                return "Transport Order";
            }
            if ((task == Task.Transport_Supplier))
            {
                return "Transport Supplier";
            }
            if ((task == Task.Winners_Dispatch_Location))
            {
                return "Winners Dispatch Location";
            }
            if ((task == Task.Woreda_Bid_Proposal))
            {
                return "Woreda Bid Proposal";
            }
            if ((task == Task.Woreda_bid_Status))
            {
                return "Woreda bid Status";
            }
            throw new System.ArgumentException("Unknown Task name", "task");
        }
        /// <summary>
        /// Retrieve Item name from a Operation Enum.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The Operation Name.</returns>
        public virtual string ItemName(Operation operation)
        {
            if ((operation == Operation.Add_TO))
            {
                return "Add TO";
            }
            if ((operation == Operation.Approve_TO))
            {
                return "Approve TO";
            }
            if ((operation == Operation.Assign_Transporter))
            {
                return "Assign Transporter";
            }
            if ((operation == Operation.Bid_Planning))
            {
                return "Bid Planning";
            }
            if ((operation == Operation.Cash_Check))
            {
                return "Cash Check";
            }
            if ((operation == Operation.Create_new_bid))
            {
                return "Create new bid";
            }
            if ((operation == Operation.Create_new_bid_plan))
            {
                return "Create new bid plan";
            }
            if ((operation == Operation.Create_New_Bid_Proposal))
            {
                return "Create New Bid Proposal";
            }
            if ((operation == Operation.Delete_bid_plan))
            {
                return "Delete bid plan";
            }
            if ((operation == Operation.Delete_Bid_Proposal))
            {
                return "Delete Bid Proposal";
            }
            if ((operation == Operation.Delete_Transport_Supplier))
            {
                return "Delete Transport Supplier";
            }
            if ((operation == Operation.Edit_bid))
            {
                return "Edit bid";
            }
            if ((operation == Operation.Edit_bid_plan))
            {
                return "Edit bid plan";
            }
            if ((operation == Operation.Edit_Bid_Proposal))
            {
                return "Edit Bid Proposal";
            }
            if ((operation == Operation.Edit_bid_proposal_details))
            {
                return "Edit bid proposal details";
            }
            if ((operation == Operation.Edit_TO))
            {
                return "Edit TO";
            }
            if ((operation == Operation.Edit_Transport_Supplier))
            {
                return "Edit Transport Supplier";
            }
            if ((operation == Operation.Export_bid))
            {
                return "Export bid";
            }
            if ((operation == Operation.Export_bid_plan))
            {
                return "Export bid plan";
            }
            if ((operation == Operation.Export_Payment_Request))
            {
                return "Export Payment Request";
            }
            if ((operation == Operation.Generate_Agreement))
            {
                return "Generate Agreement";
            }
            if ((operation == Operation.Generate_Winners_for_a_bid))
            {
                return "Generate Winners for a bid";
            }
            if ((operation == Operation.Issue_Check))
            {
                return "Issue Check";
            }
            if ((operation == Operation.Manage_Bids))
            {
                return "Manage Bids";
            }
            if ((operation == Operation.New_Payment_Request))
            {
                return "New Payment Request";
            }
            if ((operation == Operation.New_Transport_Supplier))
            {
                return "New Transport Supplier";
            }
            if ((operation == Operation.Price_Quotation_Data_Entry))
            {
                return "Price Quotation Data Entry";
            }
            if ((operation == Operation.Print_bid))
            {
                return "Print bid";
            }
            if ((operation == Operation.Print_bid_plan))
            {
                return "Print bid plan";
            }
            if ((operation == Operation.Print_Contract))
            {
                return "Print Contract";
            }
            if ((operation == Operation.Print_Payment_request))
            {
                return "Print Payment request";
            }
            if ((operation == Operation.Print_RFQ))
            {
                return "Print RFQ";
            }
            if ((operation == Operation.Reject_Approval))
            {
                return "Reject Approval";
            }
            if ((operation == Operation.Request_Approval))
            {
                return "Request Approval";
            }
            if ((operation == Operation.Transport_Suppliers))
            {
                return "Transport Suppliers";
            }
            if ((operation == Operation.Transport_warehouse_assignment))
            {
                return "Transport warehouse assignment";
            }
            if ((operation == Operation.View_Active_Agreement))
            {
                return "View Active Agreement";
            }
            if ((operation == Operation.View_approved_bid))
            {
                return "View approved bid";
            }
            if ((operation == Operation.View_Bid_admin))
            {
                return "View Bid admin";
            }
            if ((operation == Operation.View_bid_list))
            {
                return "View bid list";
            }
            if ((operation == Operation.View_bid_plan))
            {
                return "View bid plan";
            }
            if ((operation == Operation.View_Bid_Proposals))
            {
                return "View Bid Proposals";
            }
            if ((operation == Operation.View_current_bid))
            {
                return "View current bid";
            }
            if ((operation == Operation.View_Dispath_Locations))
            {
                return "View Dispath Locations";
            }
            if ((operation == Operation.View_History))
            {
                return "View History";
            }
            if ((operation == Operation.View_Payment_History))
            {
                return "View Payment History";
            }
            if ((operation == Operation.View_Payment_Request))
            {
                return "View Payment Request";
            }
            if ((operation == Operation.View_Price_Quotation_Data_Entries))
            {
                return "View Price Quotation Data Entries";
            }
            if ((operation == Operation.View_Request_For_Quotation))
            {
                return "View Request For Quotation";
            }
            if ((operation == Operation.View_Transport_Order))
            {
                return "View Transport Order";
            }
            if ((operation == Operation.View_Transport_Suppliers))
            {
                return "View Transport Suppliers";
            }
            if ((operation == Operation.View_Winners_for_Contract))
            {
                return "View Winners for Contract";
            }
            if ((operation == Operation.Winners_Dispatch_Locations))
            {
                return "Winners Dispatch Locations";
            }
            throw new System.ArgumentException("Unknown Operation name", "operation");
        }
        
        #endregion  

        #region Enums
        /// <summary>
        /// Roles Enumeration
        /// </summary>
        public enum Role
        {
            /// <summary>
            /// Role Procurement Unit Coordinator
            /// </summary>
            Procurement_Unit_Coordinator,
            /// <summary>
            /// Role Procurement-Data Encoder
            /// </summary>
            Procurement_Data_Encoder,
            /// <summary>
            /// Role Procurement-Purchaser
            /// </summary>
            Procurement_Purchaser,
        }
        /// <summary>
        /// Tasks Enumeration
        /// </summary>
        public enum Task
        {
            /// <summary>
            /// Task Approve Bid Proposals
            /// </summary>
            Approve_Bid_Proposals,
            /// <summary>
            /// Task Bid admin
            /// </summary>
            Bid_admin,
            /// <summary>
            /// Task Bid plan
            /// </summary>
            Bid_plan,
            /// <summary>
            /// Task Contract Admin
            /// </summary>
            Contract_Admin,
            /// <summary>
            /// Task Generate Winners
            /// </summary>
            Generate_Winners,
            /// <summary>
            /// Task Manage bid
            /// </summary>
            Manage_bid,
            /// <summary>
            /// Task Payment Request
            /// </summary>
            Payment_Request,
            /// <summary>
            /// Task Price Quotation Data Entries
            /// </summary>
            Price_Quotation_Data_Entries,
            /// <summary>
            /// Task Request For Quotation (RFQ)
            /// </summary>
            Request_For_Quotation__RFQ,
            /// <summary>
            /// Task Transport Order
            /// </summary>
            Transport_Order,
            /// <summary>
            /// Task Transport Supplier
            /// </summary>
            Transport_Supplier,
            /// <summary>
            /// Task Winners Dispatch Location
            /// </summary>
            Winners_Dispatch_Location,
            /// <summary>
            /// Task Woreda Bid Proposal
            /// </summary>
            Woreda_Bid_Proposal,
            /// <summary>
            /// Task Woreda bid Status
            /// </summary>
            Woreda_bid_Status,
        }
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Add TO
            /// </summary>
            Add_TO,
            /// <summary>
            /// Operation Approve TO
            /// </summary>
            Approve_TO,
            /// <summary>
            /// Operation Assign Transporter
            /// </summary>
            Assign_Transporter,
            /// <summary>
            /// Operation Bid Planning
            /// </summary>
            Bid_Planning,
            /// <summary>
            /// Operation Cash Check
            /// </summary>
            Cash_Check,
            /// <summary>
            /// Operation Create new bid
            /// </summary>
            Create_new_bid,
            /// <summary>
            /// Operation Create new bid plan
            /// </summary>
            Create_new_bid_plan,
            /// <summary>
            /// Operation Create New Bid Proposal
            /// </summary>
            Create_New_Bid_Proposal,
            /// <summary>
            /// Operation Delete bid plan
            /// </summary>
            Delete_bid_plan,
            /// <summary>
            /// Operation Delete Bid Proposal
            /// </summary>
            Delete_Bid_Proposal,
            /// <summary>
            /// Operation Delete Transport Supplier
            /// </summary>
            Delete_Transport_Supplier,
            /// <summary>
            /// Operation Edit bid
            /// </summary>
            Edit_bid,
            /// <summary>
            /// Operation Edit bid plan
            /// </summary>
            Edit_bid_plan,
            /// <summary>
            /// Operation Edit Bid Proposal
            /// </summary>
            Edit_Bid_Proposal,
            /// <summary>
            /// Operation Edit bid proposal details
            /// </summary>
            Edit_bid_proposal_details,
            /// <summary>
            /// Operation Edit TO
            /// </summary>
            Edit_TO,
            /// <summary>
            /// Operation Edit Transport Supplier
            /// </summary>
            Edit_Transport_Supplier,
            /// <summary>
            /// Operation Export bid
            /// </summary>
            Export_bid,
            /// <summary>
            /// Operation Export bid plan
            /// </summary>
            Export_bid_plan,
            /// <summary>
            /// Operation Export Payment Request
            /// </summary>
            Export_Payment_Request,
            /// <summary>
            /// Operation Generate Agreement
            /// </summary>
            Generate_Agreement,
            /// <summary>
            /// Operation Generate Winners for a bid
            /// </summary>
            Generate_Winners_for_a_bid,
            /// <summary>
            /// Operation Issue Check
            /// </summary>
            Issue_Check,
            /// <summary>
            /// Operation Manage Bids
            /// </summary>
            Manage_Bids,
            /// <summary>
            /// Operation New Payment Request
            /// </summary>
            New_Payment_Request,
            /// <summary>
            /// Operation New Transport Supplier
            /// </summary>
            New_Transport_Supplier,
            /// <summary>
            /// Operation Price Quotation Data Entry
            /// </summary>
            Price_Quotation_Data_Entry,
            /// <summary>
            /// Operation Print bid
            /// </summary>
            Print_bid,
            /// <summary>
            /// Operation Print bid plan
            /// </summary>
            Print_bid_plan,
            /// <summary>
            /// Operation Print Contract
            /// </summary>
            Print_Contract,
            /// <summary>
            /// Operation Print Payment request
            /// </summary>
            Print_Payment_request,
            /// <summary>
            /// Operation Print RFQ
            /// </summary>
            Print_RFQ,
            /// <summary>
            /// Operation Reject Approval
            /// </summary>
            Reject_Approval,
            /// <summary>
            /// Operation Request Approval
            /// </summary>
            Request_Approval,
            /// <summary>
            /// Operation Transport Suppliers
            /// </summary>
            Transport_Suppliers,
            /// <summary>
            /// Operation Transport warehouse assignment
            /// </summary>
            Transport_warehouse_assignment,
            /// <summary>
            /// Operation View Active Agreement
            /// </summary>
            View_Active_Agreement,
            /// <summary>
            /// Operation View approved bid
            /// </summary>
            View_approved_bid,
            /// <summary>
            /// Operation View Bid admin
            /// </summary>
            View_Bid_admin,
            /// <summary>
            /// Operation View bid list
            /// </summary>
            View_bid_list,
            /// <summary>
            /// Operation View bid plan
            /// </summary>
            View_bid_plan,
            /// <summary>
            /// Operation View Bid Proposals
            /// </summary>
            View_Bid_Proposals,
            /// <summary>
            /// Operation View current bid
            /// </summary>
            View_current_bid,
            /// <summary>
            /// Operation View Dispath Locations
            /// </summary>
            View_Dispath_Locations,
            /// <summary>
            /// Operation View History
            /// </summary>
            View_History,
            /// <summary>
            /// Operation View Payment History
            /// </summary>
            View_Payment_History,
            /// <summary>
            /// Operation View Payment Request
            /// </summary>
            View_Payment_Request,
            /// <summary>
            /// Operation View Price Quotation Data Entries
            /// </summary>
            View_Price_Quotation_Data_Entries,
            /// <summary>
            /// Operation View Request For Quotation
            /// </summary>
            View_Request_For_Quotation,
            /// <summary>
            /// Operation View Transport Order
            /// </summary>
            View_Transport_Order,
            /// <summary>
            /// Operation View Transport Suppliers
            /// </summary>
            View_Transport_Suppliers,
            /// <summary>
            /// Operation View Winners for Contract
            /// </summary>
            View_Winners_for_Contract,
            /// <summary>
            /// Operation Winners Dispatch Locations
            /// </summary>
            Winners_Dispatch_Locations,
        }
        #endregion
    
    }
    #endregion
    #region Hub Security Constants
    public class HubConstants
    {
        #region Methods
        /// <summary>
        /// Retrieve Item name from a Role Enum.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The Role Name.</returns>
        public virtual string ItemName(Role role)
        {
            if ((role == Role.Hub_Manager))
            {
                return "Hub Manager";
            }
            if ((role == Role.Hub_Data_entry_Operator))
            {
                return "Hub-Data-entry Operator";
            }
            if ((role == Role.Hub_Store_keeper))
            {
                return "Hub-Store keeper";
            }
            throw new System.ArgumentException("Unknown Role name", "role");
        }
        /// <summary>
        /// Retrieve Item name from a Task Enum.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The Task Name.</returns>
        public virtual string ItemName(Task task)
        {
            if ((task == Task.Approve_Dispatch))
            {
                return "Approve Dispatch";
            }
            if ((task == Task.Approve_Receipt))
            {
                return "Approve Receipt";
            }
            if ((task == Task.Approve_Transport_Order))
            {
                return "Approve Transport Order";
            }
            if ((task == Task.Bin_Card_Report))
            {
                return "Bin Card Report";
            }
            if ((task == Task.Dispatch_Stock_Status_Report))
            {
                return "Dispatch Stock Status Report";
            }
            if ((task == Task.Free_Stock_Report))
            {
                return "Free Stock Report";
            }
            if ((task == Task.Internal_Movements))
            {
                return "Internal Movements";
            }
            if ((task == Task.Losses_and_Adjustments))
            {
                return "Losses and Adjustments";
            }
            if ((task == Task.Manage_Dispatch))
            {
                return "Manage Dispatch";
            }
            if ((task == Task.Manage_Receipt))
            {
                return "Manage Receipt";
            }
            if ((task == Task.Receipt_Status))
            {
                return "Receipt Status";
            }
            if ((task == Task.Stack_Events))
            {
                return "Stack Events";
            }
            if ((task == Task.Starting_Balance))
            {
                return "Starting Balance";
            }
            if ((task == Task.Store_Report))
            {
                return "Store Report";
            }
            if ((task == Task.Transport_Order_List))
            {
                return "Transport Order List";
            }
            if ((task == Task.Transportation_Report))
            {
                return "Transportation Report";
            }
            throw new System.ArgumentException("Unknown Task name", "task");
        }
        /// <summary>
        /// Retrieve Item name from a Operation Enum.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The Operation Name.</returns>
        public virtual string ItemName(Operation operation)
        {
            if ((operation == Operation.Add_new_event))
            {
                return "Add new event";
            }
            if ((operation == Operation.Close_dispatch_plan_to_FDPs))
            {
                return "Close dispatch plan to FDPs";
            }
            if ((operation == Operation.Close_dispatch_plan_to_loans))
            {
                return "Close dispatch plan to loans";
            }
            if ((operation == Operation.Close_dispatch_plan_to_transfers))
            {
                return "Close dispatch plan to transfers";
            }
            if ((operation == Operation.Close_receipt_plan_from_donation))
            {
                return "Close receipt plan from donation";
            }
            if ((operation == Operation.Close_receipt_plan_from_loan))
            {
                return "Close receipt plan from loan";
            }
            if ((operation == Operation.Close_receipt_plan_from_repayment))
            {
                return "Close receipt plan from repayment";
            }
            if ((operation == Operation.Close_receipt_plan_from_swap))
            {
                return "Close receipt plan from swap";
            }
            if ((operation == Operation.Close_receipt_plan_from_transfer))
            {
                return "Close receipt plan from transfer";
            }
            if ((operation == Operation.Create_new_starting_balance))
            {
                return "Create new starting balance";
            }
            if ((operation == Operation.Edit_dispatch_to_FDPs))
            {
                return "Edit dispatch to FDPs";
            }
            if ((operation == Operation.Edit_dispatch_to_loans))
            {
                return "Edit dispatch to loans";
            }
            if ((operation == Operation.Edit_dispatch_to_transfers))
            {
                return "Edit dispatch to transfers";
            }
            if ((operation == Operation.Edit_receipt_from_donation))
            {
                return "Edit receipt from donation";
            }
            if ((operation == Operation.Edit_receipt_from_loan))
            {
                return "Edit receipt from loan";
            }
            if ((operation == Operation.Edit_receipt_from_repayment))
            {
                return "Edit receipt from repayment";
            }
            if ((operation == Operation.Edit_receipt_from_swap))
            {
                return "Edit receipt from swap";
            }
            if ((operation == Operation.Edit_receipt_from_transfer))
            {
                return "Edit receipt from transfer";
            }
            if ((operation == Operation.Manage_Tranport_Order))
            {
                return "Manage Tranport Order";
            }
            if ((operation == Operation.New_dispatch_plan_to_FDPs))
            {
                return "New dispatch plan to FDPs";
            }
            if ((operation == Operation.New_dispatch_plan_to_loans))
            {
                return "New dispatch plan to loans";
            }
            if ((operation == Operation.New_dispatch_plan_to_transfers))
            {
                return "New dispatch plan to transfers";
            }
            if ((operation == Operation.New_dispatch_to_FDPs))
            {
                return "New dispatch to FDPs";
            }
            if ((operation == Operation.New_dispatch_to_loans))
            {
                return "New dispatch to loans";
            }
            if ((operation == Operation.New_dispatch_to_transfers))
            {
                return "New dispatch to transfers";
            }
            if ((operation == Operation.New_internal_movement))
            {
                return "New internal movement";
            }
            if ((operation == Operation.New_receipt__from_transfer))
            {
                return "New receipt  from transfer";
            }
            if ((operation == Operation.New_receipt_from_donation))
            {
                return "New receipt from donation";
            }
            if ((operation == Operation.New_receipt_from_loan))
            {
                return "New receipt from loan";
            }
            if ((operation == Operation.New_receipt_from_repayment))
            {
                return "New receipt from repayment";
            }
            if ((operation == Operation.New_receipt_from_swap))
            {
                return "New receipt from swap";
            }
            if ((operation == Operation.New_receipt_plan_from_donation))
            {
                return "New receipt plan from donation";
            }
            if ((operation == Operation.New_receipt_plan_from_loan))
            {
                return "New receipt plan from loan";
            }
            if ((operation == Operation.New_receipt_plan_from_repayment))
            {
                return "New receipt plan from repayment";
            }
            if ((operation == Operation.New_receipt_plan_from_swap))
            {
                return "New receipt plan from swap";
            }
            if ((operation == Operation.New_receipt_plan_from_transfer))
            {
                return "New receipt plan from transfer";
            }
            if ((operation == Operation.New_starting_balance))
            {
                return "New starting balance";
            }
            if ((operation == Operation.Print_dispatch_record__GIN))
            {
                return "Print dispatch record (GIN)";
            }
            if ((operation == Operation.Print_receipt_record__GRN))
            {
                return "Print receipt record (GRN)";
            }
            if ((operation == Operation.Record_new_adjustment))
            {
                return "Record new adjustment";
            }
            if ((operation == Operation.Record_new_loss))
            {
                return "Record new loss";
            }
            if ((operation == Operation.View_adjustiments))
            {
                return "View adjustiments";
            }
            if ((operation == Operation.View_bin_card_report))
            {
                return "View bin card report";
            }
            if ((operation == Operation.View_dispatch_stock_status_report))
            {
                return "View dispatch stock status report";
            }
            if ((operation == Operation.View_dispatch_to_FDPs))
            {
                return "View dispatch to FDPs";
            }
            if ((operation == Operation.View_dispatch_to_loans))
            {
                return "View dispatch to loans";
            }
            if ((operation == Operation.View_dispatch_to_transfers))
            {
                return "View dispatch to transfers";
            }
            if ((operation == Operation.View_free_stock_report))
            {
                return "View free stock report";
            }
            if ((operation == Operation.View_internal_movement))
            {
                return "View internal movement";
            }
            if ((operation == Operation.View_losses))
            {
                return "View losses";
            }
            if ((operation == Operation.View_receipt_from_donation))
            {
                return "View receipt from donation";
            }
            if ((operation == Operation.View_receipt_from_loan))
            {
                return "View receipt from loan";
            }
            if ((operation == Operation.View_receipt_from_repayment))
            {
                return "View receipt from repayment";
            }
            if ((operation == Operation.View_receipt_from_swap))
            {
                return "View receipt from swap";
            }
            if ((operation == Operation.View_receipt_from_transfer))
            {
                return "View receipt from transfer";
            }
            if ((operation == Operation.View_receipt_status_report))
            {
                return "View receipt status report";
            }
            if ((operation == Operation.View_stack_events))
            {
                return "View stack events";
            }
            if ((operation == Operation.View_starting_balance))
            {
                return "View starting balance";
            }
            if ((operation == Operation.View_transport_order_list))
            {
                return "View transport order list";
            }
            if ((operation == Operation.View_transportation_report))
            {
                return "View transportation report";
            }
            if ((operation == Operation.Viiew_store_report))
            {
                return "Viiew store report";
            }
            throw new System.ArgumentException("Unknown Operation name", "operation");
        }
        #endregion
        
        #region Enums
        /// <summary>
        /// Roles Enumeration
        /// </summary>
        public enum Role
        {
            /// <summary>
            /// Role Hub Manager
            /// </summary>
            Hub_Manager,
            /// <summary>
            /// Role Hub-Data-entry Operator
            /// </summary>
            Hub_Data_entry_Operator,
            /// <summary>
            /// Role Hub-Store keeper
            /// </summary>
            Hub_Store_keeper,
        }
        /// <summary>
        /// Tasks Enumeration
        /// </summary>
        public enum Task
        {
            /// <summary>
            /// Task Approve Dispatch
            /// </summary>
            Approve_Dispatch,
            /// <summary>
            /// Task Approve Receipt
            /// </summary>
            Approve_Receipt,
            /// <summary>
            /// Task Approve Transport Order
            /// </summary>
            Approve_Transport_Order,
            /// <summary>
            /// Task Bin Card Report
            /// </summary>
            Bin_Card_Report,
            /// <summary>
            /// Task Dispatch Stock Status Report
            /// </summary>
            Dispatch_Stock_Status_Report,
            /// <summary>
            /// Task Free Stock Report
            /// </summary>
            Free_Stock_Report,
            /// <summary>
            /// Task Internal Movements
            /// </summary>
            Internal_Movements,
            /// <summary>
            /// Task Losses and Adjustments
            /// </summary>
            Losses_and_Adjustments,
            /// <summary>
            /// Task Manage Dispatch
            /// </summary>
            Manage_Dispatch,
            /// <summary>
            /// Task Manage Receipt
            /// </summary>
            Manage_Receipt,
            /// <summary>
            /// Task Receipt Status
            /// </summary>
            Receipt_Status,
            /// <summary>
            /// Task Stack Events
            /// </summary>
            Stack_Events,
            /// <summary>
            /// Task Starting Balance
            /// </summary>
            Starting_Balance,
            /// <summary>
            /// Task Store Report
            /// </summary>
            Store_Report,
            /// <summary>
            /// Task Transport Order List
            /// </summary>
            Transport_Order_List,
            /// <summary>
            /// Task Transportation Report
            /// </summary>
            Transportation_Report,
        }
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Add new event
            /// </summary>
            Add_new_event,
            /// <summary>
            /// Operation Close dispatch plan to FDPs
            /// </summary>
            Close_dispatch_plan_to_FDPs,
            /// <summary>
            /// Operation Close dispatch plan to loans
            /// </summary>
            Close_dispatch_plan_to_loans,
            /// <summary>
            /// Operation Close dispatch plan to transfers
            /// </summary>
            Close_dispatch_plan_to_transfers,
            /// <summary>
            /// Operation Close receipt plan from donation
            /// </summary>
            Close_receipt_plan_from_donation,
            /// <summary>
            /// Operation Close receipt plan from loan
            /// </summary>
            Close_receipt_plan_from_loan,
            /// <summary>
            /// Operation Close receipt plan from repayment
            /// </summary>
            Close_receipt_plan_from_repayment,
            /// <summary>
            /// Operation Close receipt plan from swap
            /// </summary>
            Close_receipt_plan_from_swap,
            /// <summary>
            /// Operation Close receipt plan from transfer
            /// </summary>
            Close_receipt_plan_from_transfer,
            /// <summary>
            /// Operation Create new starting balance
            /// </summary>
            Create_new_starting_balance,
            /// <summary>
            /// Operation Edit dispatch to FDPs
            /// </summary>
            Edit_dispatch_to_FDPs,
            /// <summary>
            /// Operation Edit dispatch to loans
            /// </summary>
            Edit_dispatch_to_loans,
            /// <summary>
            /// Operation Edit dispatch to transfers
            /// </summary>
            Edit_dispatch_to_transfers,
            /// <summary>
            /// Operation Edit receipt from donation
            /// </summary>
            Edit_receipt_from_donation,
            /// <summary>
            /// Operation Edit receipt from loan
            /// </summary>
            Edit_receipt_from_loan,
            /// <summary>
            /// Operation Edit receipt from repayment
            /// </summary>
            Edit_receipt_from_repayment,
            /// <summary>
            /// Operation Edit receipt from swap
            /// </summary>
            Edit_receipt_from_swap,
            /// <summary>
            /// Operation Edit receipt from transfer
            /// </summary>
            Edit_receipt_from_transfer,
            /// <summary>
            /// Operation Manage Tranport Order
            /// </summary>
            Manage_Tranport_Order,
            /// <summary>
            /// Operation New dispatch plan to FDPs
            /// </summary>
            New_dispatch_plan_to_FDPs,
            /// <summary>
            /// Operation New dispatch plan to loans
            /// </summary>
            New_dispatch_plan_to_loans,
            /// <summary>
            /// Operation New dispatch plan to transfers
            /// </summary>
            New_dispatch_plan_to_transfers,
            /// <summary>
            /// Operation New dispatch to FDPs
            /// </summary>
            New_dispatch_to_FDPs,
            /// <summary>
            /// Operation New dispatch to loans
            /// </summary>
            New_dispatch_to_loans,
            /// <summary>
            /// Operation New dispatch to transfers
            /// </summary>
            New_dispatch_to_transfers,
            /// <summary>
            /// Operation New internal movement
            /// </summary>
            New_internal_movement,
            /// <summary>
            /// Operation New receipt  from transfer
            /// </summary>
            New_receipt__from_transfer,
            /// <summary>
            /// Operation New receipt from donation
            /// </summary>
            New_receipt_from_donation,
            /// <summary>
            /// Operation New receipt from loan
            /// </summary>
            New_receipt_from_loan,
            /// <summary>
            /// Operation New receipt from repayment
            /// </summary>
            New_receipt_from_repayment,
            /// <summary>
            /// Operation New receipt from swap
            /// </summary>
            New_receipt_from_swap,
            /// <summary>
            /// Operation New receipt plan from donation
            /// </summary>
            New_receipt_plan_from_donation,
            /// <summary>
            /// Operation New receipt plan from loan
            /// </summary>
            New_receipt_plan_from_loan,
            /// <summary>
            /// Operation New receipt plan from repayment
            /// </summary>
            New_receipt_plan_from_repayment,
            /// <summary>
            /// Operation New receipt plan from swap
            /// </summary>
            New_receipt_plan_from_swap,
            /// <summary>
            /// Operation New receipt plan from transfer
            /// </summary>
            New_receipt_plan_from_transfer,
            /// <summary>
            /// Operation New starting balance
            /// </summary>
            New_starting_balance,
            /// <summary>
            /// Operation Print dispatch record (GIN)
            /// </summary>
            Print_dispatch_record__GIN,
            /// <summary>
            /// Operation Print receipt record (GRN)
            /// </summary>
            Print_receipt_record__GRN,
            /// <summary>
            /// Operation Record new adjustment
            /// </summary>
            Record_new_adjustment,
            /// <summary>
            /// Operation Record new loss
            /// </summary>
            Record_new_loss,
            /// <summary>
            /// Operation View adjustiments
            /// </summary>
            View_adjustiments,
            /// <summary>
            /// Operation View bin card report
            /// </summary>
            View_bin_card_report,
            /// <summary>
            /// Operation View dispatch stock status report
            /// </summary>
            View_dispatch_stock_status_report,
            /// <summary>
            /// Operation View dispatch to FDPs
            /// </summary>
            View_dispatch_to_FDPs,
            /// <summary>
            /// Operation View dispatch to loans
            /// </summary>
            View_dispatch_to_loans,
            /// <summary>
            /// Operation View dispatch to transfers
            /// </summary>
            View_dispatch_to_transfers,
            /// <summary>
            /// Operation View free stock report
            /// </summary>
            View_free_stock_report,
            /// <summary>
            /// Operation View internal movement
            /// </summary>
            View_internal_movement,
            /// <summary>
            /// Operation View losses
            /// </summary>
            View_losses,
            /// <summary>
            /// Operation View receipt from donation
            /// </summary>
            View_receipt_from_donation,
            /// <summary>
            /// Operation View receipt from loan
            /// </summary>
            View_receipt_from_loan,
            /// <summary>
            /// Operation View receipt from repayment
            /// </summary>
            View_receipt_from_repayment,
            /// <summary>
            /// Operation View receipt from swap
            /// </summary>
            View_receipt_from_swap,
            /// <summary>
            /// Operation View receipt from transfer
            /// </summary>
            View_receipt_from_transfer,
            /// <summary>
            /// Operation View receipt status report
            /// </summary>
            View_receipt_status_report,
            /// <summary>
            /// Operation View stack events
            /// </summary>
            View_stack_events,
            /// <summary>
            /// Operation View starting balance
            /// </summary>
            View_starting_balance,
            /// <summary>
            /// Operation View transport order list
            /// </summary>
            View_transport_order_list,
            /// <summary>
            /// Operation View transportation report
            /// </summary>
            View_transportation_report,
            /// <summary>
            /// Operation Viiew store report
            /// </summary>
            Viiew_store_report,
        }
        #endregion
    
    }
    #endregion
    #region Region Security Constants
    public class RegionalConstants
    {
        #region Methods
        /// <summary>
        /// Retrieve Item name from a Role Enum.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The Role Name.</returns>
        public virtual string ItemName(Role role)
        {
            if ((role == Role.Afar_Regional_DPPBs))
            {
                return "Afar-Regional DPPBs";
            }
            if ((role == Role.Amhara_Regional_DPPBs))
            {
                return "Amhara-Regional DPPBs";
            }
            if ((role == Role.Benshangul_Gumuz_Regional_DPPBs))
            {
                return "Benshangul Gumuz-Regional DPPBs";
            }
            if ((role == Role.Dire_Dawa_Regional_DPPBs))
            {
                return "Dire Dawa-Regional DPPBs";
            }
            if ((role == Role.Gambella_Regional_DPPBs))
            {
                return "Gambella-Regional DPPBs";
            }
            if ((role == Role.Harar_Regional_DPPBs))
            {
                return "Harar-Regional DPPBs";
            }
            if ((role == Role.Oromia_Regional_DPPBs))
            {
                return "Oromia-Regional DPPBs";
            }
            if ((role == Role.SNNPR_Regional_DPPBs))
            {
                return "SNNPR-Regional DPPBs";
            }
            if ((role == Role.Somali_Regional_DPPBs))
            {
                return "Somali-Regional DPPBs";
            }
            if ((role == Role.Tigray_Regional_DPPBs))
            {
                return "Tigray-Regional DPPBs";
            }
            throw new System.ArgumentException("Unknown Role name", "role");
        }
        /// <summary>
        /// Retrieve Item name from a Task Enum.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The Task Name.</returns>
        public virtual string ItemName(Task task)
        {
            if ((task == Task.Approve_Needs_Assessment))
            {
                return "Approve Needs Assessment";
            }
            if ((task == Task.Approve_Requests))
            {
                return "Approve Requests";
            }
            if ((task == Task.Manage_monthly_regional_requests))
            {
                return "Manage monthly regional requests";
            }
            if ((task == Task.Manage_Needs_Assessment))
            {
                return "Manage Needs Assessment";
            }
            throw new System.ArgumentException("Unknown Task name", "task");
        }
        /// <summary>
        /// Retrieve Item name from a Operation Enum.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The Operation Name.</returns>
        public virtual string ItemName(Operation operation)
        {
            if ((operation == Operation.Add_new_needs_assessment))
            {
                return "Add new needs assessment";
            }
            if ((operation == Operation.Add_new_regional_request))
            {
                return "Add new regional request";
            }
            if ((operation == Operation.Allocate_requests))
            {
                return "Allocate requests";
            }
            if ((operation == Operation.Delete_needs_assessment))
            {
                return "Delete needs assessment";
            }
            if ((operation == Operation.Edit_needs_assessment))
            {
                return "Edit needs assessment";
            }
            if ((operation == Operation.Edit_regional_request))
            {
                return "Edit regional request";
            }
            if ((operation == Operation.Export_needs_assessment))
            {
                return "Export needs assessment";
            }
            if ((operation == Operation.Export_requests))
            {
                return "Export requests";
            }
            if ((operation == Operation.Print_needs_assessment))
            {
                return "Print needs assessment";
            }
            if ((operation == Operation.Print_requests))
            {
                return "Print requests";
            }
            if ((operation == Operation.View_needs_assessment))
            {
                return "View needs assessment";
            }
            if ((operation == Operation.View_requests))
            {
                return "View requests";
            }
            throw new System.ArgumentException("Unknown Operation name", "operation");
        }
        #endregion
       
        #region Enums
        /// <summary>
        /// Roles Enumeration
        /// </summary>
        public enum Role
        {
            /// <summary>
            /// Role Afar-Regional DPPBs
            /// </summary>
            Afar_Regional_DPPBs,
            /// <summary>
            /// Role Amhara-Regional DPPBs
            /// </summary>
            Amhara_Regional_DPPBs,
            /// <summary>
            /// Role Benshangul Gumuz-Regional DPPBs
            /// </summary>
            Benshangul_Gumuz_Regional_DPPBs,
            /// <summary>
            /// Role Dire Dawa-Regional DPPBs
            /// </summary>
            Dire_Dawa_Regional_DPPBs,
            /// <summary>
            /// Role Gambella-Regional DPPBs
            /// </summary>
            Gambella_Regional_DPPBs,
            /// <summary>
            /// Role Harar-Regional DPPBs
            /// </summary>
            Harar_Regional_DPPBs,
            /// <summary>
            /// Role Oromia-Regional DPPBs
            /// </summary>
            Oromia_Regional_DPPBs,
            /// <summary>
            /// Role SNNPR-Regional DPPBs
            /// </summary>
            SNNPR_Regional_DPPBs,
            /// <summary>
            /// Role Somali-Regional DPPBs
            /// </summary>
            Somali_Regional_DPPBs,
            /// <summary>
            /// Role Tigray-Regional DPPBs
            /// </summary>
            Tigray_Regional_DPPBs,
        }
        /// <summary>
        /// Tasks Enumeration
        /// </summary>
        public enum Task
        {
            /// <summary>
            /// Task Approve Needs Assessment
            /// </summary>
            Approve_Needs_Assessment,
            /// <summary>
            /// Task Approve Requests
            /// </summary>
            Approve_Requests,
            /// <summary>
            /// Task Manage monthly regional requests
            /// </summary>
            Manage_monthly_regional_requests,
            /// <summary>
            /// Task Manage Needs Assessment
            /// </summary>
            Manage_Needs_Assessment,
        }
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Add new needs assessment
            /// </summary>
            Add_new_needs_assessment,
            /// <summary>
            /// Operation Add new regional request
            /// </summary>
            Add_new_regional_request,
            /// <summary>
            /// Operation Allocate requests
            /// </summary>
            Allocate_requests,
            /// <summary>
            /// Operation Delete needs assessment
            /// </summary>
            Delete_needs_assessment,
            /// <summary>
            /// Operation Edit needs assessment
            /// </summary>
            Edit_needs_assessment,
            /// <summary>
            /// Operation Edit regional request
            /// </summary>
            Edit_regional_request,
            /// <summary>
            /// Operation Export needs assessment
            /// </summary>
            Export_needs_assessment,
            /// <summary>
            /// Operation Export requests
            /// </summary>
            Export_requests,
            /// <summary>
            /// Operation Print needs assessment
            /// </summary>
            Print_needs_assessment,
            /// <summary>
            /// Operation Print requests
            /// </summary>
            Print_requests,
            /// <summary>
            /// Operation View needs assessment
            /// </summary>
            View_needs_assessment,
            /// <summary>
            /// Operation View requests
            /// </summary>
            View_requests,
        }
        #endregion
    
    }
    #endregion
}