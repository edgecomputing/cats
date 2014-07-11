


Create PROC [dbo].[RPT_StockStatusNonFood]
(
	@Warehouse int,
	@commodity int
)
AS
BEGIN
select *,StoreID = 0 from 
(select s.Number as StoreNo, pc.Value as Project, si.Value as SINumber, SUM(t.QuantityInUnit) as Balance from [Transaction] t join ShippingInstruction si on t.ShippingInstructionID = si.ShippingInstructionID join ProjectCode pc on t.ProjectCodeID = pc.ProjectCodeID join Store s on t.StoreID = s.StoreID where t.HubID = @Warehouse and (t.ParentCommodityID = @commodity) and LedgerID in (2) group by s.StoreID, s.Number, si.Value,pc.Value ) as PivotData
		PIVOT
		(
		SUM(Balance)
		For StoreNo in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20])
		) AS PivotTable
END


