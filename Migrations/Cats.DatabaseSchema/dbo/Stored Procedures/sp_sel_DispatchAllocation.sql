
CREATE PROCEDURE [sp_sel_DispatchAllocation]
AS
  SELECT 
    [PartitionID],
    [DispatchAllocationID],
    [TransactionGroupID],
    [HubID],
    [Year],
    [Month],
    [Round],
    [DonorID],
    [ProgramID],
    [CommodityID],
    [RequisitionNo],
    [BidRefNo],
    [Beneficiery],
    [Amount],
    [Unit],
    [TransporterID],
    [FDPID]
  FROM 
    [dbo].[DispatchAllocation]