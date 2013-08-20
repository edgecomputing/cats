CREATE TABLE [dbo].[StateTemplate] (
    [StateTemplateID]         INT           IDENTITY (1, 1) NOT NULL,
    [ParentProcessTemplateID] INT           NULL,
    [Name]                    NVARCHAR (50) NULL,
    [AllowedAccessLevel]      INT           NULL,
    [StateNo]                 INT           NOT NULL,
    [StateType]               INT           NULL
);

