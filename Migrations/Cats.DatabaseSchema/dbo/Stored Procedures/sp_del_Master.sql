

CREATE PROCEDURE [sp_del_Master] (
  @MasterID int
)
AS
  DELETE FROM [dbo].[Master]
  WHERE 
    ([MasterID] = @MasterID)