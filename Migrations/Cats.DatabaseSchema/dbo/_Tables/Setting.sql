CREATE TABLE [dbo].[Setting] (
    [SettingID] INT           IDENTITY (1, 1) NOT NULL,
    [Category]  VARCHAR (100) NOT NULL,
    [Key]       VARCHAR (100) NOT NULL,
    [Value]     VARCHAR (100) NOT NULL,
    [Option]    VARCHAR (100) NULL,
    [Type]      VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED ([SettingID] ASC)
);

