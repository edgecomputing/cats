CREATE TABLE [dbo].[VersionInfo] (
    [Version]   BIGINT   NOT NULL,
    [AppliedOn] DATETIME NULL
);


GO
CREATE UNIQUE CLUSTERED INDEX [UC_Version]
    ON [dbo].[VersionInfo]([Version] ASC);

