USE Produtos
GO


SELECT * FROM Product INNER JOIN preco ON product.id_preco = preco.ID_Preco where nome = 'Máquina de Lavar Roupa LG F4WN408N0 - 8kg 1360rpm A+++'
SELECT * FROM SitesAVerificar
SELECT * FROM Website
SELECT * FROM Categoria

SELECT * FROM Preco


delete  from website where ID_Website=38



SELECT * FROM Product INNER JOIN preco ON product.id_preco = preco.ID_Preco where Preco_MaisBaixo_flag =1 AND New_Product = 0 order by data_preco_mais_Baixo


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