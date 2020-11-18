CREATE TABLE [dbo].[User]
(
	[Id]		INT				NOT NULL	IDENTITY,
	[Name]		NVARCHAR(255)	NOT NULL,
	[Secret]	NVARCHAR(255)	NULL,
	[PairId]	INT				NULL,

	CONSTRAINT [pkUsers] PRIMARY KEY ([Id]),
	CONSTRAINT [fkUserToUser] FOREIGN KEY ([PairId]) REFERENCES [dbo].[User]([Id])
)
