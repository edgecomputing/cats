
CREATE PROCEDURE [sp_sel_Detail]
AS
  SELECT 
    [DetailID],
    [Name],
    [Description],
    [MasterID],
    [SortOrder]
  FROM 
    [dbo].[Detail]