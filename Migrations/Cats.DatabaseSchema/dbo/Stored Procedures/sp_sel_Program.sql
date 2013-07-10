
CREATE PROCEDURE [sp_sel_Program]
AS
  SELECT 
    [ProgramID],
    [Name],
    [Description],
    [LongName]
  FROM 
    [dbo].[Program]