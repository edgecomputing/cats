

CREATE PROCEDURE [sp_ins_LetterTemplate] (
  @Name nvarchar(50),
  @Parameters xml,
  @Template ntext
)
AS
  INSERT INTO [dbo].[LetterTemplate] (
    [Name],
    [Parameters],
    [Template]
  )
  VALUES (
    @Name,
    @Parameters,
    @Template
  )