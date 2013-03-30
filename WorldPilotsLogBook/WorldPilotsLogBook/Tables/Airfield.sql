CREATE TABLE [WorldPilotsLogBook].[Airfield] (
    [id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [ICAOCode] NVARCHAR (MAX) NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [UserId]   BIGINT         DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Airfield_User] FOREIGN KEY ([UserId]) REFERENCES [WorldPilotsLogBook].[User] ([id])
);







