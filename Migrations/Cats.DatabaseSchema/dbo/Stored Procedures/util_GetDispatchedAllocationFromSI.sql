

CREATE PROCEDURE [dbo].[util_GetDispatchedAllocationFromSI]
@HubID int,
@ShippingInstruction int

AS
BEGIN
SET NOCOUNT ON;
select sum(t.QuantityInMT) Quantity, SUM(t.QuantityInUnit) QuantityInUnit 
from DispatchAllocation da 
join Dispatch d on da.DispatchAllocationID = d.DispatchAllocationID 
join DispatchDetail dd on d.DispatchID = dd.DispatchID 
join [Transaction] t on t.TransactionGroupID = dd.TransactionGroupID 
where da.ShippingInstructionID = @ShippingInstruction and IsClosed = 0 and t.LedgerID = 9 and d.HubID = @HubID
END
