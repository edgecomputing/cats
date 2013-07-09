

CREATE PROCEDURE [sp_upd_Program] (
  @ProgramID int,
  @Name nvarchar(50),
  @Description nvarchar(50),
  @LongName nvarchar(500)
)
AS
  UPDATE [dbo].[Program] SET
    [Name] = @Name,
    [Description] = @Description,
    [LongName] = @LongName
  WHERE 
    ([ProgramID] = @ProgramID)