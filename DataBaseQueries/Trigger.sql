USE Produtos
GO

create TRIGGER AddPriceToTable 
ON Preco 
INSTEAD OF INSERT 
AS
	BEGIN
		DECLARE @Valor_Atual		FLOAT(6)
		DECLARE @Valor_MaisBaixo	FLOAT(6)
		DECLARE @ID_Preco			INT
		DECLARE @Flag				BIT

		SELECT @ID_Preco = i.ID_Preco, @Valor_Atual =  I.Preco_Atual, @Valor_MaisBaixo = I.Preco_MaisBaixo  FROM INSERTED I
		IF (@Valor_MaisBaixo IS NULL)
			
			SET @Valor_MaisBaixo =  @Valor_Atual + 1
			

			-- INSERE NA TABELA OS DADOS
			INSERT INTO [dbo].[Preco]
					   (
					   [Preco_Atual],
					   [Preco_MaisBaixo],
					   [Preco_MaisBaixo_flag],
					   [New_Product],
					   [Data_Preco_Mais_Baixo],
					   [Soma],
					   [Contador]
					   ) VALUES (@Valor_Atual,@Valor_Atual, 1,1, GETDATE(), @Valor_Atual, 1)
		
			

	END
GO;

ALTER TRIGGER CheckIfMinimumValueHasBeenReached 
ON Preco 
INSTEAD OF UPDATE 
AS

		DECLARE @Valor_Anterior		FLOAT(6)
		DECLARE @Valor_Atual		FLOAT(6)
		DECLARE @Valor_MaisBaixo	FLOAT(6)
		DECLARE @ID_Preco			INT
		DECLARE @Flag				BIT
		DECLARE @New_Product		BIT




		SELECT @ID_Preco = i.ID_Preco, @Valor_Atual =  I.Preco_Atual  FROM INSERTED I 

		SELECT @Valor_Anterior= @Valor_Atual,	@Valor_MaisBaixo = Preco.Preco_MaisBaixo, @New_Product= New_Product FROM Preco	WHERE ID_Preco = @ID_Preco

		
			-- Alterou o preço pelo menos uma vez o que significa que o produto perde o estado de novo produto
		IF ((@Valor_Atual < @Valor_Anterior OR @Valor_Atual > @Valor_Anterior) AND @New_Product=1  )
			BEGIN
				UPDATE PRECO
				SET  New_Product=0, Soma= Soma+ @Valor_Atual , Contador= Contador + 1
				WHERE ID_Preco = @ID_Preco	

			

	

		IF(@Valor_Atual < @Valor_Anterior)
			BEGIN
				UPDATE PRECO
				SET Preco_Atual = @Valor_Atual, Preco_MaisBaixo = @Valor_MaisBaixo, Preco_MaisBaixo_flag = 1, Data_Preco_Mais_Baixo	 = GETDATE(), New_Product=0
				WHERE ID_Preco = @ID_Preco	

					IF(@Valor_Atual < @Valor_MaisBaixo)
					BEGIN
						UPDATE PRECO
						SET  Preco_MaisBaixo = @Valor_MaisBaixo, Preco_MaisBaixo_flag = 1, Data_Preco_Mais_Baixo = GETDATE()
						WHERE ID_Preco = @ID_Preco	
					END

				
			END
		ELSE IF(@Valor_Atual > @Valor_Anterior)
			BEGIN
				UPDATE Preco
				SET	Preco_Atual = @Valor_Atual, Preco_MaisBaixo_flag= 0
				WHERE ID_Preco = @ID_Preco	

			END
			
		END
	

		


	

go;



