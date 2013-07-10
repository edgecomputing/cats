
CREATE PROCEDURE [sp_sel_LetterTemplate]
AS
  SELECT 
    [LetterTemplateID],
    [Name],
    [Parameters],
    [Template]
  FROM 
    [dbo].[LetterTemplate]