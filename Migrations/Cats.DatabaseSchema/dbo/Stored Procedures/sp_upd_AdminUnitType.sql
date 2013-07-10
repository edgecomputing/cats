

CREATE PROCEDURE [sp_upd_AdminUnitType] (
  @AdminUnitTypeID int,
  @Name nvarchar(50),
  @SortOrder int
)
AS
  UPDATE [dbo].[AdminUnitType] SET
    [Name] = @Name,
    [SortOrder] = @SortOrder
  WHERE 
    ([AdminUnitTypeID] = @AdminUnitTypeID)