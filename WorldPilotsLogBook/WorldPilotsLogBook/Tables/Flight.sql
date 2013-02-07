CREATE TABLE [WorldPilotsLogBook].[Flight] (
    [id]             BIGINT        IDENTITY (1, 1) NOT NULL,
    [AcTypeId]       INT           NOT NULL,
    [CapacityId]     INT           NOT NULL,
    [AirfieldFromId] INT           NOT NULL,
    [AirfieldToId]   INT           NOT NULL,
    [Depart]         DATETIME      NOT NULL,
    [Arrival]        DATETIME      NOT NULL,
    [Captain]        VARCHAR (MAX) NOT NULL,
    [AircraftId]     INT           NOT NULL,
    [Date]           DATETIME      NOT NULL,
    [Remarks] NVARCHAR(50) NULL, 
    [TODay] INT NULL, 
    [LDGDay] INT NULL, 
    [TONight] INT NULL, 
    [LDGNight] INT NULL, 
    PRIMARY KEY CLUSTERED ([id] ASC)
);





