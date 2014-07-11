

CREATE PROCEDURE [sp_upd_AdminUnit] (
  @AdminUnitID int,
  @Name nvarchar(50),
  @AdminUnitTypeID int,
  @ParentID int
)
AS
  UPDATE [dbo].[AdminUnit] SET
    [Name] = @Name,
    [AdminUnitTypeID] = @AdminUnitTypeID,
    [ParentID] = @ParentID
  WHERE 
    ([AdminUnitID] = @AdminUnitID)