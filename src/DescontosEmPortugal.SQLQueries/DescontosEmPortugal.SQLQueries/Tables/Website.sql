
CREATE TABLE Website(

	ID_Website					INT				NOT NULL		IDENTITY(1,1),
	SiteURL						VARCHAR(512)	UNIQUE,
	PRIMARY KEY (ID_Website	),
);