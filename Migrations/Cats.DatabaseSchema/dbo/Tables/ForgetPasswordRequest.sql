CREATE TABLE [dbo].[ForgetPasswordRequest] (
    [ForgetPasswordRequestID] INT           IDENTITY (1, 1) NOT NULL,
    [UserProfileID]           INT           NOT NULL,
    [GeneratedDate]           DATETIME      NOT NULL,
    [ExpieryDate]             DATETIME      NOT NULL,
    [Completed]               BIT           NOT NULL,
    [RequestKey]              NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ForgetPasswordRequest] PRIMARY KEY CLUSTERED ([ForgetPasswordRequestID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'When a user has forgotten their passoword, they submit a form and it should send for them a password reset link. The link to validate the user''s email creates an entry in this table which is validated first when the user comes back to reset the password.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ForgetPasswordRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key field', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ForgetPasswordRequest', @level2type = N'COLUMN', @level2name = N'ForgetPasswordRequestID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The user''s profile ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ForgetPasswordRequest', @level2type = N'COLUMN', @level2name = N'UserProfileID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'When this email was generated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ForgetPasswordRequest', @level2type = N'COLUMN', @level2name = N'GeneratedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Expiry date at which this forget password link is no more valid. by default this is 24 hours since the reset password message has been sent.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ForgetPasswordRequest', @level2type = N'COLUMN', @level2name = N'ExpieryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag to check if the password was reset using this link or not. this invalidates the Password reset request that is placed in this table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ForgetPasswordRequest', @level2type = N'COLUMN', @level2name = N'Completed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The encrypted key that is used to validate if the user is the one who owns the email address', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ForgetPasswordRequest', @level2type = N'COLUMN', @level2name = N'RequestKey';

