CREATE TRIGGER AddPriceToTable 
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
			

			INSERT INTO [dbo].[Preco_Variacoes]
						([ID_Preco]
						,[Preco]
						,[Data_Alteracao])
					VALUES
						(@ID_Preco
						,@Valor_Atual
						,GETDATE())


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