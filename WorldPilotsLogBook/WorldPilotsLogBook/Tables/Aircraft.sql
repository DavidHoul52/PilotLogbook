CREATE TABLE [WorldPilotsLogBook].[Aircraft] (
    [id]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [Reg] NVARCHAR (MAX) NULL,
    [AcClassId] INT NULL, 
    PRIMARY KEY CLUSTERED ([id] ASC)
);

