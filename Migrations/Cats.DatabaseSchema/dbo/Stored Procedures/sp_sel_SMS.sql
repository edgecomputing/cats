
CREATE PROCEDURE [sp_sel_SMS]
AS
  SELECT 
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
  FROM 
    [dbo].[SMS]