CREATE TABLE [dbo].[Log] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Date]      DATETIME       NOT NULL,
    [Thread]    VARCHAR (255)  NOT NULL,
    [Level]     VARCHAR (50)   NOT NULL,
    [Logger]    VARCHAR (255)  NOT NULL,
    [Message]   VARCHAR (4000) NOT NULL,
    [Exception] VARCHAR (2000) NULL,
    [LogUser]   NVARCHAR (50)  NULL
);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[Log] TO PUBLIC
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[Log] TO PUBLIC
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[Log] TO PUBLIC
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[Log] TO PUBLIC
    AS [dbo];

