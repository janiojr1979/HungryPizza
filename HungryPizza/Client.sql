CREATE TABLE [dbo].[Client] (
    [Id]      UNIQUEIDENTIFIER NOT NULL,
    [Name]    NVARCHAR (250)   NOT NULL,
    [Address] NVARCHAR (120)   NOT NULL,
    [ZipCode] NVARCHAR (20)    NOT NULL,
    [State]   NVARCHAR (50)    NOT NULL,
    [City]    NVARCHAR (50)    NOT NULL,
    [Email]   NVARCHAR (120)   NOT NULL,
    [Phone]   NVARCHAR (50)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Client_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);