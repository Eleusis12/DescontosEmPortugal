CREATE PROCEDURE IsNullOrEmpty 
		@SomeVarcharParm varchar(max)
AS
BEGIN
IF @SomeVarcharParm IS NOT NULL AND LEN(@SomeVarcharParm) > 0
    RETURN 0
ELSE
    RETURN 1
END