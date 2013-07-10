

CREATE PROCEDURE [sp_ins_HubSettingValue] (
  @HubSettingID int,
  @HubID int,
  @Value nvarchar(4000)
)
AS
  INSERT INTO [dbo].[HubSettingValue] (
    [HubSettingID],
    [HubID],
    [Value]
  )
  VALUES (
    @HubSettingID,
    @HubID,
    @Value
  )