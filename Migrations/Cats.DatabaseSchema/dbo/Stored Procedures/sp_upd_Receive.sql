

CREATE PROCEDURE [sp_upd_Receive] (
  @ReceiveID int,
  @PartitionID int,
  @HubID int,
  @GRN nvarchar(6),
  @CommodityTypeID int,
  @SourceDonorID int,
  @ResponsibleDonorID int,
  @TransporterID int,
  @PlateNo_Prime nvarchar(50),
  @PlateNo_Trailer nvarchar(50),
  @DriverName nvarchar(50),
  @WeightBridgeTicketNumber nvarchar(10),
  @WeightBeforeUnloading decimal(18,3),
  @WeightAfterUnloading decimal(18,3),
  @ReceiptDate datetime,
  @UserProfileID int,
  @CreatedDate datetime,
  @WayBillNo nvarchar(50),
  @CommoditySourceID int,
  @Remark nvarchar(4000),
  @VesselName nvarchar(50),
  @ReceivedByStoreMan nvarchar(50),
  @PortName nvarchar(50)
)
AS
  UPDATE [dbo].[Receive] SET
    [HubID] = @HubID,
    [GRN] = @GRN,
    [CommodityTypeID] = @CommodityTypeID,
    [SourceDonorID] = @SourceDonorID,
    [ResponsibleDonorID] = @ResponsibleDonorID,
    [TransporterID] = @TransporterID,
    [PlateNo_Prime] = @PlateNo_Prime,
    [PlateNo_Trailer] = @PlateNo_Trailer,
    [DriverName] = @DriverName,
    [WeightBridgeTicketNumber] = @WeightBridgeTicketNumber,
    [WeightBeforeUnloading] = @WeightBeforeUnloading,
    [WeightAfterUnloading] = @WeightAfterUnloading,
    [ReceiptDate] = @ReceiptDate,
    [UserProfileID] = @UserProfileID,
    [CreatedDate] = @CreatedDate,
    [WayBillNo] = @WayBillNo,
    [CommoditySourceID] = @CommoditySourceID,
    [Remark] = @Remark,
    [VesselName] = @VesselName,
    [ReceivedByStoreMan] = @ReceivedByStoreMan,
    [PortName] = @PortName
  WHERE 
    ([ReceiveID] = @ReceiveID) AND
    ([PartitionID] = @PartitionID)