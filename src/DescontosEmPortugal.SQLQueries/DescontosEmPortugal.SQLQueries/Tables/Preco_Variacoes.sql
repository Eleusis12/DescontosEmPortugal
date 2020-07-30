
CREATE TABLE Preco_Variacoes(
	ID_Variacao				INT				NOT NULL				IDENTITY(1,1),
	ID_Preco				INT				NOT NULL,
	Preco					FLOAT(6)		NOT NULL,
	Data_Alteracao			Date			NOT NULL,

	PRIMARY KEY (ID_Variacao),
	FOREIGN KEY (ID_Preco) REFERENCES Preco(ID_Preco) ON DELETE CASCADE


);