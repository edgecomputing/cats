CREATE TABLE [dbo].[ErrorLog] (
    [ErrorLogID]  UNIQUEIDENTIFIER CONSTRAINT [DF_ErrorLog_Id] DEFAULT (newid()) NOT NULL,
    [PartitionID] INT              CONSTRAINT [DF_ErrorLog_PartitionID] DEFAULT ((0)) NOT NULL,
    [Application] NVARCHAR (60)    NOT NULL,
    [Host]        NVARCHAR (50)    NOT NULL,
    [Type]        NVARCHAR (100)   NOT NULL,
    [Source]      NVARCHAR (60)    NOT NULL,
    [Message]     NVARCHAR (500)   NOT NULL,
    [User]        NVARCHAR (50)    NOT NULL,
    [StatusCode]  INT              NOT NULL,
    [TimeUtc]     DATETIME         NOT NULL,
    [Sequence]    INT              IDENTITY (1, 1) NOT NULL,
    [AllXml]      NTEXT            NOT NULL,
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY NONCLUSTERED ([ErrorLogID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_ErrorLog_App_Time_Seq]
    ON [dbo].[ErrorLog]([Application] ASC, [TimeUtc] DESC, [Sequence] DESC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contains an error log. if there is an error on the system, the error log routine will log it with the detail stack trace.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key of the error log.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog', @level2type = N'COLUMN', @level2name = N'ErrorLogID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The application on which this error happened on.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog', @level2type = N'COLUMN', @level2name = N'Application';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Host', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog', @level2type = N'COLUMN', @level2name = N'Host';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The type of error that happened', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The source of the error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Message trapped from the exception handler', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The user who was affected by the Error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog', @level2type = N'COLUMN', @level2name = N'User';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The status of the error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog', @level2type = N'COLUMN', @level2name = N'StatusCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Time at which the error happened.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog', @level2type = N'COLUMN', @level2name = N'TimeUtc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A detail error stack trace that happened.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ErrorLog', @level2type = N'COLUMN', @level2name = N'AllXml';

