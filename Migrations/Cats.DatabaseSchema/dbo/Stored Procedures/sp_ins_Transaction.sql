

CREATE PROCEDURE [sp_ins_Transaction] (
  @PartitionID int,
  @TransactionGroupID int,
  @LedgerID int,
  @HubOwnerID int,
  @AccountID int,
  @HubID int,
  @StoreID int,
  @Stack int,
  @ProjectCodeID int,
  @ShippingInstructionID int,
  @ProgramID int,
  @ParentCommodityID int,
  @CommodityID int,
  @CommodityGradeID int,
  @QuantityInMT decimal(18,3),
  @QuantityInUnit decimal(18,3),
  @UnitID int,
  @TransactionDate datetime
)
AS
  INSERT INTO [dbo].[Transaction] (
    [PartitionID],
    [TransactionGroupID],
    [LedgerID],
    [HubOwnerID],
    [AccountID],
    [HubID],
    [StoreID],
    [Stack],
    [ProjectCodeID],
    [ShippingInstructionID],
    [ProgramID],
    [ParentCommodityID],
    [CommodityID],
    [CommodityGradeID],
    [QuantityInMT],
    [QuantityInUnit],
    [UnitID],
    [TransactionDate]
  )
  VALUES (
    @PartitionID,
    @TransactionGroupID,
    @LedgerID,
    @HubOwnerID,
    @AccountID,
    @HubID,
    @StoreID,
    @Stack,
    @ProjectCodeID,
    @ShippingInstructionID,
    @ProgramID,
    @ParentCommodityID,
    @CommodityID,
    @CommodityGradeID,
    @QuantityInMT,
    @QuantityInUnit,
    @UnitID,
    @TransactionDate
  )