


CREATE PROCEDURE [dbo].[sp_ins_Receive] (
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
  INSERT INTO [dbo].[Receive] (
    [PartitionID],
    [HubID],
    [GRN],
    [CommodityTypeID],
    [SourceDonorID],
    [ResponsibleDonorID],
    [TransporterID],
    [PlateNo_Prime],
    [PlateNo_Trailer],
    [DriverName],
    [WeightBridgeTicketNumber],
    [WeightBeforeUnloading],
    [WeightAfterUnloading],
    [ReceiptDate],
    [UserProfileID],
    [CreatedDate],
    [WayBillNo],
    [CommoditySourceID],
    [Remark],
    [VesselName],
    [ReceivedByStoreMan],
    [PortName]
  )
  VALUES (
    @PartitionID,
    @HubID,
    @GRN,
    @CommodityTypeID,
    @SourceDonorID,
    @ResponsibleDonorID,
    @TransporterID,
    @PlateNo_Prime,
    @PlateNo_Trailer,
    @DriverName,
    @WeightBridgeTicketNumber,
    @WeightBeforeUnloading,
    @WeightAfterUnloading,
    @ReceiptDate,
    @UserProfileID,
    @CreatedDate,
    @WayBillNo,
    @CommoditySourceID,
    @Remark,
    @VesselName,
    @ReceivedByStoreMan,
    @PortName
  )
  
Select SCOPE_IDENTITY() as ReceiveID