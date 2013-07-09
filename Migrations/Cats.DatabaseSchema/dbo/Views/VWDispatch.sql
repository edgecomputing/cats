CREATE VIEW dbo.VWDispatch
AS
SELECT        dbo.Dispatch.DispatchDate, dbo.Hub.Name AS HubName, dbo.FDP.Name AS FDPName, dbo.Commodity.Name AS CommodityName, dbo.DispatchAllocation.Amount, 
                         dbo.Unit.Name AS UnitName, dbo.DispatchAllocation.ShippingInstructionID, dbo.DispatchAllocation.ProjectCodeID, dbo.Dispatch.HubID
FROM            dbo.Dispatch INNER JOIN
                         dbo.DispatchDetail ON dbo.Dispatch.DispatchID = dbo.DispatchDetail.DispatchID INNER JOIN
                         dbo.Hub ON dbo.Dispatch.HubID = dbo.Hub.HubID INNER JOIN
                         dbo.Unit ON dbo.DispatchDetail.UnitID = dbo.Unit.UnitID INNER JOIN
                         dbo.Commodity ON dbo.DispatchDetail.CommodityID = dbo.Commodity.CommodityID INNER JOIN
                         dbo.DispatchAllocation ON dbo.Dispatch.DispatchAllocationID = dbo.DispatchAllocation.DispatchAllocationID AND dbo.Hub.HubID = dbo.DispatchAllocation.HubID AND
                          dbo.Commodity.CommodityID = dbo.DispatchAllocation.CommodityID AND dbo.Unit.UnitID = dbo.DispatchAllocation.Unit INNER JOIN
                         dbo.FDP ON dbo.Dispatch.FDPID = dbo.FDP.FDPID AND dbo.DispatchAllocation.FDPID = dbo.FDP.FDPID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWDispatch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'gs = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
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
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWDispatch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[59] 4[27] 2[4] 3) )"
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
         Begin Table = "Dispatch"
            Begin Extent = 
               Top = 0
               Left = 415
               Bottom = 194
               Right = 648
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DispatchDetail"
            Begin Extent = 
               Top = 2
               Left = 33
               Bottom = 205
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Hub"
            Begin Extent = 
               Top = 130
               Left = 823
               Bottom = 242
               Right = 993
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Unit"
            Begin Extent = 
               Top = 265
               Left = 399
               Bottom = 360
               Right = 569
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Commodity"
            Begin Extent = 
               Top = 224
               Left = 562
               Bottom = 353
               Right = 752
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DispatchAllocation"
            Begin Extent = 
               Top = 210
               Left = 38
               Bottom = 368
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 17
         End
         Begin Table = "FDP"
            Begin Extent = 
               Top = 6
               Left = 686
               Bottom = 135
               Right = 856
            End
            DisplayFla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWDispatch';

