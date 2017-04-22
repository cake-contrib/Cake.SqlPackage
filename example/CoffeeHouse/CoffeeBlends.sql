CREATE TABLE [dbo].[CoffeeBlends]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CoffeeBeanId] INT NULL, 
    CONSTRAINT [FK_CoffeeBlends_CoffeeBeans] FOREIGN KEY ([CoffeeBeanId]) REFERENCES [CoffeeBeans]([Id])
)
