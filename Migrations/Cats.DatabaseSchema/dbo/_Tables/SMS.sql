CREATE TABLE [dbo].[SMS] (
    [SMSID]           INT           IDENTITY (1, 1) NOT NULL,
    [InOutInd]        CHAR (1)      NOT NULL,
    [MobileNumber]    VARCHAR (30)  NOT NULL,
    [Text]            VARCHAR (500) NOT NULL,
    [RequestDate]     DATETIME      NULL,
    [SendAfterDate]   DATETIME      NULL,
    [QueuedDate]      DATETIME      NULL,
    [SentDate]        DATETIME      NULL,
    [Status]          VARCHAR (10)  NOT NULL,
    [StatusDate]      DATETIME      NOT NULL,
    [Attempts]        INT           NOT NULL,
    [LastAttemptDate] DATETIME      NULL,
    [EventTag]        VARCHAR (30)  NULL
);

