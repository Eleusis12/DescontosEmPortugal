CREATE TRIGGER CheckIfMinimumValueHasBeenReached 
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

		SELECT @Valor_Anterior= Preco.Preco_Atual,	@Valor_MaisBaixo = Preco.Preco_MaisBaixo, @New_Product= New_Product FROM Preco	WHERE ID_Preco = @ID_Preco

		
			
		IF (@Valor_Atual != @Valor_Anterior   )
			BEGIN


			INSERT INTO [dbo].[Preco_Variacoes]
						([ID_Preco]
						,[Preco]
						,[Data_Alteracao])
					VALUES
						(@ID_Preco
						,@Valor_Atual
						,GETDATE())
			



			IF(@New_Product=1)
				BEGIN 
					-- Alterou o preço pelo menos uma vez o que significa que o produto perde o estado de novo produto
					UPDATE PRECO
					SET  New_Product=0
					WHERE ID_Preco = @ID_Preco	
				END
			
				-- Atualizar o preço, caso este seja diferente do anterior
			UPDATE PRECO
			SET  Preco_Atual = @Valor_Atual, Soma= Soma+ @Valor_Atual , Contador= Contador + 1
			WHERE ID_Preco = @ID_Preco	

				
				


			IF(@Valor_Atual < @Valor_MaisBaixo)
			BEGIN
								
				UPDATE PRECO
				SET  Preco_MaisBaixo = @Valor_Atual, Preco_MaisBaixo_flag = 1, Data_Preco_Mais_Baixo = GETDATE()
				WHERE ID_Preco = @ID_Preco	
			END

			ELSE	
				BEGIN
				
					UPDATE PRECO
					SET  Preco_MaisBaixo_flag = 0
					WHERE ID_Preco = @ID_Preco	

				END


				
				
				
			
		END


		

