

CREATE PROCEDURE [sp_del_Transporter] (
  @TransporterID int
)
AS
  DELETE FROM [dbo].[Transporter]
  WHERE 
    ([TransporterID] = @TransporterID)