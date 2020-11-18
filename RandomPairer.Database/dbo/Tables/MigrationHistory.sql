CREATE TABLE [dbo].[MigrationHistory]
(
	[MigrationName]		VARCHAR(100)	NOT NULL,
	[StartDate]			DATETIME		NOT NULL, 
	[EndDate]			DATETIME		NULL,

	CONSTRAINT [pkMigrationHistory] PRIMARY KEY CLUSTERED ([MigrationName]),
)
GO