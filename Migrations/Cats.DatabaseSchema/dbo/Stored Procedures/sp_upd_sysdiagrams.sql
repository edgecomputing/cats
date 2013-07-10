

CREATE PROCEDURE [sp_upd_sysdiagrams] (
  @name sys.sysname,
  @principal_id int,
  @diagram_id int,
  @version int,
  @definition varbinary(max)
)
AS
  UPDATE [dbo].[sysdiagrams] SET
    [name] = @name,
    [principal_id] = @principal_id,
    [version] = @version,
    [definition] = @definition
  WHERE 
    ([diagram_id] = @diagram_id)