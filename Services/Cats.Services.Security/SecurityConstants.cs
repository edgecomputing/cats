
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
        }
        #endregion
    
    }
    #endregion


}

