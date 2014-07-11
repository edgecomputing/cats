

CREATE PROCEDURE [sp_ins_LedgerType] (
  @Name nvarchar(50),
  @Direction char
)
AS
  INSERT INTO [dbo].[LedgerType] (
    [Name],
    [Direction]
  )
  VALUES (
    @Name,
    @Direction
  )