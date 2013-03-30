CREATE TABLE [WorldPilotsLogBook].[Flight] (
    [id]                        BIGINT        IDENTITY (1, 1) NOT NULL,
    [CapacityId]                BIGINT        NOT NULL,
    [AirfieldFromId]            BIGINT        NOT NULL,
    [AirfieldToId]              BIGINT        NOT NULL,
    [Depart]                    DATETIME      NOT NULL,
    [Arrival]                   DATETIME      NOT NULL,
    [Captain]                   VARCHAR (MAX) NOT NULL,
    [AircraftId]                BIGINT        NOT NULL,
    [Date]                      DATETIME      NOT NULL,
    [Remarks]                   NVARCHAR (50) NULL,
    [Takeoffs]                  INT           NULL,
    [LDG]                       INT           NULL,
    [Night]                     BIT           NULL,
    [SimulatedInstrumentFlying] DATETIME      NULL,
    [InstrumentFlying]          DATETIME      NULL,
    [UserId]                    BIGINT        NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Flight_Aircraft] FOREIGN KEY ([AircraftId]) REFERENCES [WorldPilotsLogBook].[Aircraft] ([id]),
    CONSTRAINT [FK_Flight_AirfieldFrom] FOREIGN KEY ([AirfieldFromId]) REFERENCES [WorldPilotsLogBook].[Airfield] ([id]),
    CONSTRAINT [FK_Flight_AirfieldTo] FOREIGN KEY ([AirfieldToId]) REFERENCES [WorldPilotsLogBook].[Airfield] ([id]),
    CONSTRAINT [FK_Flight_Capacity] FOREIGN KEY ([CapacityId]) REFERENCES [WorldPilotsLogBook].[Capacity] ([id]),
    CONSTRAINT [FK_Flight_User] FOREIGN KEY ([UserId]) REFERENCES [WorldPilotsLogBook].[User] ([id])
);











