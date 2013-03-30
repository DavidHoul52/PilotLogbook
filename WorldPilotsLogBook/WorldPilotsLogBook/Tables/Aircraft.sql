CREATE TABLE [WorldPilotsLogBook].[Aircraft] (
    [id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Reg]       NVARCHAR (MAX) NULL,
    [AcClassId] INT            NULL,
    [AcTypeId]  BIGINT         NOT NULL,
    [UserId]    BIGINT         DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Aircraft_AcType] FOREIGN KEY ([AcTypeId]) REFERENCES [WorldPilotsLogBook].[AcType] ([id]),
    CONSTRAINT [FK_Aircraft_User] FOREIGN KEY ([UserId]) REFERENCES [WorldPilotsLogBook].[User] ([id])
);





