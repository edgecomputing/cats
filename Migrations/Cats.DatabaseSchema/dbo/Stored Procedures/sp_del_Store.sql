

CREATE PROCEDURE [sp_del_Store] (
  @StoreID int
)
AS
  DELETE FROM [dbo].[Store]
  WHERE 
    ([StoreID] = @StoreID)