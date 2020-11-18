
SET XACT_ABORT ON
SET ARITHABORT ON
IF OBJECT_ID('dbo.MigrationHistory') IS NULL
BEGIN
	SET NOEXEC ON
END
DECLARE @StartDate DATETIME, @EndDate DATETIME, @ShouldExecute BIT
SELECT @StartDate = StartDate, @EndDate = EndDate
	FROM dbo.MigrationHistory
	WHERE [MigrationName] = @MigrationName

IF @StartDate IS NULL
BEGIN
	PRINT 'Inserting and executing ' + @MigrationName
	INSERT dbo.MigrationHistory([MigrationName], [StartDate])
	VALUES (@MigrationName, GETUTCDATE())
	SET @ShouldExecute = 1
END
ELSE IF @EndDate IS NULL
BEGIN
	PRINT 'Executing ' + @MigrationName
	SET @ShouldExecute = 1
END
ELSE BEGIN
	PRINT 'Already executed ' + @MigrationName
	SET @ShouldExecute = 0
END

IF @ShouldExecute = 1
BEGIN
	BEGIN TRY
	BEGIN TRAN