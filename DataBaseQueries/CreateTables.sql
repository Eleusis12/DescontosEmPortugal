USE Produtos
GO

CREATE TABLE Categoria(
	ID			INTEGER			NOT NULL						IDENTITY(1,1),
	Nome		VARCHAR(30)		NOT NULL						UNIQUE,	

	PRIMARY KEY (ID),
);



CREATE TABLE Website(

	ID_Website					INT				NOT NULL		IDENTITY(1,1),
	SiteURL						VARCHAR(512)	UNIQUE,
	PRIMARY KEY (ID_Website	),
);


CREATE TABLE SitesAVerificar(
	
	ID_Pesquisa					INT				NOT NULL		IDENTITY(1,1),
	ID_Website					INT				NOT NULL,
	ID_Categoria				INT				NOT NULL,
	

	PRIMARY KEY (ID_Pesquisa),
	FOREIGN KEY (ID_Categoria) REFERENCES Categoria(ID),
	FOREIGN KEY (ID_Website) REFERENCES Website(ID_Website),

);

CREATE TABLE Preco(
	ID_Preco				INT				NOT NULL				IDENTITY(1,1),
	Preco_Atual				FLOAT(6)		NOT NULL,
	Preco_MaisBaixo			FLOAT(6)		,
	Preco_MaisBaixo_flag	BIT				NOT NULL,
	New_Product				BIT,
	Data_Preco_Mais_Baixo	DATE			NOT NULL,
	Soma					FLOAT(6)		NOT NULL,
	Contador				INT				NOT NULL
	
	PRIMARY KEY(ID_Preco)
);

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

ALTER TABLE Product ALTER COLUMN Nome VARCHAR (200) NOT NULL;








