

CREATE PROCEDURE [sp_del_LetterTemplate] (
  @LetterTemplateID int
)
AS
  DELETE FROM [dbo].[LetterTemplate]
  WHERE 
    ([LetterTemplateID] = @LetterTemplateID)