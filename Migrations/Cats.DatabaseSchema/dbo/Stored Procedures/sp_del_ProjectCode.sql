

CREATE PROCEDURE [sp_del_ProjectCode] (
  @ProjectCodeID int
)
AS
  DELETE FROM [dbo].[ProjectCode]
  WHERE 
    ([ProjectCodeID] = @ProjectCodeID)