

CREATE PROCEDURE [sp_upd_FDP] (
  @FDPID int,
  @Name nvarchar(50),
  @AdminUnitID int
)
AS
  UPDATE [dbo].[FDP] SET
    [Name] = @Name,
    [AdminUnitID] = @AdminUnitID
  WHERE 
    ([FDPID] = @FDPID)