CREATE VIEW dbo.vw_NeedAssessment
AS
SELECT        TOP (100) PERCENT dbo.AdminUnit.Name, dbo.Season.Name AS Season, EarlyWarning.TypeOfNeedAssessment.TypeOfNeedAssessment, 
                         EarlyWarning.NeedAssessment.NeedAApproved, DATEPART(year, EarlyWarning.NeedAssessment.NeedADate) AS Year, 
                         SUM(EarlyWarning.NeedAssessmentDetail.PSNPFromWoredasMale) AS PSNPFromWoredasMale, 
                         SUM(EarlyWarning.NeedAssessmentDetail.PSNPFromWoredasFemale) AS PSNPFromWoredasFemale, 
                         SUM(EarlyWarning.NeedAssessmentDetail.NonPSNPFromWoredasMale) AS NonPSNPFromWoredasMale, 
                         SUM(EarlyWarning.NeedAssessmentDetail.NonPSNPFromWoredasFemale) AS NonPSNPFromWoredasFemale, 
                         SUM(EarlyWarning.NeedAssessmentDetail.PSNPFromWoredasMale + EarlyWarning.NeedAssessmentDetail.PSNPFromWoredasFemale + EarlyWarning.NeedAssessmentDetail.NonPSNPFromWoredasMale
                          + EarlyWarning.NeedAssessmentDetail.NonPSNPFromWoredasFemale) AS TotalBeneficiaries
FROM            EarlyWarning.NeedAssessment INNER JOIN
                         EarlyWarning.NeedAssessmentHeader ON EarlyWarning.NeedAssessment.NeedAID = EarlyWarning.NeedAssessmentHeader.NeedAID INNER JOIN
                         EarlyWarning.NeedAssessmentDetail ON EarlyWarning.NeedAssessmentHeader.NAHeaderId = EarlyWarning.NeedAssessmentDetail.NeedAID INNER JOIN
                         dbo.AdminUnit ON EarlyWarning.NeedAssessment.Region = dbo.AdminUnit.AdminUnitID INNER JOIN
                         dbo.AdminUnitType ON dbo.AdminUnit.AdminUnitTypeID = dbo.AdminUnitType.AdminUnitTypeID AND 
                         dbo.AdminUnit.AdminUnitTypeID = dbo.AdminUnitType.AdminUnitTypeID AND dbo.AdminUnit.AdminUnitTypeID = dbo.AdminUnitType.AdminUnitTypeID INNER JOIN
                         dbo.Season ON EarlyWarning.NeedAssessment.Season = dbo.Season.SeasonID INNER JOIN
                         EarlyWarning.TypeOfNeedAssessment ON 
                         EarlyWarning.NeedAssessment.TypeOfNeedAssessment = EarlyWarning.TypeOfNeedAssessment.TypeOfNeedAssessmentID AND 
                         EarlyWarning.NeedAssessment.TypeOfNeedAssessment = EarlyWarning.TypeOfNeedAssessment.TypeOfNeedAssessmentID AND 
                         EarlyWarning.NeedAssessment.TypeOfNeedAssessment = EarlyWarning.TypeOfNeedAssessment.TypeOfNeedAssessmentID
GROUP BY dbo.AdminUnit.Name, dbo.Season.Name, DATEPART(year, EarlyWarning.NeedAssessment.NeedADate), dbo.AdminUnit.ParentID, 
                         EarlyWarning.NeedAssessment.NeedAApproved, EarlyWarning.TypeOfNeedAssessment.TypeOfNeedAssessment
HAVING        (EarlyWarning.NeedAssessment.NeedAApproved = 1) AND (dbo.AdminUnit.ParentID = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_NeedAssessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'= 462
               Bottom = 445
               Right = 691
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
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 2130
         Width = 1515
         Width = 1500
         Width = 2130
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1590
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 4050
         Alias = 900
         Table = 1485
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
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_NeedAssessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[37] 4[24] 2[11] 3) )"
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
         Begin Table = "NeedAssessment (EarlyWarning)"
            Begin Extent = 
               Top = 16
               Left = 6
               Bottom = 241
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "NeedAssessmentDetail (EarlyWarning)"
            Begin Extent = 
               Top = 4
               Left = 603
               Bottom = 163
               Right = 884
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "NeedAssessmentHeader (EarlyWarning)"
            Begin Extent = 
               Top = 5
               Left = 309
               Bottom = 134
               Right = 479
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdminUnit"
            Begin Extent = 
               Top = 171
               Left = 413
               Bottom = 334
               Right = 597
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdminUnitType"
            Begin Extent = 
               Top = 221
               Left = 700
               Bottom = 385
               Right = 884
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Season"
            Begin Extent = 
               Top = 293
               Left = 240
               Bottom = 395
               Right = 410
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TypeOfNeedAssessment (EarlyWarning)"
            Begin Extent = 
               Top = 333
               Left', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_NeedAssessment';

