CREATE TABLE [Localization].[Page] (
    [PageId]  INT            IDENTITY (1, 1) NOT NULL,
    [PageKey] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED ([PageId] ASC),
    CONSTRAINT [UK_Page_PageKey] UNIQUE NONCLUSTERED ([PageId] ASC)
);

