Create PROCEDURE InsertWebsiteToTrack
	@URL		 VARCHAR(512),
	@Category	 VARCHAR(80)
	
AS
BEGIN

DECLARE @ID_Website INT;
DECLARE @ID_Categoria INT;



DECLARE @Param1 BIT;
DECLARE @Param2  BIT;
DECLARE @Param3  BIT;
EXECUTE  @Param1 = IsNullOrEmpty @URL
EXECUTE  @Param2 = IsNullOrEmpty @URL


		-- Verificar se os paramatros foram enviados com sucesso
IF (@Param1 = 1 OR @Param2 = 1 )
		BEGIN
		RAISERROR ('Empty Parameters',16,1)
		PRINT 'Nao foi possivel efetuar a operacao sem todos os parametros'
		END

		-- Os Parametros foram enviados com sucesso
ELSE	
		-- Preencher as tabelas
	BEGIN


		INSERT INTO [dbo].[Website]
				   ([SiteURL])
			
			 VALUES
				   (@URL)

		SELECT @ID_Website = ID_Website FROM Website WHERE ID_Website = SCOPE_IDENTITY();

		INSERT INTO [dbo].[Categoria]
				   ([Nome])
			
			VALUES
				   (@Category)
			 

			SELECT @ID_Categoria = ID FROM Categoria WHERE ID = SCOPE_IDENTITY();
		--DECLARE @INTEIRO1 INT = SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];

		
		
		
		--DECLARE @ID_Website INT = SELECT MAX(ID_Website)
		--			FROM [dbo].[Website]

		INSERT INTO [dbo].[SitesAVerificar]
				   ([ID_Website]
				   ,[ID_Categoria]
				   )
			 VALUES
				   (@ID_Website
				   ,@ID_Categoria
				   )



	END
END
