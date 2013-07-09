





CREATE PROC [dbo].[RPT_DispatchFulfillmentStatus]
(
	@Hub int
)
AS
BEGIN
	select Allocated.Name as Commodtiy,Allocated.Region,Allocated.Zone,  Allocated.CommodityID, Allocated.RequisitionNo, Allocated.Name as CommodityName, Allocated.QuantityAllocated, Dispatched.QuantityDispatched,FirstDate,LastDate, case when QuantityAllocated <> 0 then Dispatched.QuantityDispatched / Allocated.QuantityAllocated end * 100 as PercentageFulfilled   from (
		select c.Name,region.Name as Region, zone.Name as Zone,c.CommodityID, da.RequisitionNo,SUM(da.Amount / 10) QuantityAllocated from DispatchAllocation da join Commodity c on da.CommodityID = c.CommodityID join FDP fd on fd.FDPID = da.FDPID join AdminUnit woreda on woreda.AdminUnitID = fd.AdminUnitID join AdminUnit zone on zone.AdminUnitID = woreda.ParentID join AdminUnit region on zone.ParentID = region.AdminUnitID where da.HubID = @Hub group by da.RequisitionNo, c.Name,c.CommodityID,region.Name,zone.Name
	) as Allocated
	left join 
	(select d.RequisitionNo,abs(SUM(t.QuantityInMT)) QuantityDispatched, MAX(t.TransactionDate) as LastDate, MIN(t.TransactionDate) as FirstDate from [Transaction] t join DispatchDetail da  on t.TransactionGroupID = da.TransactionGroupID join Dispatch d on d.DispatchID = da.DispatchID where t.LedgerID = 9 and t.HubID = @Hub group by d.RequisitionNo) Dispatched
	on Allocated.RequisitionNo = Dispatched.RequisitionNo

END




