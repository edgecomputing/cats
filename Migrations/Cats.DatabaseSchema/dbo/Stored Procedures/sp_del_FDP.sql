

CREATE PROCEDURE [sp_del_FDP] (
  @FDPID int
)
AS
  DELETE FROM [dbo].[FDP]
  WHERE 
    ([FDPID] = @FDPID)