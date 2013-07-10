

CREATE PROCEDURE [sp_upd_HubSettingValue] (
  @HubSettingValueID int,
  @HubSettingID int,
  @HubID int,
  @Value nvarchar(4000)
)
AS
  UPDATE [dbo].[HubSettingValue] SET
    [HubSettingID] = @HubSettingID,
    [HubID] = @HubID,
    [Value] = @Value
  WHERE 
    ([HubSettingValueID] = @HubSettingValueID)