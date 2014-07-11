

CREATE PROCEDURE [sp_upd_Contact] (
  @ContactID int,
  @FirstName nvarchar(350),
  @LastName nvarchar(350),
  @PhoneNo nvarchar(10),
  @FDPID int
)
AS
  UPDATE [dbo].[Contact] SET
    [FirstName] = @FirstName,
    [LastName] = @LastName,
    [PhoneNo] = @PhoneNo,
    [FDPID] = @FDPID
  WHERE 
    ([ContactID] = @ContactID)