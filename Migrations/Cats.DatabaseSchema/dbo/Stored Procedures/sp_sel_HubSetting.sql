
CREATE PROCEDURE [sp_sel_HubSetting]
AS
  SELECT 
    [HubSettingID],
    [Key],
    [Name],
    [ValueType],
    [Options]
  FROM 
    [dbo].[HubSetting]