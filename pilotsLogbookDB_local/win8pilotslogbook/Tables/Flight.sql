CREATE TABLE [WorldPilotsLogBook].[Flight] (
    [Id]         BIGINT             IDENTITY (1, 1) NOT NULL,
    [Reg]        NVARCHAR (MAX)     NULL,
    [Depart]     DATETIMEOFFSET (3) NULL,
    [Captain]    NVARCHAR (MAX)     NULL,
    [Arrival]    DATETIMEOFFSET (3) NULL,
    [Date]       DATETIMEOFFSET (3) NULL,
    [AcTypeId]   BIGINT             NULL,
    [FromAirfieldId] BIGINT             NULL,
    [CapacityId] BIGINT NULL, 
    [ToAirfieldId] BIGINT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [flight_actypeid_fk] FOREIGN KEY ([AcTypeId]) REFERENCES [WorldPilotsLogBook].[AcType] ([id]),
    CONSTRAINT [flight_fromairfieldid_fk] FOREIGN KEY ([FromAirfieldId]) REFERENCES [WorldPilotsLogBook].[Airfield] ([id]), 
	CONSTRAINT [flight_toairfieldid_fk] FOREIGN KEY ([ToAirfieldId]) REFERENCES [WorldPilotsLogBook].[Airfield] ([id]), 
    CONSTRAINT [flight_capacity_fk] FOREIGN KEY ([CapacityId]) REFERENCES [WorldPilotsLogBook].[Capacity](id)
);

