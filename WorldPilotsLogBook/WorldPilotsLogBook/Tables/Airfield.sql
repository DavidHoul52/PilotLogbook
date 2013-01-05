CREATE TABLE [WorldPilotsLogBook].[Airfield] (
    [id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [ICAOCode] NVARCHAR (MAX) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);



