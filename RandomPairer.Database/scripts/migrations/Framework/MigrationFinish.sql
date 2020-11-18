
		print @MigrationName + ' completed successfully' 
		UPDATE [dbo].[MigrationHistory] SET EndDate = GETUTCDATE()
		WHERE [MigrationName] = @MigrationName
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROllBACK;
		PRINT 'ROLLBACKED'

		SELECT 
		ERROR_NUMBER() AS ErrorNumber 
		,ERROR_SEVERITY() AS ErrorSeverity 
		,ERROR_STATE() AS ErrorState 
		,ERROR_PROCEDURE() AS ErrorProcedure 
		,ERROR_LINE() AS ErrorLine 
		,ERROR_MESSAGE() AS ErrorMessage; 

		declare @ErrorMessage nvarchar(max), @ErrorSeverity int, @ErrorState int;
		select @ErrorMessage = ERROR_MESSAGE() + ' Line ' + cast(ERROR_LINE() - 33 as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
		raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);

		BEGIN TRAN
			IF (EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE INFORMATION_SCHEMA.TABLES.TABLE_NAME = 'MigrationError'))
			BEGIN
				INSERT INTO [dbo].[MigrationError](MigrationName, Date, ErrorMessage) VALUES (@MigrationName, GETDATE(), @ErrorMessage)
			END
		COMMIT TRAN

		SET NOEXEC ON

	END CATCH;
END