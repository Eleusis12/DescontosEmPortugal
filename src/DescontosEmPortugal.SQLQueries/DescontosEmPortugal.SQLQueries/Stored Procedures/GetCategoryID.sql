CREATE PROCEDURE GetCategoryID
	@SearchID	INT
AS
	
	BEGIN
	DECLARE @CategoryID INT
		 SELECT @CategoryID = S.ID_Categoria
					 FROM [dbo].[SitesAVerificar] S
					 WHERE S.ID_Pesquisa = @SearchID

					RETURN @CategoryID
	END