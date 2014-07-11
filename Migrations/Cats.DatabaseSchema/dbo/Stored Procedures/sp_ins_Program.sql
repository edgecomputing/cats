

CREATE PROCEDURE [sp_ins_Program] (
  @Name nvarchar(50),
  @Description nvarchar(50),
  @LongName nvarchar(500)
)
AS
  INSERT INTO [dbo].[Program] (
    [Name],
    [Description],
    [LongName]
  )
  VALUES (
    @Name,
    @Description,
    @LongName
  )