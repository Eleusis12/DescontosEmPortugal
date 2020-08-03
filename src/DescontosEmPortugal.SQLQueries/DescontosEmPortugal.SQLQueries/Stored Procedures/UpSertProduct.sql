
CREATE PROCEDURE UpSertProduct
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

		

			print (@CurrentPrice)
			--UPDATE
			UPDATE Preco
			SET PRECO.Preco_Atual = @CurrentPrice
			WHERE Preco.ID_Preco= @ID_Preco

			UPDATE Product
			SET Popularidade = @Popularity
			WHERE Product.ID = @ID

			print (@CurrentPrice)

			
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
			
					INSERT INTO [dbo].[Preco_Variacoes]
						([ID_Preco]
						,[Preco]
						,[Data_Alteracao])
					VALUES
						(@ID_Preco
						,@CurrentPrice
						,GETDATE())




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
