

CREATE PROCEDURE [sp_upd_Setting] (
  @SettingID int,
  @Category varchar(100),
  @Key varchar(100),
  @Value varchar(100),
  @Option varchar(100),
  @Type varchar(100)
)
AS
  UPDATE [dbo].[Setting] SET
    [Category] = @Category,
    [Key] = @Key,
    [Value] = @Value,
    [Option] = @Option,
    [Type] = @Type
  WHERE 
    ([SettingID] = @SettingID)