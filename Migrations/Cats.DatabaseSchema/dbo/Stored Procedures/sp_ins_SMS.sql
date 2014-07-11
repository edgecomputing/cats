

CREATE PROCEDURE [sp_ins_SMS] (
  @SMSID int,
  @InOutInd char,
  @MobileNumber varchar(30),
  @Text varchar(500),
  @RequestDate datetime,
  @SendAfterDate datetime,
  @QueuedDate datetime,
  @SentDate datetime,
  @Status varchar(10),
  @StatusDate datetime,
  @Attempts int,
  @LastAttemptDate datetime,
  @EventTag varchar(30)
)
AS
  INSERT INTO [dbo].[SMS] (
    [SMSID],
    [InOutInd],
    [MobileNumber],
    [Text],
    [RequestDate],
    [SendAfterDate],
    [QueuedDate],
    [SentDate],
    [Status],
    [StatusDate],
    [Attempts],
    [LastAttemptDate],
    [EventTag]
  )
  VALUES (
    @SMSID,
    @InOutInd,
    @MobileNumber,
    @Text,
    @RequestDate,
    @SendAfterDate,
    @QueuedDate,
    @SentDate,
    @Status,
    @StatusDate,
    @Attempts,
    @LastAttemptDate,
    @EventTag
  )