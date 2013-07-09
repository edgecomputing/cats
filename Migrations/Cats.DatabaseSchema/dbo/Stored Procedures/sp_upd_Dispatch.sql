

CREATE PROCEDURE [sp_upd_Dispatch] (
  @DispatchID int,
  @PartitionID int,
  @HubID int,
  @GIN nvarchar(6),
  @FDPID int,
  @WeighBridgeTicketNumber nvarchar(50),
  @RequisitionNo nvarchar(50),
  @BidNumber nvarchar(50),
  @TransporterID int,
  @DriverName nvarchar(50),
  @PlateNo_Prime nvarchar(50),
  @PlateNo_Trailer nvarchar(50),
  @PeriodYear int,
  @PeriodMonth int,
  @Round int,
  @UserProfileID int,
  @DispatchDate datetime,
  @CreatedDate datetime,
  @Remark nvarchar(4000),
  @DispatchedByStoreMan nvarchar(50)
)
AS
  UPDATE [dbo].[Dispatch] SET
    [HubID] = @HubID,
    [GIN] = @GIN,
    [FDPID] = @FDPID,
    [WeighBridgeTicketNumber] = @WeighBridgeTicketNumber,
    [RequisitionNo] = @RequisitionNo,
    [BidNumber] = @BidNumber,
    [TransporterID] = @TransporterID,
    [DriverName] = @DriverName,
    [PlateNo_Prime] = @PlateNo_Prime,
    [PlateNo_Trailer] = @PlateNo_Trailer,
    [PeriodYear] = @PeriodYear,
    [PeriodMonth] = @PeriodMonth,
    [Round] = @Round,
    [UserProfileID] = @UserProfileID,
    [DispatchDate] = @DispatchDate,
    [CreatedDate] = @CreatedDate,
    [Remark] = @Remark,
    [DispatchedByStoreMan] = @DispatchedByStoreMan
  WHERE 
    ([DispatchID] = @DispatchID) AND
    ([PartitionID] = @PartitionID)