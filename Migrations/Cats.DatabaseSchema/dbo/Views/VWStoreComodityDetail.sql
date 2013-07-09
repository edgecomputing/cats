CREATE VIEW dbo.VWStoreComodityDetail
AS
SELECT        dbo.VWStoreCommodity.LedgerName, dbo.VWStoreCommodity.LedgerID, dbo.VWStoreCommodity.HubID, dbo.Hub.Name AS HubName, 
                         dbo.VWStoreCommodity.StoreID, dbo.Store.Name AS StoreName, dbo.VWStoreCommodity.ProjectCodeID, dbo.ProjectCode.Value AS ProjectCodeValue, 
                         dbo.VWStoreCommodity.ShippingInstructionID, dbo.ShippingInstruction.Value AS ShippingInstructionValue, dbo.VWStoreCommodity.ProgramID, 
                         dbo.VWStoreCommodity.CommodityID, dbo.Commodity.Name AS ComodityName, dbo.VWStoreCommodity.QuantityInMT
FROM            dbo.VWStoreCommodity INNER JOIN
                         dbo.Hub ON dbo.VWStoreCommodity.HubID = dbo.Hub.HubID INNER JOIN
                         dbo.Store ON dbo.Hub.HubID = dbo.Store.HubID AND dbo.VWStoreCommodity.StoreID = dbo.Store.StoreID INNER JOIN
                         dbo.Commodity ON dbo.VWStoreCommodity.CommodityID = dbo.Commodity.CommodityID INNER JOIN
                         dbo.ShippingInstruction ON dbo.VWStoreCommodity.ShippingInstructionID = dbo.ShippingInstruction.ShippingInstructionID INNER JOIN
                         dbo.ProjectCode ON dbo.VWStoreCommodity.ProjectCodeID = dbo.ProjectCode.ProjectCodeID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWStoreComodityDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'idth = 1500
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
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWStoreComodityDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[60] 4[0] 2[4] 3) )"
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
         Begin Table = "VWStoreCommodity"
            Begin Extent = 
               Top = 55
               Left = 398
               Bottom = 294
               Right = 670
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Hub"
            Begin Extent = 
               Top = 122
               Left = 705
               Bottom = 234
               Right = 875
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Store"
            Begin Extent = 
               Top = 19
               Left = 906
               Bottom = 148
               Right = 1078
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Commodity"
            Begin Extent = 
               Top = 176
               Left = 42
               Bottom = 316
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ShippingInstruction"
            Begin Extent = 
               Top = 89
               Left = 41
               Bottom = 181
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ProjectCode"
            Begin Extent = 
               Top = 0
               Left = 46
               Bottom = 92
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 15
         Width = 284
         Width = 1500
         W', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWStoreComodityDetail';

