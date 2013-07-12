CREATE VIEW dbo.vwTransportOrder
AS
SELECT     Procurement.TransportOrder.*, Procurement.TransportOrderDetail.TransportOrderDetailID, Procurement.TransportOrderDetail.FdpID, 
                      Procurement.TransportOrderDetail.SourceWarehouseID, Procurement.TransportOrderDetail.QuantityQtl, Procurement.TransportOrderDetail.DistanceFromOrigin, 
                      Procurement.TransportOrderDetail.TariffPerQtl, Procurement.TransportOrderDetail.RequisitionID, Procurement.TransportOrderDetail.CommodityID, 
                      Procurement.TransportOrderDetail.ZoneID, Procurement.TransportOrderDetail.DonorID, dbo.FDP.Name AS FDPName, dbo.Hub.Name AS HubName, 
                      EarlyWarning.ReliefRequisition.RequisitionNo, dbo.Commodity.Name AS CommodityName, dbo.Donor.Name AS DonorName, dbo.AdminUnit.Name AS WoredaName, 
                      AdminUnit_1.Name AS ZoneName, Procurement.Transporter.Name AS TransporterName
FROM         Procurement.TransportOrder INNER JOIN
                      Procurement.TransportOrderDetail ON Procurement.TransportOrder.TransportOrderID = Procurement.TransportOrderDetail.TransportOrderID INNER JOIN
                      dbo.FDP ON Procurement.TransportOrderDetail.FdpID = dbo.FDP.FDPID INNER JOIN
                      dbo.Hub ON Procurement.TransportOrderDetail.SourceWarehouseID = dbo.Hub.HubID INNER JOIN
                      EarlyWarning.ReliefRequisition ON Procurement.TransportOrderDetail.RequisitionID = EarlyWarning.ReliefRequisition.RequisitionID INNER JOIN
                      dbo.Commodity ON Procurement.TransportOrderDetail.CommodityID = dbo.Commodity.CommodityID INNER JOIN
                      dbo.AdminUnit ON dbo.FDP.AdminUnitID = dbo.AdminUnit.AdminUnitID INNER JOIN
                      dbo.AdminUnit AS AdminUnit_1 ON dbo.AdminUnit.ParentID = AdminUnit_1.AdminUnitID AND dbo.AdminUnit.ParentID = AdminUnit_1.AdminUnitID AND 
                      dbo.AdminUnit.ParentID = AdminUnit_1.AdminUnitID AND dbo.AdminUnit.ParentID = AdminUnit_1.AdminUnitID INNER JOIN
                      Procurement.Transporter ON Procurement.TransportOrder.TransporterID = Procurement.Transporter.TransporterID AND 
                      Procurement.TransportOrder.TransporterID = Procurement.Transporter.TransporterID AND 
                      Procurement.TransportOrder.TransporterID = Procurement.Transporter.TransporterID AND 
                      Procurement.TransportOrder.TransporterID = Procurement.Transporter.TransporterID AND 
                      Procurement.TransportOrder.TransporterID = Procurement.Transporter.TransporterID AND 
                      Procurement.TransportOrder.TransporterID = Procurement.Transporter.TransporterID AND 
                      Procurement.TransportOrder.TransporterID = Procurement.Transporter.TransporterID AND 
                      Procurement.TransportOrder.TransporterID = Procurement.Transporter.TransporterID LEFT OUTER JOIN
                      dbo.Donor ON Procurement.TransportOrderDetail.DonorID = dbo.Donor.DonorID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwTransportOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdminUnit"
            Begin Extent = 
               Top = 337
               Left = 342
               Bottom = 456
               Right = 514
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "AdminUnit_1"
            Begin Extent = 
               Top = 338
               Left = 341
               Bottom = 457
               Right = 513
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Transporter (Procurement)"
            Begin Extent = 
               Top = 16
               Left = 931
               Bottom = 288
               Right = 1129
            End
            DisplayFlags = 280
            TopColumn = 11
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 32
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwTransportOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[63] 4[14] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TransportOrder (Procurement)"
            Begin Extent = 
               Top = 3
               Left = 665
               Bottom = 278
               Right = 888
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TransportOrderDetail (Procurement)"
            Begin Extent = 
               Top = 9
               Left = 351
               Bottom = 292
               Right = 634
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "FDP"
            Begin Extent = 
               Top = 339
               Left = 124
               Bottom = 458
               Right = 284
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Hub"
            Begin Extent = 
               Top = 339
               Left = 204
               Bottom = 443
               Right = 364
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ReliefRequisition (EarlyWarning)"
            Begin Extent = 
               Top = 334
               Left = 116
               Bottom = 612
               Right = 336
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Commodity"
            Begin Extent = 
               Top = 334
               Left = 110
               Bottom = 453
               Right = 287
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Donor"
            Begin Extent = 
               Top = 341
               Left = 212
               Bottom = 460', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwTransportOrder';

