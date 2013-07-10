
CREATE PROCEDURE [sp_sel_UserHub]
AS
  SELECT 
    [UserHubID],
    [UserProfileID],
    [HubID],
    [IsDefault]
  FROM 
    [dbo].[UserHub]