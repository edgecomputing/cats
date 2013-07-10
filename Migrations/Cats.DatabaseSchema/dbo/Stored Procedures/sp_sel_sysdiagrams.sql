
CREATE PROCEDURE [sp_sel_sysdiagrams]
AS
  SELECT 
    [name],
    [principal_id],
    [diagram_id],
    [version],
    [definition]
  FROM 
    [dbo].[sysdiagrams]