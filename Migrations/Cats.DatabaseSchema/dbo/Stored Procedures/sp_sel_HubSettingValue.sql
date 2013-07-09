
CREATE PROCEDURE [sp_sel_HubSettingValue]
AS
  SELECT 
    [HubSettingValueID],
    [HubSettingID],
    [HubID],
    [Value]
  FROM 
    [dbo].[HubSettingValue]