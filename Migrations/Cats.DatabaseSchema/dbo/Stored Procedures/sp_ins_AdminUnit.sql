

CREATE PROCEDURE [sp_ins_AdminUnit] (
  @Name nvarchar(50),
  @AdminUnitTypeID int,
  @ParentID int
)
AS
  INSERT INTO [dbo].[AdminUnit] (
    [Name],
    [AdminUnitTypeID],
    [ParentID]
  )
  VALUES (
    @Name,
    @AdminUnitTypeID,
    @ParentID
  )