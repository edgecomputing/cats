
CREATE PROCEDURE [sp_sel_Contact]
AS
  SELECT 
    [ContactID],
    [FirstName],
    [LastName],
    [PhoneNo],
    [FDPID]
  FROM 
    [dbo].[Contact]