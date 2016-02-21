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
            if ((role == Role.CaseTeam_Coordinator))
            {
                return "CaseTeam-Coordinator";
            }
            if ((role == Role.EW_CustomsOfficer))
            {
                return "EW-CustomsOfficer";
            }
            if ((role == Role.EW_Experts))
            {
                return "EW-Experts";
            }
            if ((role == Role.EW_Logistics_Planner))
            {
                return "EW-Logistics Planner";
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
            if ((task == Task.Manage_Approval))
            {
                return "Manage Approval";
            }
            if ((task == Task.Manage_Gift_Cereficate))
            {
                return "Manage Gift Cereficate";
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
            if ((operation == Operation.Add_New_IDPS_request))
            {
                return "Add New IDPS request";
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
            if ((operation == Operation.New_requisition))
            {
                return "New requisition";
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
            if ((operation == Operation.Reject_Request))
            {
                return "Reject_Request";
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
            if ((operation == Operation.Reverse_Request))
            {
                return "Reverse_Request";
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
            if ((operation == Operation.View_Bid_Winners))
            {
                return "View Bid Winners";
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
            if ((operation == Operation.View_Plans))
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
            /// Role CaseTeam-Coordinator
            /// </summary>
            CaseTeam_Coordinator,
            /// <summary>
            /// Role EW-CustomsOfficer
            /// </summary>
            EW_CustomsOfficer,
            /// <summary>
            /// Role EW-Experts
            /// </summary>
            EW_Experts,
            /// <summary>
            /// Role EW-Logistics Planner
            /// </summary>
            EW_Logistics_Planner,
        }
        /// <summary>
        /// Tasks Enumeration
        /// </summary>
        public enum Task
        {
            /// <summary>
            /// Task Manage Approval
            /// </summary>
            Manage_Approval,
            /// <summary>
            /// Task Manage Gift Cereficate
            /// </summary>
            Manage_Gift_Cereficate,
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
            Add_New_IDPS_request,
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
            /// Operation Reject_Request
            /// </summary>
            Reject_Request,

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
            ///             /// Operation New requisition
            /// </summary>
            New_requisition,
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
            /// Operation Reverse_Request
            /// </summary>
            Reverse_Request,
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
            View_Bid_Winners,
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
            View_Plans,
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
        /// <summary>
        /// Retrieve Item name from a Operation Enum.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The Operation Name.</returns>
        public virtual string ItemName(Operation operation)
        {
            if ((operation == Operation.Add_commodity))
            {
                return "Add commodity";
            }
            if ((operation == Operation.Add_new_plan))
            {
                return "Add new plan";
            }
            if ((operation == Operation.Add_new_Ration))
            {
                return "Add new Ration";
            }
            if ((operation == Operation.Add_new_resource_allocation))
            {
                return "Add new resource allocation";
            }
            if ((operation == Operation.Approve_Monthly_relife_requisitions))
            {
                return "Approve Monthly relife requisitions";
            }
            if ((operation == Operation.Approve_Request))
            {
                return "Approve Request";
            }
            if ((operation == Operation.Delete__allocated_resource))
            {
                return "Delete  allocated resource";
            }
            if ((operation == Operation.Delete_PSNP_plan))
            {
                return "Delete PSNP plan";
            }
            if ((operation == Operation.Edit__plan))
            {
                return "Edit  plan";
            }
            if ((operation == Operation.Edit_PSNP_plan))
            {
                return "Edit PSNP plan";
            }
            if ((operation == Operation.Edit_ration))
            {
                return "Edit ration";
            }
            if ((operation == Operation.New_requisition))
            {
                return "New requisition";
            }
            if ((operation == Operation.PSNP_plan_Ask_approval))
            {
                return "PSNP plan Ask approval";
            }
            if ((operation == Operation.PSNP_plan_history))
            {
                return "PSNP plan history";
            }
            if ((operation == Operation.Reject_Request))
            {
                return "Reject_Request";
            }

            if ((operation == Operation.Remove_commodity))
            {
                return "Remove commodity";
            }
            if ((operation == Operation.Reverse_Request))
            {
                return "Reverse_Request";
            }
            if ((operation == Operation.Select_default_ration))
            {
                return "Select default ration";
            }
            if ((operation == Operation.View__plan_menu_item))
            {
                return "View  plan menu item";
            }
            if ((operation == Operation.View_approved_requisitions))
            {
                return "View approved requisitions";
            }
            if ((operation == Operation.View_assessment_created_plan))
            {
                return "View assessment created plan";
            }
            if ((operation == Operation.View_closed_plan))
            {
                return "View closed plan";
            }
            if ((operation == Operation.View_Draft_plan))
            {
                return "View Draft plan";
            }
            if ((operation == Operation.View_draft_requisitions))
            {
                return "View draft requisitions";
            }
            if ((operation == Operation.View_HRD_created_Plan))
            {
                return "View HRD created Plan";
            }
            if ((operation == Operation.View_hub_assigned_requisitions))
            {
                return "View hub assigned requisitions";
            }
            if ((operation == Operation.View_New_Request_menu_item))
            {
                return "View New Request menu item";
            }
            if ((operation == Operation.View_PSNP_created_plan))
            {
                return "View PSNP created plan";
            }
            if ((operation == Operation.View_PSNP_plan_menu_item))
            {
                return "View PSNP plan menu item";
            }
            if ((operation == Operation.View_ration_menu_item))
            {
                return "View ration menu item";
            }
            if ((operation == Operation.View_reginal_PSNP_plan))
            {
                return "View reginal PSNP plan";
            }
            if ((operation == Operation.View_request_allocation))
            {
                return "View request allocation";
            }
            if ((operation == Operation.View_request_Detail))
            {
                return "View request Detail";
            }
            if ((operation == Operation.View_request_reference_number))
            {
                return "View request reference number";
            }
            if ((operation == Operation.View_requests_menu_item))
            {
                return "View requests menu item";
            }
            if ((operation == Operation.View_requisition_no_link))
            {
                return "View requisition no link";
            }
            if ((operation == Operation.View_Requisitions))
            {
                return "View Requisitions";
            }
            throw new System.ArgumentException("Unknown Operation name", "operation");
        }
        #region Enums
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Add commodity
            /// </summary>
            Add_commodity,
            /// <summary>
            /// Operation Add new plan
            /// </summary>
            Add_new_plan,
            /// <summary>
            /// Operation Add new Ration
            /// </summary>
            Add_new_Ration,
            /// <summary>
            /// Operation Add new resource allocation
            /// </summary>
            Add_new_resource_allocation,
            /// <summary>
            /// Operation Approve Monthly relife requisitions
            /// </summary>
            Approve_Monthly_relife_requisitions,
            /// <summary>
            /// Operation Approve Request
            /// </summary>
            Approve_Request,
            /// <summary>
            /// Operation Delete  allocated resource
            /// </summary>
            Delete__allocated_resource,
            /// <summary>
            /// Operation Delete PSNP plan
            /// </summary>
            Delete_PSNP_plan,
            /// <summary>
            /// Operation Edit  plan
            /// </summary>
            Edit__plan,
            /// <summary>
            /// Operation Edit PSNP plan
            /// </summary>
            Edit_PSNP_plan,
            /// <summary>
            /// Operation Edit ration
            /// </summary>
            Edit_ration,
            /// <summary>
            /// Operation New requisition
            /// </summary>
            New_requisition,
            /// <summary>
            /// Operation PSNP plan Ask approval
            /// </summary>
            PSNP_plan_Ask_approval,
            /// <summary>
            /// Operation PSNP plan history
            /// </summary>
            PSNP_plan_history,
            /// <summary>
            /// Operation Reject_Request
            /// </summary>
            Reject_Request,
            /// <summary>
            /// Operation Remove commodity
            /// </summary>
            Remove_commodity,
            /// <summary>
            /// Operation Reverse_Request
            /// </summary>
            Reverse_Request,
            /// <summary>
            /// Operation Select default ration
            /// </summary>
            Select_default_ration,
            /// <summary>
            /// Operation View  plan menu item
            /// </summary>
            View__plan_menu_item,
            /// <summary>
            /// Operation View approved requisitions
            /// </summary>
            View_approved_requisitions,
            /// <summary>
            /// Operation View assessment created plan
            /// </summary>
            View_assessment_created_plan,
            /// <summary>
            /// Operation View closed plan
            /// </summary>
            View_closed_plan,
            /// <summary>
            /// Operation View Draft plan
            /// </summary>
            View_Draft_plan,
            /// <summary>
            /// Operation View draft requisitions
            /// </summary>
            View_draft_requisitions,
            /// <summary>
            /// Operation View HRD created Plan
            /// </summary>
            View_HRD_created_Plan,
            /// <summary>
            /// Operation View hub assigned requisitions
            /// </summary>
            View_hub_assigned_requisitions,
            /// <summary>
            /// Operation View New Request menu item
            /// </summary>
            View_New_Request_menu_item,
            /// <summary>
            /// Operation View PSNP created plan
            /// </summary>
            View_PSNP_created_plan,
            /// <summary>
            /// Operation View PSNP plan menu item
            /// </summary>
            View_PSNP_plan_menu_item,
            /// <summary>
            /// Operation View ration menu item
            /// </summary>
            View_ration_menu_item,
            /// <summary>
            /// Operation View reginal PSNP plan
            /// </summary>
            View_reginal_PSNP_plan,
            /// <summary>
            /// Operation View request allocation
            /// </summary>
            View_request_allocation,
            /// <summary>
            /// Operation View request Detail
            /// </summary>
            View_request_Detail,
            /// <summary>
            /// Operation View request reference number
            /// </summary>
            View_request_reference_number,
            /// <summary>
            /// Operation View requests menu item
            /// </summary>
            View_requests_menu_item,
            /// <summary>
            /// Operation View requisition no link
            /// </summary>
            View_requisition_no_link,
            /// <summary>
            /// Operation View Requisitions
            /// </summary>
            View_Requisitions,
        }
        #endregion
    }
    #endregion
    #region Logistics Security Constants
    public class LogisticsConstants
    {
        #region Methods
         /// <summary>
        /// Retrieve Item name from a Operation Enum.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The Operation Name.</returns>
        public virtual string ItemName(Operation operation)
         {
             if ((operation == Operation.Add_new_donation))
             {
                 return "Add new donation";
             }
             if ((operation == Operation.Add_new_local_purchase))
             {
                 return "Add new local purchase";
             }
             if ((operation == Operation.Add_new_transfer_plan))
             {
                 return "Add new transfer plan";
             }
             if ((operation == Operation.Add_new_transport_order))
             {
                 return "Add new transport order";
             }
             if ((operation == Operation.Approve_local_purchase))
             {
                 return "Approve local purchase";
             }
             if ((operation == Operation.Approve_transfer_plan))
             {
                 return "Approve transfer plan";
             }
             if ((operation == Operation.Approve_transport_order))
             {
                 return "Approve transport order";
             }
             if ((operation == Operation.Approve_transport_requisition))
             {
                 return "Approve transport requisition";
             }
             if ((operation == Operation.Assign_transporter))
             {
                 return "Assign transporter";
             }
             if ((operation == Operation.Edit_donation))
             {
                 return "Edit donation";
             }
             if ((operation == Operation.Edit_GRN))
             {
                 return "Edit GRN";
             }
             if ((operation == Operation.Edit_transfer_plan))
             {
                 return "Edit transfer plan";
             }
             if ((operation == Operation.Edit_transport_order))
             {
                 return "Edit transport order";
             }
             if ((operation == Operation.Edit_transport_requisition))
             {
                 return "Edit transport requisition";
             }
             if ((operation == Operation.Generate_transport_requisition))
             {
                 return "Generate transport requisition";
             }
             if ((operation == Operation.Hub_allocation))
             {
                 return "Hub allocation";
             }
             if ((operation == Operation.Local_purchase_link))
             {
                 return "Local purchase link";
             }
             if ((operation == Operation.Receive_donation))
             {
                 return "Receive donation";
             }
             if ((operation == Operation.SI_PC_allocation))
             {
                 return "SI/PC allocation";
             }
             if ((operation == Operation.Transfer_plan_link))
             {
                 return "Transfer plan link";
             }
             if ((operation == Operation.Transport_order_link))
             {
                 return "Transport order link";
             }
             if ((operation == Operation.transport_requisition_link))
             {
                 return "transport requisition link";
             }
             if ((operation == Operation.Transporter_performance_link))
             {
                 return "Transporter performance link";
             }
             if ((operation == Operation.Veiw_free_stock_status_report))
             {
                 return "Veiw free stock status report";
             }
             if ((operation == Operation.View_active_agreement_contract))
             {
                 return "View active agreement contract";
             }
             if ((operation == Operation.View_active_transport_order))
             {
                 return "View active transport order";
             }
             if ((operation == Operation.View_approved_transport_order))
             {
                 return "View approved transport order";
             }
             if ((operation == Operation.View_bid_planning_menu_item))
             {
                 return "View bid planning menu item";
             }
             if ((operation == Operation.View_bid_winning_transporter))
             {
                 return "View bid winning transporter";
             }
             if ((operation == Operation.View_carry_over_stock_report))
             {
                 return "View carry over stock report";
             }
             if ((operation == Operation.View_commodity_recied_status_report))
             {
                 return "View commodity recied status report";
             }
             if ((operation == Operation.View_contract_admin_history))
             {
                 return "View contract admin history";
             }
             if ((operation == Operation.View_contract_Admin_menu_item))
             {
                 return "View contract Admin menu item";
             }
             if ((operation == Operation.View_dispatch_allocation_menu_item))
             {
                 return "View dispatch allocation menu item";
             }
             if ((operation == Operation.View_donation_menu_item))
             {
                 return "View donation menu item";
             }
             if ((operation == Operation.View_draft_transport_order))
             {
                 return "View draft transport order";
             }
             if ((operation == Operation. View_free_stock_report))
             {
                 return "View free stock report";
             }
           
             if ((operation == Operation.View_generate_agreement))
             {
                 return "View generate agreement";
             }
             if ((operation == Operation.View_GIN))
             {
                 return "View GIN";
             }
             if ((operation == Operation.View_Loan_Menu_Item))
             {
                 return "View Loan Menu Item";
             }
             if ((operation == Operation.View_local_purchase_menu_item))
             {
                 return "View local purchase menu item";
             }
             if ((operation == Operation.View_Payment_Requests))
             {
                 return "View Payment Requests";
             }
             if ((operation == Operation.View_transfer_menu_item))
             {
                 return "View transfer menu item";
             }
             if ((operation == Operation.View_transfereed_stock_staus_report))
             {
                 return "View transfereed stock staus report";
             }
             if ((operation == Operation.View_transport_order_menu_item))
             {
                 return "View transport order menu item";
             }
             if ((operation == Operation.View_transport_requisition_destinations))
             {
                 return "View transport requisition destinations";
             }
             if ((operation == Operation.View_transport_requisition_menu_item))
             {
                 return "View transport requisition menu item";
             }
             if ((operation == Operation.View_transporter_performance_menu_item))
             {
                 return "View transporter performance menu item";
             }
             if ((operation == Operation.Warehouse_selection))
             {
                 return "Warehouse selection";
             }
             throw new System.ArgumentException("Unknown Operation name", "operation");

             #endregion
         }

        #region Enums
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Add new donation
            /// </summary>
            Add_new_donation,
            /// <summary>
            /// Operation Add new local purchase
            /// </summary>
            Add_new_local_purchase,
            /// <summary>
            /// Operation Add new transfer plan
            /// </summary>
            Add_new_transfer_plan,
            /// <summary>
            /// Operation Add new transport order
            /// </summary>
            Add_new_transport_order,
            /// <summary>
            /// Operation Approve local purchase
            /// </summary>
            Approve_local_purchase,
            /// <summary>
            /// Operation Approve transfer plan
            /// </summary>
            Approve_transfer_plan,
            /// <summary>
            /// Operation Approve transport order
            /// </summary>
            Approve_transport_order,
            /// <summary>
            /// Operation Approve transport requisition
            /// </summary>
            Approve_transport_requisition,
            /// <summary>
            /// Operation Assign transporter
            /// </summary>
            Assign_transporter,
            /// <summary>
            /// Operation Edit donation
            /// </summary>
            Edit_donation,
            /// <summary>
            /// Operation Edit GRN
            /// </summary>
            Edit_GRN,
            /// <summary>
            /// Operation Edit transfer plan
            /// </summary>
            Edit_transfer_plan,
            /// <summary>
            /// Operation Edit transport order
            /// </summary>
            Edit_transport_order,
            /// <summary>
            /// Operation Edit transport requisition
            /// </summary>
            Edit_transport_requisition,
            /// <summary>
            /// Operation Generate transport requisition
            /// </summary>
            Generate_transport_requisition,
            /// <summary>
            /// Operation Hub allocation
            /// </summary>
            Hub_allocation,
            /// <summary>
            /// Operation Local purchase link
            /// </summary>
            Local_purchase_link,
            /// <summary>
            /// Operation Receive donation
            /// </summary>
            Receive_donation,
            /// <summary>
            /// Operation SI/PC allocation
            /// </summary>
            SI_PC_allocation,
            /// <summary>
            /// Operation Transfer plan link
            /// </summary>
            Transfer_plan_link,
            /// <summary>
            /// Operation Transport order link
            /// </summary>
            Transport_order_link,
            /// <summary>
            /// Operation transport requisition link
            /// </summary>
            transport_requisition_link,
            /// <summary>
            /// Operation Transporter performance link
            /// </summary>
            Transporter_performance_link,
            /// <summary>
            /// Operation Veiw free stock status report
            /// </summary>
            Veiw_free_stock_status_report,
            /// <summary>
            /// Operation View active agreement contract
            /// </summary>
            View_active_agreement_contract,
            /// <summary>
            /// Operation View active transport order
            /// </summary>
            View_active_transport_order,
            /// <summary>
            /// Operation View approved transport order
            /// </summary>
            View_approved_transport_order,
            /// <summary>
            /// Operation View bid planning menu item
            /// </summary>
            View_bid_planning_menu_item,
            /// <summary>
            /// Operation View bid winning transporter
            /// </summary>
            View_bid_winning_transporter,
            /// <summary>
            /// Operation View carry over stock report
            /// </summary>
            View_carry_over_stock_report,
            /// <summary>
            /// Operation View commodity recied status report
            /// </summary>
            View_commodity_recied_status_report,
            /// <summary>
            /// Operation View contract admin history
            /// </summary>
            View_contract_admin_history,
            /// <summary>
            /// Operation View contract Admin menu item
            /// </summary>
            View_contract_Admin_menu_item,
            /// <summary>
            /// Operation View dispatch allocation menu item
            /// </summary>
            View_dispatch_allocation_menu_item,
            /// <summary>
            /// Operation View donation menu item
            /// </summary>
            View_donation_menu_item,
            /// <summary>
            /// Operation View draft transport order
            /// </summary>
            View_draft_transport_order,
            /// <summary>
            /// Operation View generate agreement
            /// </summary>
            View_free_stock_report,
            /// <summary>
            /// Operation View free stock report
            /// </summary>
            View_generate_agreement,
            /// <summary>
            /// Operation View GIN
            /// </summary>
            View_GIN,
            /// <summary>
            /// Operation View local purchase menu item
            /// </summary>
            View_Loan_Menu_Item,
            /// <summary>
            /// Operation View local purchase menu item
            /// </summary>
            View_local_purchase_menu_item,
            /// <summary>
            /// Operation View transfer menu item
            /// </summary>
            /// 
            View_Payment_Requests,
            /// <summary>
            /// Operation View transfer menu item
            /// </summary>
            /// 
            View_transfer_menu_item,
            /// <summary>
            /// Operation View transfereed stock staus report
            /// </summary>
            View_transfereed_stock_staus_report,
            /// <summary>
            /// Operation View transport order menu item
            /// </summary>
            View_transport_order_menu_item,
            /// <summary>
            /// Operation View transport requisition destinations
            /// </summary>
            View_transport_requisition_destinations,
            /// <summary>
            /// Operation View transport requisition menu item
            /// </summary>
            View_transport_requisition_menu_item,
            /// <summary>
            /// Operation View transporter performance menu item
            /// </summary>
            View_transporter_performance_menu_item,
            /// <summary>
            /// Operation Warehouse selection
            /// </summary>
            Warehouse_selection,
        }
        #endregion
    }
    #endregion

    #region Procurement Security Constants
    public class ProcurementConstants
    {
        /// <summary>
        /// Retrieve Item name from a Operation Enum.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The Operation Name.</returns>
        public virtual string ItemName(Operation operation)
        {
            if ((operation == Operation.Assign_Transporter))
            {
                return "Assign Transporter";
            }
            if ((operation == Operation.Bid_Planning))
            {
                return "Bid Planning";
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
            if ((operation == Operation.Generate_Agreement))
            {
                return "Generate Agreement";
            }
            if ((operation == Operation.Generate_Winners_for_a_bid))
            {
                return "Generate Winners for a bid";
            }
            if ((operation == Operation.Manage_Bids))
            {
                return "Manage Bids";
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
            if ((operation == Operation.Print_RFQ))
            {
                return "Print RFQ";
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
            if ((operation == Operation.View_bid_menu_item))
            {
                return "View bid menu item";
            }
            if ((operation == Operation.View_bid_plan_menu_item))
            {
                return "View bid plan menu item";
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
            if ((operation == Operation.View_generate_winners_menu_item))
            {
                return "View generate winners menu item";
            }
            if ((operation == Operation.View_History))
            {
                return "View History";
            }
            if ((operation == Operation.View_Price_Quotation_Data_Entries))
            {
                return "View Price Quotation Data Entries";
            }
            if ((operation == Operation.View_Request_For_Quotation))
            {
                return "View Request For Quotation";
            }
            if ((operation == Operation.View_show_proposals))
            {
                return "View show proposals";
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

        #region Enums
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Assign Transporter
            /// </summary>
            Assign_Transporter,
            /// <summary>
            /// Operation Bid Planning
            /// </summary>
            Bid_Planning,
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
            /// Operation Generate Agreement
            /// </summary>
            Generate_Agreement,
            /// <summary>
            /// Operation Generate Winners for a bid
            /// </summary>
            Generate_Winners_for_a_bid,
            /// <summary>
            /// Operation Manage Bids
            /// </summary>
            Manage_Bids,
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
            /// Operation Print RFQ
            /// </summary>
            Print_RFQ,
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
            /// Operation View bid menu item
            /// </summary>
            View_bid_menu_item,
            /// <summary>
            /// Operation View bid plan menu item
            /// </summary>
            View_bid_plan_menu_item,
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
            /// Operation View generate winners menu item
            /// </summary>
            View_generate_winners_menu_item,
            /// <summary>
            /// Operation View History
            /// </summary>
            View_History,
            /// <summary>
            /// Operation View Price Quotation Data Entries
            /// </summary>
            View_Price_Quotation_Data_Entries,
            /// <summary>
            /// Operation View Request For Quotation
            /// </summary>
            View_Request_For_Quotation,
            /// <summary>
            /// Operation View show proposals
            /// </summary>
            View_show_proposals,
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
            if ((operation == Operation.View_store_report))
            {
                return "View store report";
            }
            if ((operation == Operation.View_transport_order_list))
            {
                return "View transport order list";
            }
            if ((operation == Operation.View_transportation_report))
            {
                return "View transportation report";
            }
            throw new System.ArgumentException("Unknown Operation name", "operation");
        }

        #region Enums
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
            /// Operation View store report
            /// </summary>
            View_store_report,
            /// <summary>
            /// Operation View transport order list
            /// </summary>
            View_transport_order_list,
            /// <summary>
            /// Operation View transportation report
            /// </summary>
            View_transportation_report,
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
            if ((role == Role.BureauHead))
            {
                return "BureauHead";
            }
            if ((role == Role.DataEntry))
            {
                return "DataEntry";
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
            if ((task == Task.Mange_Distribution))
            {
                return "Mange Distribution";
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
            if ((operation == Operation.Add_Distribution_Entry))
            {
                return "Add Distribution Entry";
            }
            if ((operation == Operation.Add_new_needs_assessment))
            {
                return "Add new needs assessment";
            }
            if ((operation == Operation.Add_new_regional_request))
            {
                return "Add new regional request";
            }
            if ((operation == Operation.Add_woreda_distribution))
            {
                return "Add woreda distribution";
            }
            if ((operation == Operation.Allocate_requests))
            {
                return "Allocate requests";
            }
            if ((operation == Operation.Approve__need_assesment))
            {
                return "Approve  need assesment";
            }
            if ((operation == Operation.Approve_regional_request))
            {
                return "Approve regional request";
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
            if (operation == Operation.Print_requisition)
            {
                return "Print Requisition";
            }
            if ((operation == Operation.Vew_requests_menu_item))
            {
                return "Vew requests menu item";
            }
            if ((operation == Operation.View_allocate_link))
            {
                return "View allocate link";
            }
            if ((operation == Operation.View_detial_link))
            {
                return "View detial link";
            }
            if ((operation == Operation.View_Distribution_Entry))
            {
                return "View Distribution Entry";
            }
            if ((operation == Operation.View_Need_assesment_menu_item))
            {
                return "View Need assesment menu item";
            }
            if ((operation == Operation.View_needs_assessment))
            {
                return "View needs assessment";
            }
            if ((operation == Operation.View_reference_link))
            {
                return "View reference link";
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
            /// Role BureauHead
            /// </summary>
            BureauHead,
            /// <summary>
            /// Role DataEntry
            /// </summary>
            DataEntry,
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
            /// <summary>
            /// Task Mange Distribution
            /// </summary>
            Mange_Distribution,
        }
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Add Distribution Entry
            /// </summary>
            Add_Distribution_Entry,
            /// <summary>
            /// Operation Add new needs assessment
            /// </summary>
            Add_new_needs_assessment,
            /// <summary>
            /// Operation Add new regional request
            /// </summary>
            Add_new_regional_request,
            /// <summary>
            /// Operation Add woreda distribution
            /// </summary>
            Add_woreda_distribution,
            /// <summary>
            /// Operation Allocate requests
            /// </summary>
            Allocate_requests,
            /// <summary>
            /// Operation Approve  need assesment
            /// </summary>
            Approve__need_assesment,
            /// <summary>
            /// Operation Approve regional request
            /// </summary>
            Approve_regional_request,
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
            /// Operation Print Requisition
            /// </summary>
            Print_requisition,
            /// <summary>
            /// Operation Vew requests menu item
            /// </summary>
            Vew_requests_menu_item,
            /// <summary>
            /// Operation View allocate link
            /// </summary>
            View_allocate_link,
            /// <summary>
            /// Operation View detial link
            /// </summary>
            View_detial_link,
            /// <summary>
            /// Operation View Distribution Entry
            /// </summary>
            View_Distribution_Entry,
            /// <summary>
            /// Operation View Need assesment menu item
            /// </summary>
            View_Need_assesment_menu_item,
            /// <summary>
            /// Operation View needs assessment
            /// </summary>
            View_needs_assessment,
            /// <summary>
            /// Operation View reference link
            /// </summary>
            View_reference_link,
            /// <summary>
            /// Operation View requests
            /// </summary>
            View_requests,
        }
        #endregion
    }
    #endregion
    #region Finance Security Constants
    public class FinanceConstants
    {
        #region Methods
        /// <summary>
        /// Retrieve Item name from a Operation Enum.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The Operation Name.</returns>
       /// <summary>
        /// Retrieve Item name from a Role Enum.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The Role Name.</returns>
        public virtual string ItemName(Role role)
        {
            if ((role == Role.Finance__Case_Team_Coordinator))
            {
                return "Finance  Case Team Coordinator";
            }
            if ((role == Role.Finance_DataEntry))
            {
                return "Finance-DataEntry";
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
            if ((task == Task.Manage_payment_request))
            {
                return "Manage payment request";
            }
            if ((task == Task.Payment_workflow))
            {
                return "Payment workflow";
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
            if ((operation == Operation.Approve_cheque))
            {
                return "Approve cheque";
            }
            if ((operation == Operation.Approve_Payment_request))
            {
                return "Approve Payment request";
            }
            if ((operation == Operation.Collect_cheque))
            {
                return "Collect cheque";
            }
            if ((operation == Operation.Contract_agreement_link))
            {
                return "Contract agreement link";
            }
            if ((operation == Operation.Issue_cheque))
            {
                return "Issue cheque";
            }
            if ((operation == Operation.Payment_deduction))
            {
                return "Payment deduction";
            }
            if ((operation == Operation.Payment_request_detail))
            {
                return "Payment request detail";
            }
            if ((operation == Operation.Transporter_link))
            {
                return "Transporter link";
            }
            if ((operation == Operation.Transporter_order_link))
            {
                return "Transporter order link";
            }
            if ((operation == Operation.View_cheque_information))
            {
                return "View cheque information";
            }
            if ((operation == Operation.View_history))
            {
                return "View history";
            }
            if ((operation == Operation.View_Payment_request_menu_items))
            {
                return "View Payment request menu items";
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
            /// Role Finance  Case Team Coordinator
            /// </summary>
            Finance__Case_Team_Coordinator,
            /// <summary>
            /// Role Finance-DataEntry
            /// </summary>
            Finance_DataEntry,
        }
        /// <summary>
        /// Tasks Enumeration
        /// </summary>
        public enum Task
        {
            /// <summary>
            /// Task Manage payment request
            /// </summary>
            Manage_payment_request,
            /// <summary>
            /// Task Payment workflow
            /// </summary>
            Payment_workflow,
        }
        /// <summary>
        /// Operations Enumeration
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// Operation Approve cheque
            /// </summary>
            Approve_cheque,
            /// <summary>
            /// Operation Approve Payment request
            /// </summary>
            Approve_Payment_request,
            /// <summary>
            /// Operation Collect cheque
            /// </summary>
            Collect_cheque,
            /// <summary>
            /// Operation Contract agreement link
            /// </summary>
            Contract_agreement_link,
            /// <summary>
            /// Operation Issue cheque
            /// </summary>
            Issue_cheque,
            /// <summary>
            /// Operation Payment deduction
            /// </summary>
            Payment_deduction,
            /// <summary>
            /// Operation Payment request detail
            /// </summary>
            Payment_request_detail,
            /// <summary>
            /// Operation Transporter link
            /// </summary>
            Transporter_link,
            /// <summary>
            /// Operation Transporter order link
            /// </summary>
            Transporter_order_link,
            /// <summary>
            /// Operation View cheque information
            /// </summary>
            View_cheque_information,
            /// <summary>
            /// Operation View history
            /// </summary>
            View_history,
            /// <summary>
            /// Operation View Payment request menu items
            /// </summary>
            View_Payment_request_menu_items,
        }
        #endregion
    }
    #endregion  
}