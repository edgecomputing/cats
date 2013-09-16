CREATE TABLE [dbo].[Contact] (
    [ContactID] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (350) NOT NULL,
    [LastName]  NVARCHAR (350) NOT NULL,
    [PhoneNo]   NVARCHAR (10)  NOT NULL,
    [FDPID]     INT            NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([ContactID] ASC),
    CONSTRAINT [FK_Contact_FDP] FOREIGN KEY ([FDPID]) REFERENCES [dbo].[FDP] ([FDPID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contact addresses of FDPs. The main purpose of this database was to send out notifications of respective dispatches for the FDP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contact';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prmiary key of FDP Contacts', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'First name of the contact person at a given FDP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'FirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Last name of the contact person at a given FDP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'LastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mobile phone of the FDP contact person.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'PhoneNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ID of the FDP for which we have a contact information.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'FDPID';

