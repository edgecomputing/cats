

CREATE PROCEDURE [sp_del_Setting] (
  @SettingID int
)
AS
  DELETE FROM [dbo].[Setting]
  WHERE 
    ([SettingID] = @SettingID)