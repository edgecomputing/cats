CREATE VIEW dbo.RRDInfo
AS
SELECT     dbo.DistributionPlanDetail.DistributionPlanDetailID, dbo.DistributionPlanDetail.RRDNo, dbo.DistributionPlan.Year, dbo.DistributionPlan.Month, 
                      dbo.DistributionPlan.Round, dbo.AdminUnit.Name
FROM         dbo.RRDStatus INNER JOIN
                      dbo.DistributionPlanDetail ON dbo.RRDStatus.RRDStatusID = dbo.DistributionPlanDetail.RRDStatusID FULL OUTER JOIN
                      dbo.DistributionPlan INNER JOIN
                      dbo.AdminUnit ON dbo.DistributionPlan.Region = dbo.AdminUnit.AdminUnitID ON 
                      dbo.DistributionPlanDetail.DistributionPlanID = dbo.DistributionPlan.DistributionPlanID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RRDInfo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RRDInfo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[50] 4[22] 2[9] 3) )"
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
         Begin Table = "RRDStatus"
            Begin Extent = 
               Top = 118
               Left = 0
               Bottom = 207
               Right = 160
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DistributionPlanDetail"
            Begin Extent = 
               Top = 0
               Left = 106
               Bottom = 230
               Right = 307
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DistributionPlan"
            Begin Extent = 
               Top = 102
               Left = 368
               Bottom = 345
               Right = 542
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "AdminUnit"
            Begin Extent = 
               Top = 29
               Left = 499
               Bottom = 173
               Right = 671
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
         Or = 1350', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RRDInfo';

