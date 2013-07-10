

CREATE PROCEDURE [sp_del_AdminUnitType] (
  @AdminUnitTypeID int
)
AS
  DELETE FROM [dbo].[AdminUnitType]
  WHERE 
    ([AdminUnitTypeID] = @AdminUnitTypeID)