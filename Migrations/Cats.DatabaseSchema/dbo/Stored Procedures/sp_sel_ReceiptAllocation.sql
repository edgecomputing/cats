
CREATE PROCEDURE [sp_sel_ReceiptAllocation]
AS
  SELECT 
    [PartitionID],
    [ReceiptAllocationID],
    [TransactionGroupID],
    [ETA],
    [ProjectNumber],
    [GiftCertificateDetailID],
    [CommodityID],
    [SINumber],
    [QuantityInMT],
    [HubID],
    [DonorID],
    [ProgramID],
    [CommoditySourceID]
  FROM 
    [dbo].[ReceiptAllocation]