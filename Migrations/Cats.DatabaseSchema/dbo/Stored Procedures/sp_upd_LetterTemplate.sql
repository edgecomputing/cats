

CREATE PROCEDURE [sp_upd_LetterTemplate] (
  @LetterTemplateID int,
  @Name nvarchar(50),
  @Parameters xml,
  @Template ntext
)
AS
  UPDATE [dbo].[LetterTemplate] SET
    [Name] = @Name,
    [Parameters] = @Parameters,
    [Template] = @Template
  WHERE 
    ([LetterTemplateID] = @LetterTemplateID)