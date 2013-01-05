CREATE TABLE [WorldPilotsLogBook].[Flight] (
    [id]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [AcTypeId]        INT           NOT NULL,
    [CapacityId]      INT           NOT NULL,
    [AirfiieldFromId] INT           NOT NULL,
    [AirfieldToId]    INT           NOT NULL,
    [Depart]        DATETIME      NOT NULL,
    [Arrival]          DATETIME      NOT NULL,
    [Captain]         VARCHAR (MAX) NOT NULL,
    [AircraftId]        INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);



