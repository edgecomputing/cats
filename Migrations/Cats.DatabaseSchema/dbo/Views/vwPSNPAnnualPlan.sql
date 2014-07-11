CREATE VIEW dbo.vwPSNPAnnualPlan
AS
SELECT     dbo.RegionalPSNPPlanDetail.FoodRatio, dbo.RegionalPSNPPlanDetail.CashRatio, dbo.RegionalPSNPPlanDetail.BeneficiaryCount, 
                      dbo.RegionalPSNPPlanDetail.PlanedFDPID, dbo.RegionalPSNPPlan.Duration, dbo.RegionalPSNPPlan.RegionID, dbo.RegionalPSNPPlan.Year, 
                      dbo.RegionalPSNPPlan.RegionalPSNPPlanID, AdminUnit_1.Name AS RegionName, dbo.FDP.Name AS FDPName, dbo.AdminUnit.Name AS WoredaName, 
                      dbo.AdminUnit.AdminUnitID AS WoredaID, AdminUnit_2.AdminUnitID AS ZoneID, AdminUnit_2.Name AS ZoneName
FROM         dbo.FDP INNER JOIN
                      dbo.AdminUnit ON dbo.FDP.AdminUnitID = dbo.AdminUnit.AdminUnitID INNER JOIN
                      dbo.RegionalPSNPPlan INNER JOIN
                      dbo.RegionalPSNPPlanDetail ON dbo.RegionalPSNPPlan.RegionalPSNPPlanID = dbo.RegionalPSNPPlanDetail.RegionalPSNPPlanID ON 
                      dbo.FDP.FDPID = dbo.RegionalPSNPPlanDetail.PlanedFDPID INNER JOIN
                      dbo.AdminUnit AS AdminUnit_1 ON dbo.RegionalPSNPPlan.RegionID = AdminUnit_1.AdminUnitID INNER JOIN
                      dbo.AdminUnit AS AdminUnit_2 ON dbo.AdminUnit.ParentID = AdminUnit_2.AdminUnitID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwPSNPAnnualPlan';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Width = 1500
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
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwPSNPAnnualPlan';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[56] 4[17] 2[9] 3) )"
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
         Begin Table = "RegionalPSNPPlan"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 249
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RegionalPSNPPlanDetail"
            Begin Extent = 
               Top = 14
               Left = 322
               Bottom = 227
               Right = 506
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdminUnit"
            Begin Extent = 
               Top = 16
               Left = 728
               Bottom = 166
               Right = 865
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "FDP"
            Begin Extent = 
               Top = 17
               Left = 547
               Bottom = 136
               Right = 707
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdminUnit_1"
            Begin Extent = 
               Top = 270
               Left = 266
               Bottom = 389
               Right = 438
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdminUnit_2"
            Begin Extent = 
               Top = 158
               Left = 533
               Bottom = 277
               Right = 705
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
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwPSNPAnnualPlan';

