CREATE TABLE [dbo].[MigrationError]
(
	[Id]			INT				NOT NULL	IDENTITY,
	[MigrationName]	VARCHAR(100)	NOT NULL,
	[Date]			DATETIME		NOT NULL,
	[ErrorMessage]	NVARCHAR(MAX)	NOT NULL,

	CONSTRAINT [pkMigrationError]	PRIMARY KEY CLUSTERED ([Id])
)
GO