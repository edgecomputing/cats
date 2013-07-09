
CREATE proc [dbo].[RPT_MonthlyGiftSummaryETA]
as
Begin
	select * from 
(
SELECT Year(ETA) Year, Month(ETA) Month,  SUBSTRING (DATENAME(Month, (ETA)),0, 4)  + ' ' + CAST( Year(ETA) as nvarchar) MonthName, case when c.ParentID is null then c.CommodityID else c.ParentID end as CommodityID, SUM(WeightInMT) Weight 
		from GiftCertificate g join GiftCertificateDetail d on g.GiftCertificateID = d.GiftCertificateID join Commodity c on d.CommodityID = c.CommodityID 
		group by SUBSTRING(DATENAME(Month, (ETA)),0 , 4) + ' ' + cast( Year(ETA) as nvarchar) ,case when c.ParentID is null then c.CommodityID else c.ParentID end, Year(ETA), MONTH(ETA) 
		
	) as PivotData
	PIVOT
	(
	Sum(Weight)
	For CommodityID in 	
		 ( [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12] )
		 ) as PivotTable
		Order By Year,Month
END

