CREATE TABLE [WorldPilotsLogBook].[User] (
    [id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [DisplayName] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

