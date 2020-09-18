CREATE TABLE [dbo].[OrderItem] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [OrderId]    UNIQUEIDENTIFIER NOT NULL,
    [PizzaId1]   UNIQUEIDENTIFIER NOT NULL,
    [PizzaId2]   UNIQUEIDENTIFIER NULL,
    [PizzaName1] NVARCHAR (50)    NOT NULL,
    [PizzaName2] NVARCHAR (50)    NULL,
    [Price]      NUMERIC (18, 2)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderItem_ToOrder] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItem_Pizza1] FOREIGN KEY ([PizzaId1]) REFERENCES [dbo].[Pizza] ([Id]),
    CONSTRAINT [FK_OrderItem_Pizza2] FOREIGN KEY ([PizzaId2]) REFERENCES [dbo].[Pizza] ([Id])
);

