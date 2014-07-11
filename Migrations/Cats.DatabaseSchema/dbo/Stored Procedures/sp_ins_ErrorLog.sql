

CREATE PROCEDURE [sp_ins_ErrorLog] (
  @ErrorLogID uniqueidentifier,
  @Application nvarchar(60),
  @Host nvarchar(50),
  @Type nvarchar(100),
  @Source nvarchar(60),
  @Message nvarchar(500),
  @User nvarchar(50),
  @StatusCode int,
  @TimeUtc datetime,
  @Sequence int,
  @AllXml ntext
)
AS
  INSERT INTO [dbo].[ErrorLog] (
    [ErrorLogID],
    [Application],
    [Host],
    [Type],
    [Source],
    [Message],
    [User],
    [StatusCode],
    [TimeUtc],
    [Sequence],
    [AllXml]
  )
  VALUES (
    @ErrorLogID,
    @Application,
    @Host,
    @Type,
    @Source,
    @Message,
    @User,
    @StatusCode,
    @TimeUtc,
    @Sequence,
    @AllXml
  )