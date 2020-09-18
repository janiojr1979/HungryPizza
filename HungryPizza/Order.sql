CREATE TABLE [dbo].[Order] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [ClientId]   UNIQUEIDENTIFIER NULL,
    [ClientName] NVARCHAR (120)   NULL,
    [Email]      NVARCHAR (120)   NULL,
    [Address]    NVARCHAR (120)   NULL,
    [ZipCode]    NVARCHAR (20)    NULL,
    [State]      NVARCHAR (50)    NULL,
    [City]       NVARCHAR (50)    NULL,
    [Date]       DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_ToClient] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id])
);
