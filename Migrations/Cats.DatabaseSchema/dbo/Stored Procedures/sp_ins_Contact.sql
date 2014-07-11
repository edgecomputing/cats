

CREATE PROCEDURE [sp_ins_Contact] (
  @FirstName nvarchar(350),
  @LastName nvarchar(350),
  @PhoneNo nvarchar(10),
  @FDPID int
)
AS
  INSERT INTO [dbo].[Contact] (
    [FirstName],
    [LastName],
    [PhoneNo],
    [FDPID]
  )
  VALUES (
    @FirstName,
    @LastName,
    @PhoneNo,
    @FDPID
  )