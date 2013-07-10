

CREATE PROCEDURE [sp_ins_FDP] (
  @Name nvarchar(50),
  @AdminUnitID int
)
AS
  INSERT INTO [dbo].[FDP] (
    [Name],
    [AdminUnitID]
  )
  VALUES (
    @Name,
    @AdminUnitID
  )