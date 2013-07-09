
CREATE PROCEDURE [sp_sel_ErrorLog]
AS
  SELECT 
    [ErrorLogID],
    [Application],
    [Host],
    [Type],
    [Source],
    [Message],
    [User],
    [StatusCode],
    [TimeUtc],
    [Sequence],
    [AllXml]
  FROM 
    [dbo].[ErrorLog]