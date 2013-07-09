

CREATE PROCEDURE [sp_upd_Unit] (
  @UnitID int,
  @Name nvarchar(50)
)
AS
  UPDATE [dbo].[Unit] SET
    [Name] = @Name
  WHERE 
    ([UnitID] = @UnitID)