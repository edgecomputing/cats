





CREATE PROC [dbo].[RPT_StatusReportBySI]
(
	@Hub int
)
AS
BEGIN
	select 
		c.CommodityID, 
		pc.Value as ProjectCode,
		c.Name as CommodityName, 
		uncomm.SINumber,
		isnull(uncomm.UncommitedStock,0) - (ISNULL(commitedToOthers.CommitedBalance, 0) + ISNULL(commitedToFDP.CommitedBalance,0))  UncommitedStock, 
		isnull(commitedToFDP.CommitedBalance,0) + isnull(commitedToOthers.CommitedBalance,0) CommitedBalance, 
		isnull(uncomm.UncommitedStock,0) as TotalStockOnHand, 
		receiveAllocated.AllocatedBalance AllocatedToHub, 
		received.ReceivedBalance, 
		(case when receiveAllocated.AllocatedBalance = 0 then 1 else received.ReceivedBalance / receiveAllocated.AllocatedBalance end) * 100 as PercentageReceived  
	 from  
	 
	(
		select t.ParentCommodityID as CommodityID,t.ShippingInstructionID ,t.ProjectCodeID, SUM(t.QuantityInMT) ReceivedBalance 
			from [Transaction] t 
			where t.LedgerID = 2 and t.QuantityInMT > 0 and t.HubID = @Hub 
			group by t.ShippingInstructionID, t.ParentCommodityID,t.ProjectCodeID
			) received
	left join		
	 
	(
		select si.Value as SINumber, t.ShippingInstructionID ,t.ProjectCodeID, t.ParentCommodityID as CommodityID, SUM(t.QuantityInMT) UncommitedStock from [Transaction] t join ShippingInstruction si on t.ShippingInstructionID = si.ShippingInstructionID where t.LedgerID = 2 and t.HubID = @Hub group by t.ShippingInstructionID, t.ParentCommodityID,si.Value,t.ProjectCodeID) 
	uncomm
	on  received.CommodityID = uncomm.CommodityID  and  received.ShippingInstructionID = uncomm.ShippingInstructionID and received.ProjectCodeID = uncomm.ProjectCodeID
	left join 
	(
		-- convert the default Quintal quantity on dispatch allocation to MT
		select t.CommodityID as CommodityID ,t.ProjectCodeID, t.ShippingInstructionID ,SUM(t.Amount / 10) CommitedBalance 
			from DispatchAllocation t 
			where  t.HubID = @Hub  and t.ShippingInstructionID is not null
			group by t.ShippingInstructionID, t.CommodityID, t.ProjectCodeID
	) as commitedToFDP
	on received.CommodityID = commitedToFDP.CommodityID 
		and received.ShippingInstructionID = commitedToFDP.ShippingInstructionID and received.ProjectCodeID = commitedToFDP.ProjectCodeID
	left join 
	(
		-- convert the default Quintal quantity on dispatch allocation to MT
		select t.CommodityID as CommodityID ,t.ProjectCodeID, t.ShippingInstructionID ,SUM(t.QuantityInMT) CommitedBalance 
			from OtherDispatchAllocation t 
			where  t.HubID = @Hub 
			group by t.ShippingInstructionID, t.CommodityID, t.ProjectCodeID
	) as commitedToOthers
	on
		received.CommodityID = commitedToOthers.CommodityID and received.ShippingInstructionID = commitedToOthers.ShippingInstructionID and received.ProjectCodeID = commitedToOthers.ProjectCodeID
	left join 
	(
		select raBeforeAggregation.CommodityID, raBeforeAggregation.ShippingInstructionID, sum(raBeforeAggregation.AllocatedBalance) as AllocatedBalance from 
		(select 
			case when (select top 1 c.ParentID from Commodity c where CommodityID = t.CommodityID) is null then t.CommodityID else (select top 1 c.ParentID from Commodity c where c.CommodityID = t.CommodityID) end  as CommodityID ,
			si.ShippingInstructionID , 
			t.QuantityInMT AllocatedBalance 
			from [ReceiptAllocation] t join ShippingInstruction si on t.SINumber = si.Value 
			where t.QuantityInMT > 0 and t.HubID = @Hub 
			) raBeforeAggregation
			group by raBeforeAggregation.ShippingInstructionID, raBeforeAggregation.CommodityID
			 ) receiveAllocated
		on received.CommodityID = receiveAllocated.CommodityID and received.ShippingInstructionID = receiveAllocated.ShippingInstructionID
	
	
	join Commodity c on uncomm.CommodityID = c.CommodityID
	join ProjectCode pc on received.ProjectCodeID = pc.ProjectCodeID

END




