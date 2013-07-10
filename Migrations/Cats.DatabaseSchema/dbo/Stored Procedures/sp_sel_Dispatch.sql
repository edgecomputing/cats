
CREATE PROCEDURE [sp_sel_Dispatch]
AS
  SELECT 
    [DispatchID],
    [PartitionID],
    [HubID],
    [GIN],
    [FDPID],
    [WeighBridgeTicketNumber],
    [RequisitionNo],
    [BidNumber],
    [TransporterID],
    [DriverName],
    [PlateNo_Prime],
    [PlateNo_Trailer],
    [PeriodYear],
    [PeriodMonth],
    [Round],
    [UserProfileID],
    [DispatchDate],
    [CreatedDate],
    [Remark],
    [DispatchedByStoreMan]
  FROM 
    [dbo].[Dispatch]