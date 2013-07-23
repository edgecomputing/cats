CREATE TABLE [dbo].[netsqlazman_LogTable] (
    [LogId]            INT              IDENTITY (1, 1) NOT NULL,
    [LogDateTime]      DATETIME         NOT NULL,
    [WindowsIdentity]  NVARCHAR (255)   NOT NULL,
    [SqlIdentity]      NVARCHAR (128)   CONSTRAINT [DF_Log_SqlIdentity] DEFAULT (suser_sname()) NULL,
    [MachineName]      NVARCHAR (255)   NOT NULL,
    [InstanceGuid]     UNIQUEIDENTIFIER NOT NULL,
    [TransactionGuid]  UNIQUEIDENTIFIER NULL,
    [OperationCounter] INT              NOT NULL,
    [ENSType]          NVARCHAR (255)   NOT NULL,
    [ENSDescription]   NVARCHAR (4000)  NOT NULL,
    [LogType]          CHAR (1)         NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY NONCLUSTERED ([LogId] ASC),
    CONSTRAINT [CK_Log] CHECK ([LogType]='I' OR [LogType]='W' OR [LogType]='E')
);


GO
CREATE CLUSTERED INDEX [IX_Log_2]
    ON [dbo].[netsqlazman_LogTable]([LogDateTime] DESC, [InstanceGuid] ASC, [OperationCounter] DESC);


GO
CREATE NONCLUSTERED INDEX [IX_Log]
    ON [dbo].[netsqlazman_LogTable]([WindowsIdentity] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Log_1]
    ON [dbo].[netsqlazman_LogTable]([SqlIdentity] ASC);

