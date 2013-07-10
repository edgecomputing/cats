
CREATE PROCEDURE [sp_sel_Donor]
AS
  SELECT 
    [DonorID],
    [Name],
    [IsResponsibleDonor],
    [IsSourceDonor],
    [LongName]
  FROM 
    [dbo].[Donor]