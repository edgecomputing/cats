using System;

namespace Cats.AzManHelpers
{
    public class AzManHelper
    {
        public struct Early_Warning
        {
            public enum Role
            {
                /// <summary>
                /// Role EW- Coordinator
                /// </summary>
                EW__Coordinator,
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
            public enum Task
            {
                /// <summary>
                /// Task Approve HRD
                /// </summary>
                Approve_HRD,
                /// <summary>
                /// Task Approve Needs assessment
                /// </summary>
                Approve_Needs_assessment,
                /// <summary>
                /// Task Approve requests
                /// </summary>
                Approve_requests,
                /// <summary>
                /// Task Approve Requisitions
                /// </summary>
                Approve_Requisitions,
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
                /// Operation Create new needs assessment
                /// </summary>
                Create_new_needs_assessment,
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
                /// Operation Gift-Add new item
                /// </summary>
                Gift_Add_new_item,
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
                /// Operation Requisition exceptions
                /// </summary>
                Requisition_exceptions,
                /// <summary>
                /// Operation View approved needs assesment
                /// </summary>
                View_approved_needs_assesment,
                /// <summary>
                /// Operation View approved requisitions
                /// </summary>
                View_approved_requisitions,
                /// <summary>
                /// Operation View Beneficiary no and duration of assisstance
                /// </summary>
                View_Beneficiary_no_and_duration_of_assisstance,
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
                /// Operation View HRD list
                /// </summary>
                View_HRD_list,
                /// <summary>
                /// Operation View request
                /// </summary>
                View_request,
                /// <summary>
                /// Operation View submitted requests
                /// </summary>
                View_submitted_requests,
            }
        }

        public struct Finance
        {
            public enum Role
            {
                /// <summary>
                /// Role Finance & Procurement Case Team Coordinator
                /// </summary>
                Finance___Procurement_Case_Team_Coordinator,
                /// <summary>
                /// Role Finance-Account Settlement
                /// </summary>
                Finance_Account_Settlement,
                /// <summary>
                /// Role Finance-Accountant
                /// </summary>
                Finance_Accountant,
                /// <summary>
                /// Role Finance-Accountant (check officer )
                /// </summary>
                Finance_Accountant__check_officer,
                /// <summary>
                /// Role Finance-Accountant(Registration)
                /// </summary>
                Finance_Accountant_Registration,
                /// <summary>
                /// Role Finance-Accountant(Verification)
                /// </summary>
                Finance_Accountant_Verification,
                /// <summary>
                /// Role Finance-Budget Section(Accountant)
                /// </summary>
                Finance_Budget_Section_Accountant,
                /// <summary>
                /// Role Finance-Cashier(Accountant)
                /// </summary>
                Finance_Cashier_Accountant,
                /// <summary>
                /// Role Finance-Data Encoder
                /// </summary>
                Finance_Data_Encoder,
                /// <summary>
                /// Role Finance-officer
                /// </summary>
                Finance_officer,
                /// <summary>
                /// Role Finance-Payment Section(Senior Accountant)
                /// </summary>
                Finance_Payment_Section_Senior_Accountant,
            }
        }

        public struct Logistics
        {
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
                /// Task Hub Allocation
                /// </summary>
                Hub_Allocation,
                /// <summary>
                /// Task Manage RFQ
                /// </summary>
                Manage_RFQ,
                /// <summary>
                /// Task Manage transport suppliers
                /// </summary>
                Manage_transport_suppliers,
                /// <summary>
                /// Task Transport Order
                /// </summary>
                Transport_Order,
            }

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
                /// Operation New transport order
                /// </summary>
                New_transport_order,
                /// <summary>
                /// Operation New transport supplier
                /// </summary>
                New_transport_supplier,
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
                /// Operation View draft hub allocation
                /// </summary>
                View_draft_hub_allocation,
                /// <summary>
                /// Operation View transporter list
                /// </summary>
                View_transporter_list,
            }
        }

        public struct Procurement
        {
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

            public enum Task
            {
                /// <summary>
                /// Task Bid plan
                /// </summary>
                Bid_plan,
                /// <summary>
                /// Task Manage bid
                /// </summary>
                Manage_bid,
            }

            public enum Operation
            {
                /// <summary>
                /// Operation Create new bid
                /// </summary>
                Create_new_bid,
                /// <summary>
                /// Operation Create new bid plan
                /// </summary>
                Create_new_bid_plan,
                /// <summary>
                /// Operation Delete bid plan
                /// </summary>
                Delete_bid_plan,
                /// <summary>
                /// Operation Edit bid
                /// </summary>
                Edit_bid,
                /// <summary>
                /// Operation Edit bid plan
                /// </summary>
                Edit_bid_plan,
                /// <summary>
                /// Operation Export bid
                /// </summary>
                Export_bid,
                /// <summary>
                /// Operation Export bid plan
                /// </summary>
                Export_bid_plan,
                /// <summary>
                /// Operation Print bid
                /// </summary>
                Print_bid,
                /// <summary>
                /// Operation Print bid plan
                /// </summary>
                Print_bid_plan,
                /// <summary>
                /// Operation Transport warehouse assignment
                /// </summary>
                Transport_warehouse_assignment,
                /// <summary>
                /// Operation View approved bid
                /// </summary>
                View_approved_bid,
                /// <summary>
                /// Operation View bid list
                /// </summary>
                View_bid_list,
                /// <summary>
                /// Operation View bid plan
                /// </summary>
                View_bid_plan,
                /// <summary>
                /// Operation View current bid
                /// </summary>
                View_current_bid,
            }
        }

        public struct PSNP
        {
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
        }

        public enum Applications
        {
            Early_Warning,
            Finance,
            Logistics,
            Procurement,
            PSNP
        }

        public enum Store
        {
            CATS
        }
    }
}
