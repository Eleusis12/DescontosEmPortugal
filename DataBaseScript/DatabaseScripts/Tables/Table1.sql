﻿
CREATE TABLE Product(
	ID				VARCHAR(50)		NOT NULL,					
	Nome			VARCHAR(80)		NOT NULL,
	Marca			VARCHAR(512)	NOT NULL,
	ID_Categoria	INT				NOT NULL,
	Imagem			VARCHAR(512)	,
	Website			VARCHAR(512)	NOT NULL,
	ID_Preco		INT				NOT NULL,
	ID_Pesquisa		INT ,
	Popularidade	INT				NOT NULL

	PRIMARY KEY (ID),
	FOREIGN KEY (ID_Categoria)	REFERENCES Categoria(ID),
	FOREIGN KEY(ID_Preco)		REFERENCES Preco(ID_Preco),
	FOREIGN KEY (ID_Pesquisa)	REFERENCES SitesAVerificar(ID_Pesquisa)

	
	
);


  --ALTER TABLE Product 
  --ADD CONSTRAINT fk_name_product_categoria
  --FOREIGN KEY (ID_Categoria) 
  --REFERENCES Categoria(ID)
  --ON DELETE CASCADE;

  

--  USE [Produtos]
--GO

--ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK__Product__ID_Prec__603D47BB]
--GO

--ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ID_Preco])
--REFERENCES [dbo].[Preco] ([ID_Preco])  ON DELETE CASCADE;
--GO

--USE [Produtos]
--GO




  --  ALTER TABLE Product 
  --ADD CONSTRAINT fk_name_product_pesquisa
  --FOREIGN KEY (ID_Pesquisa) 
  --REFERENCES SitesAVerificar(ID_Pesquisa)
  --ON DELETE CASCADE;