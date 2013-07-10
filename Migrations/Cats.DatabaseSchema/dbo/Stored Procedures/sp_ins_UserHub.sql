

CREATE PROCEDURE [sp_ins_UserHub] (
  @UserProfileID int,
  @HubID int,
  @IsDefault char
)
AS
  INSERT INTO [dbo].[UserHub] (
    [UserProfileID],
    [HubID],
    [IsDefault]
  )
  VALUES (
    @UserProfileID,
    @HubID,
    @IsDefault
  )