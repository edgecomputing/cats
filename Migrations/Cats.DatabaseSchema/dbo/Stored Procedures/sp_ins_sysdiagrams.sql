

CREATE PROCEDURE [sp_ins_sysdiagrams] (
  @name sys.sysname,
  @principal_id int,
  @version int,
  @definition varbinary(max)
)
AS
  INSERT INTO [dbo].[sysdiagrams] (
    [name],
    [principal_id],
    [version],
    [definition]
  )
  VALUES (
    @name,
    @principal_id,
    @version,
    @definition
  )