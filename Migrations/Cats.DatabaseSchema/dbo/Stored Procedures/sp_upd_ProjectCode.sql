

CREATE PROCEDURE [sp_upd_ProjectCode] (
  @ProjectCodeID int,
  @Value nvarchar(50)
)
AS
  UPDATE [dbo].[ProjectCode] SET
    [Value] = @Value
  WHERE 
    ([ProjectCodeID] = @ProjectCodeID)