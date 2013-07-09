CREATE VIEW dbo.VWRecieving
AS
SELECT        dbo.Receive.ReceiptDate, dbo.Hub.Name AS HubName, dbo.ReceiveDetail.SentQuantityInUnit, dbo.ReceiveDetail.UnitID, dbo.Unit.Name AS UnitName, 
                         dbo.Commodity.Name AS ComodityName, dbo.Donor.Name AS DonorName
FROM            dbo.Receive INNER JOIN
                         dbo.ReceiveDetail ON dbo.Receive.ReceiveID = dbo.ReceiveDetail.ReceiveID INNER JOIN
                         dbo.Hub ON dbo.Receive.HubID = dbo.Hub.HubID INNER JOIN
                         dbo.Donor ON dbo.Receive.SourceDonorID = dbo.Donor.DonorID AND dbo.Receive.ResponsibleDonorID = dbo.Donor.DonorID INNER JOIN
                         dbo.Commodity ON dbo.ReceiveDetail.CommodityID = dbo.Commodity.CommodityID INNER JOIN
                         dbo.Unit ON dbo.ReceiveDetail.UnitID = dbo.Unit.UnitID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWRecieving';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Width = 1500
         Width = 1500
         Width = 1500
         Width = 3660
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
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWRecieving';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[47] 4[24] 2[8] 3) )"
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
         Begin Table = "Receive"
            Begin Extent = 
               Top = 25
               Left = 482
               Bottom = 224
               Right = 791
            End
            DisplayFlags = 280
            TopColumn = 13
         End
         Begin Table = "ReceiveDetail"
            Begin Extent = 
               Top = 118
               Left = 19
               Bottom = 247
               Right = 214
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Hub"
            Begin Extent = 
               Top = 1
               Left = 28
               Bottom = 113
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Donor"
            Begin Extent = 
               Top = 151
               Left = 874
               Bottom = 280
               Right = 1067
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Commodity"
            Begin Extent = 
               Top = 8
               Left = 832
               Bottom = 137
               Right = 1022
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Unit"
            Begin Extent = 
               Top = 31
               Left = 286
               Bottom = 126
               Right = 456
            End
            DisplayFlags = 280
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
         Width = 2145
         Width = 2355', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWRecieving';

