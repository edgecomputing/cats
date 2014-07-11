

CREATE PROCEDURE [sp_del_HubSetting] (
  @HubSettingID int
)
AS
  DELETE FROM [dbo].[HubSetting]
  WHERE 
    ([HubSettingID] = @HubSettingID)