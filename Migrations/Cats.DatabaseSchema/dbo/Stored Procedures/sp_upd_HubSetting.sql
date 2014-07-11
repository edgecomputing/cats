

CREATE PROCEDURE [sp_upd_HubSetting] (
  @HubSettingID int,
  @Key nvarchar(50),
  @Name nvarchar(50),
  @ValueType nvarchar(50),
  @Options nvarchar(500)
)
AS
  UPDATE [dbo].[HubSetting] SET
    [Key] = @Key,
    [Name] = @Name,
    [ValueType] = @ValueType,
    [Options] = @Options
  WHERE 
    ([HubSettingID] = @HubSettingID)