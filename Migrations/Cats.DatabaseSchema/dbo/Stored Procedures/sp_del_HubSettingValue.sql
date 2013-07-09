

CREATE PROCEDURE [sp_del_HubSettingValue] (
  @HubSettingValueID int
)
AS
  DELETE FROM [dbo].[HubSettingValue]
  WHERE 
    ([HubSettingValueID] = @HubSettingValueID)