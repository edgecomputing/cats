

CREATE PROCEDURE [sp_ins_UserRole] (
  @UserProfileID int,
  @RoleID int
)
AS
  INSERT INTO [dbo].[UserRole] (
    [UserProfileID],
    [RoleID]
  )
  VALUES (
    @UserProfileID,
    @RoleID
  )