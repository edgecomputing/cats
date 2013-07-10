

CREATE PROCEDURE [sp_del_Audit] (
  @AuditID int
)
AS
  DELETE FROM [dbo].[Audit]
  WHERE 
    ([AuditID] = @AuditID)