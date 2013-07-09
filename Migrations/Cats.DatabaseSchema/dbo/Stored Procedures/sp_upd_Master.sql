

CREATE PROCEDURE [sp_upd_Master] (
  @MasterID int,
  @Name nvarchar(50),
  @SortOrder int
)
AS
  UPDATE [dbo].[Master] SET
    [Name] = @Name,
    [SortOrder] = @SortOrder
  WHERE 
    ([MasterID] = @MasterID)