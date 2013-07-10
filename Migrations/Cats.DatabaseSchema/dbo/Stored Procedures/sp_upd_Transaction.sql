

CREATE PROCEDURE [sp_upd_Transaction] (
  @PartitionID int,
  @TransactionID int,
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
  UPDATE [dbo].[Transaction] SET
    [TransactionGroupID] = @TransactionGroupID,
    [LedgerID] = @LedgerID,
    [HubOwnerID] = @HubOwnerID,
    [AccountID] = @AccountID,
    [HubID] = @HubID,
    [StoreID] = @StoreID,
    [Stack] = @Stack,
    [ProjectCodeID] = @ProjectCodeID,
    [ShippingInstructionID] = @ShippingInstructionID,
    [ProgramID] = @ProgramID,
    [ParentCommodityID] = @ParentCommodityID,
    [CommodityID] = @CommodityID,
    [CommodityGradeID] = @CommodityGradeID,
    [QuantityInMT] = @QuantityInMT,
    [QuantityInUnit] = @QuantityInUnit,
    [UnitID] = @UnitID,
    [TransactionDate] = @TransactionDate
  WHERE 
    ([PartitionID] = @PartitionID) AND
    ([TransactionID] = @TransactionID)