USE Produtos
GO


SELECT * FROM Product INNER JOIN preco ON product.id_preco = preco.ID_Preco where nome = 'Máquina de Lavar Roupa LG F4WN408N0 - 8kg 1360rpm A+++'
SELECT * FROM SitesAVerificar
SELECT * FROM Website
SELECT * FROM Categoria

SELECT * FROM Preco


delete  from website where ID_Website=38


select * from Product inner join Preco on Preco.ID_Preco = Product.ID_Preco inner join preco_variacoes on Preco.ID_Preco= Preco_Variacoes.ID_Preco
where Nome='TV Xiaomi 32" Mi TV 4A Smart TV '

SELECT
    P.Nome, p.Popularidade, T.Id_preco,Preco, Data_alteracao, PRE.Preco_MaisBaixo_flag

FROM(
SELECT ID_Preco, Preco, Data_Alteracao, COUNT(*) OVER (PARTITION BY ID_Preco) AS cnt
FROM
Preco_Variacoes) AS T
LEFT JOIN Product AS P ON P.ID_Preco = T.ID_Preco
LEFT JOIN Preco AS PRE ON PRE.ID_Preco = T.ID_Preco
WHERE T.CNT > 1


select * from Preco_Variacoes where ID_Preco = 3728





SELECT * 
FROM Product 
INNER JOIN preco 
ON product.id_preco = preco.ID_Preco 
INNER JOIN preco_variacoes 
ON Preco_Variacoes.ID_Preco = Preco.ID_Preco
where Preco_MaisBaixo_flag =0 AND New_Product = 0 AND Nome = 'Portátil Asus VivoBook 15 F512DA-R5AV8SB1 15.6" Ryzen 5 3500U 12GB 1TB + 256GB SSD W10' 

order by data_preco_mais_Baixo 


	--INSERT INTO [dbo].[Preco_Variacoes]
	--					([ID_Preco]
	--					,[Preco]
	--					,[Data_Alteracao])
	--				VALUES
	--					(4445
	--					,600
	--					,GETDATE())

	--		UPDATE Preco
	--		SET PRECO.Preco_Atual = 600
	--		WHERE Preco.ID_Preco= 4445

