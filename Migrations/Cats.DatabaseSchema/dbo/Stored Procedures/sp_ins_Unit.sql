

CREATE PROCEDURE [sp_ins_Unit] (
  @Name nvarchar(50)
)
AS
  INSERT INTO [dbo].[Unit] (
    [Name]
  )
  VALUES (
    @Name
  )