CREATE TABLE [WorldPilotsLogBook].[Airfield] (
    [id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [ICAOCode] NVARCHAR (MAX) NULL,
    [Name]     NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

