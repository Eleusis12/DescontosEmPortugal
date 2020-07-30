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

