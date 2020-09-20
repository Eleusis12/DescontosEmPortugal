CREATE TABLE SitesAVerificar(
	
	ID_Pesquisa					INT				NOT NULL		IDENTITY(1,1),
	ID_Website					INT				NOT NULL,
	ID_Categoria				INT				NOT NULL,
	

	PRIMARY KEY (ID_Pesquisa),
	FOREIGN KEY (ID_Categoria) REFERENCES Categoria(ID),
	FOREIGN KEY (ID_Website) REFERENCES Website(ID_Website),

);

--ALTER TABLE SitesAVerificar 
--  ADD CONSTRAINT FK__SitesAVer__ID
--  FOREIGN KEY (ID_Categoria) 
--  REFERENCES Categoria(ID) 
--  ON DELETE CASCADE;

--  ALTER TABLE SitesAVerificar 
--  ADD CONSTRAINT FK__SitesAVer__ID
--  FOREIGN KEY (ID_Website) 
--  REFERENCES Website(ID_Website) 
--  ON DELETE CASCADE;