alter PROCEDURE InsertWebsiteToTrack
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

GO; 




CREATE PROCEDURE IsNullOrEmpty 
		@SomeVarcharParm varchar(max)
AS
BEGIN
IF @SomeVarcharParm IS NOT NULL AND LEN(@SomeVarcharParm) > 0
    RETURN 0
ELSE
    RETURN 1
END
GO

alter PROCEDURE UpSertProduct
	@ID				 VARCHAR(100),
	@Name			 VARCHAR(200),
	@Brand			 VARCHAR(100),
	@ImageLink		 VARCHAR(512),
	@CurrentPrice	 FLOAT(6),
	@WebsiteURL		VARCHAR(512),
	@Category		VARCHAR(100),
	@SearchID		INT,
	@Popularity		INT


AS
BEGIN

DECLARE @ID_Preco INT

DECLARE @Param1 BIT;
DECLARE @Param2  BIT;
DECLARE @Param3  BIT;
DECLARE @Param4  BIT;
DECLARE @Param5  BIT;
DECLARE @Param6  BIT;
DECLARE @Param7  BIT;
DECLARE @Param8  BIT;
DECLARE @Param9  BIT;




EXECUTE  @Param1 = IsNullOrEmpty @ID			
EXECUTE  @Param2 = IsNullOrEmpty @Name		
EXECUTE  @Param3 = IsNullOrEmpty @Brand		
EXECUTE  @Param4 = IsNullOrEmpty @ImageLink	
IF @CurrentPrice >= 0 
	
	SET @Param5 = 0

 ELSE
	SET @Param5 = 1

EXECUTE  @Param6 = IsNullOrEmpty @WebsiteURL
EXECUTE  @Param7 = IsNullOrEmpty @Category
IF @SearchID >= 0 
	
	SET @Param8 = 0

 ELSE
	SET @Param8 = 1


IF @Popularity >= 0 
	
	SET @Param9 = 0

 ELSE
	SET @Param9 = 1


		-- Verificar se os paramatros foram enviados com sucesso
IF (@Param1 = 1 OR @Param2 = 1 OR @Param3 = 1 OR @Param4 = 1 OR @Param5 = 1 OR @Param6 = 1 OR @Param7 = 1 OR @Param8 = 1 OR @Param9 = 1  )
		BEGIN
		RAISERROR ('Empty Parameters',16,1)
		PRINT 'Nao foi possivel efetuar a operacao sem todos os parametros'
		END

		-- Os Parametros foram enviados com sucesso
ELSE	
		-- Preencher as tabelas
	BEGIN


	IF EXISTS (SELECT Product.ID FROM Product WHERE ID = @ID)
		BEGIN

		SELECT @ID_Preco = Product.ID_Preco FROM Product WHERE ID = @ID

			--UPDATE
			UPDATE Preco
			SET PRECO.Preco_Atual = @CurrentPrice
			WHERE Preco.ID_Preco= @ID_Preco

			UPDATE Product
			SET Popularidade = @Popularity
			WHERE Product.ID = @ID
		END

	ELSE
		BEGIN
			--INSERT

		
			INSERT INTO [dbo].[Preco]
						   (
						   [Preco_Atual]
						   )
				VALUES
						   (
						   @CurrentPrice
						   )
			


				-- dá null aqui
				SELECT @ID_Preco = ID_Preco FROM Preco WHERE ID_Preco = @@identity;
			



			INSERT INTO [dbo].[Product]
					   ([ID]
					   ,[Nome]
					   ,[Marca]
					   ,[ID_Categoria]
					   ,[Imagem]
					   ,[Website]
					   ,[ID_Preco]
					   ,[ID_Pesquisa]
					   ,[Popularidade])

				 VALUES
					   (@ID	
					   ,@Name
					   ,@Brand	
					   ,@Category
					   ,@ImageLink
					   ,@WebsiteURL
					   ,@ID_Preco
					   ,@SearchID
					   ,@Popularity)

		END
	END

	
		
END

GO; 

CREATE PROCEDURE GetCategoryID
	@SearchID	INT
AS
	
	BEGIN
	DECLARE @CategoryID INT
		 SELECT @CategoryID = S.ID_Categoria
					 FROM [dbo].[SitesAVerificar] S
					 WHERE S.ID_Pesquisa = @SearchID

					RETURN @CategoryID
	END
GO

