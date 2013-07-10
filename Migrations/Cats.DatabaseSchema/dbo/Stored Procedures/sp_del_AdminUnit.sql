

CREATE PROCEDURE [sp_del_AdminUnit] (
  @AdminUnitID int
)
AS
  DELETE FROM [dbo].[AdminUnit]
  WHERE 
    ([AdminUnitID] = @AdminUnitID)