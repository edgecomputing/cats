

CREATE PROCEDURE [sp_del_sysdiagrams] (
  @diagram_id int
)
AS
  DELETE FROM [dbo].[sysdiagrams]
  WHERE 
    ([diagram_id] = @diagram_id)