

CREATE PROCEDURE [sp_del_Unit] (
  @UnitID int
)
AS
  DELETE FROM [dbo].[Unit]
  WHERE 
    ([UnitID] = @UnitID)