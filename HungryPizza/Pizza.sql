CREATE TABLE [dbo].[Pizza] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    [Description] NVARCHAR (250)   NULL,
    [Price]       NUMERIC (18, 2)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

