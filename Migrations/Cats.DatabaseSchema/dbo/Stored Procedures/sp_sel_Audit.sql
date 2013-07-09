
CREATE PROCEDURE [sp_sel_Audit]
AS
  SELECT 
    [AuditID],
    [LoginID],
    [DateTime],
    [Action],
    [TableName],
    [PrimaryKey],
    [ColumnName],
    [NewValue],
    [OldValue]
  FROM 
    [dbo].[Audit]