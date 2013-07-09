

CREATE PROC [dbo].[RPT_BinCard]
(
  @warehouse int,
  @store int,
  @commodity int,
  @project nvarchar(50)
)
as
BEGIN
    select * from 
    (
        select [Ordering] = 1,tr.Name Transporter, tr.NameAM TransporterAM, r.DriverName,r.PlateNo_Prime, r.ReceiptDate [Date],r.GRN as Identification, d.Name as ToFrom, pc.Value Project,si.Value SINumber, t.QuantityInMT Received, Dispatched = null from Receive r left join Donor d on r.ResponsibleDonorID = d.DonorID join ReceiveDetail rc on r.ReceiveID = rc.ReceiveID join TransactionGroup tg on tg.TransactionGroupID = rc.TransactionGroupID join [Transaction] t on t.TransactionGroupID = tg.TransactionGroupID and t.QuantityInMT >= 0 join ProjectCode pc on t.ProjectCodeID = pc.ProjectCodeID join ShippingInstruction si on si.ShippingInstructionID = t.ShippingInstructionID join Transporter tr on r.TransporterID = tr.TransporterID where t.HubID = @warehouse and t.StoreID = @store and rc.CommodityID in ( select CommodityID from Commodity where ParentID = @commodity or CommodityID = @commodity) and pc.ProjectCodeID = @project
        union
        select [Ordering] = 2,tr.Name Transporter, tr.NameAM TransporterAM, d.DriverName,d.PlateNo_Prime, d.DispatchDate [Date],d.GIN as Identification, f.Name as ToFrom, pc.Value Project,SINumber = null, Received = null, t.QuantityInMT Dispatched   from Dispatch d join DispatchDetail dc on d.DispatchID = dc.DispatchID left join FDP f on  d.FDPID = f.FDPID join TransactionGroup tg on tg.TransactionGroupID = dc.TransactionGroupID join [Transaction] t on t.TransactionGroupID = tg.TransactionGroupID and t.QuantityInMT >= 0 join ProjectCode pc on t.ProjectCodeID = pc.ProjectCodeID join ShippingInstruction si on si.ShippingInstructionID = t.ShippingInstructionID join Transporter tr on d.TransporterID = tr.TransporterID where t.HubID = @warehouse and t.StoreID = @store and pc.ProjectCodeID = @project and t.ParentCommodityID in ( select CommodityID from Commodity where ParentID = @commodity or CommodityID = @commodity)
    ) as Transactions
    order by Transactions.Date asc, Ordering
END


