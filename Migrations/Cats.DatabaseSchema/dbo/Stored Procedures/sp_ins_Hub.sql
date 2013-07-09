

CREATE PROCEDURE [sp_ins_Hub] (
  @Name nvarchar(50),
  @HubOwnerID int
)
AS
  INSERT INTO [dbo].[Hub] (
    [Name],
    [HubOwnerID]
  )
  VALUES (
    @Name,
    @HubOwnerID
  )