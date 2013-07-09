
CREATE PROCEDURE [sp_sel_Setting]
AS
  SELECT 
    [SettingID],
    [Category],
    [Key],
    [Value],
    [Option],
    [Type]
  FROM 
    [dbo].[Setting]