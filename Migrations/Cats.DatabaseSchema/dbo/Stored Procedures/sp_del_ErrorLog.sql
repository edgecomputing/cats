

CREATE PROCEDURE [sp_del_ErrorLog] (
  @ErrorLogID uniqueidentifier
)
AS
  DELETE FROM [dbo].[ErrorLog]
  WHERE 
    ([ErrorLogID] = @ErrorLogID)