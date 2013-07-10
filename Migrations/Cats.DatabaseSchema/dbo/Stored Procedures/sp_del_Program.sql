

CREATE PROCEDURE [sp_del_Program] (
  @ProgramID int
)
AS
  DELETE FROM [dbo].[Program]
  WHERE 
    ([ProgramID] = @ProgramID)