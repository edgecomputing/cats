
CREATE PROCEDURE [sp_sel_Transporter]
AS
  SELECT 
    [TransporterID],
    [Name],
    [LongName],
    [BiddingSystemID]
  FROM 
    [dbo].[Transporter]