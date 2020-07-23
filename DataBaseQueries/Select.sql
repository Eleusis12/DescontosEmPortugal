USE Produtos
GO


SELECT * FROM Product INNER JOIN preco ON product.id_preco = preco.ID_Preco 
SELECT * FROM SitesAVerificar
SELECT * FROM Website
SELECT * FROM Categoria


SELECT * FROM Product INNER JOIN preco ON product.id_preco = preco.ID_Preco where Preco_Atual != Preco_MaisBaixo
