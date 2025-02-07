USE [master]
GO
/****** Object:  Database [Produtos]    Script Date: 20/09/2020 14:41:38 ******/
CREATE DATABASE [Produtos]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProdutosAmazon', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ProdutosAmazon.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProdutosAmazon_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ProdutosAmazon_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Produtos] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Produtos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Produtos] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Produtos] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Produtos] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Produtos] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Produtos] SET ARITHABORT OFF 
GO
ALTER DATABASE [Produtos] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Produtos] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Produtos] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Produtos] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Produtos] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Produtos] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Produtos] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Produtos] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Produtos] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Produtos] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Produtos] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Produtos] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Produtos] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Produtos] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Produtos] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Produtos] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Produtos] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Produtos] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Produtos] SET  MULTI_USER 
GO
ALTER DATABASE [Produtos] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Produtos] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Produtos] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Produtos] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Produtos] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Produtos] SET QUERY_STORE = OFF
GO
USE [Produtos]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preco]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preco](
	[ID_Preco] [int] IDENTITY(1,1) NOT NULL,
	[Preco_Atual] [real] NOT NULL,
	[Preco_MaisBaixo] [real] NULL,
	[Preco_MaisBaixo_flag] [bit] NOT NULL,
	[New_Product] [bit] NULL,
	[Data_Preco_Mais_Baixo] [date] NOT NULL,
	[Soma] [real] NOT NULL,
	[Contador] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Preco] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preco_Variacoes]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preco_Variacoes](
	[ID_Variacao] [int] IDENTITY(1,1) NOT NULL,
	[ID_Preco] [int] NOT NULL,
	[Preco] [real] NOT NULL,
	[Data_Alteracao] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Variacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [varchar](50) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
	[Marca] [varchar](512) NOT NULL,
	[ID_Categoria] [int] NOT NULL,
	[Imagem] [varchar](512) NULL,
	[Website] [varchar](512) NOT NULL,
	[ID_Preco] [int] NOT NULL,
	[ID_Pesquisa] [int] NULL,
	[Popularidade] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SitesAVerificar]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SitesAVerificar](
	[ID_Pesquisa] [int] IDENTITY(1,1) NOT NULL,
	[ID_Website] [int] NOT NULL,
	[ID_Categoria] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Pesquisa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Website]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Website](
	[ID_Website] [int] IDENTITY(1,1) NOT NULL,
	[SiteURL] [varchar](512) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Website] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[SiteURL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Preco_Variacoes]  WITH CHECK ADD FOREIGN KEY([ID_Preco])
REFERENCES [dbo].[Preco] ([ID_Preco])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ID_Categoria])
REFERENCES [dbo].[Categoria] ([ID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ID_Pesquisa])
REFERENCES [dbo].[SitesAVerificar] ([ID_Pesquisa])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ID_Preco])
REFERENCES [dbo].[Preco] ([ID_Preco])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SitesAVerificar]  WITH CHECK ADD  CONSTRAINT [FK__SitesAVer__ID_Ca__59904A2C] FOREIGN KEY([ID_Categoria])
REFERENCES [dbo].[Categoria] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SitesAVerificar] CHECK CONSTRAINT [FK__SitesAVer__ID_Ca__59904A2C]
GO
ALTER TABLE [dbo].[SitesAVerificar]  WITH CHECK ADD FOREIGN KEY([ID_Website])
REFERENCES [dbo].[Website] ([ID_Website])
GO
/****** Object:  StoredProcedure [dbo].[GetCategoryID]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCategoryID]
	@SearchID	INT
AS
	
	BEGIN
	DECLARE @CategoryID INT
		 SELECT @CategoryID = S.ID_Categoria
					 FROM [dbo].[SitesAVerificar] S
					 WHERE S.ID_Pesquisa = @SearchID

					RETURN @CategoryID
	END
GO
/****** Object:  StoredProcedure [dbo].[InsertWebsiteToTrack]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertWebsiteToTrack]
	@URL		 VARCHAR(512),
	@Category	 VARCHAR(80)
	
AS
BEGIN

DECLARE @ID_Website INT;
DECLARE @ID_Categoria INT;



DECLARE @Param1 BIT;
DECLARE @Param2  BIT;
DECLARE @Param3  BIT;
EXECUTE  @Param1 = IsNullOrEmpty @URL
EXECUTE  @Param2 = IsNullOrEmpty @URL


		-- Verificar se os paramatros foram enviados com sucesso
IF (@Param1 = 1 OR @Param2 = 1 )
		BEGIN
		RAISERROR ('Empty Parameters',16,1)
		PRINT 'Nao foi possivel efetuar a operacao sem todos os parametros'
		END

		-- Os Parametros foram enviados com sucesso
ELSE	
		-- Preencher as tabelas
	BEGIN


		INSERT INTO [dbo].[Website]
				   ([SiteURL])
			
			 VALUES
				   (@URL)

		SELECT @ID_Website = ID_Website FROM Website WHERE ID_Website = SCOPE_IDENTITY();

		INSERT INTO [dbo].[Categoria]
				   ([Nome])
			
			VALUES
				   (@Category)
			 

			SELECT @ID_Categoria = ID FROM Categoria WHERE ID = SCOPE_IDENTITY();
		--DECLARE @INTEIRO1 INT = SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];

		
		
		
		--DECLARE @ID_Website INT = SELECT MAX(ID_Website)
		--			FROM [dbo].[Website]

		INSERT INTO [dbo].[SitesAVerificar]
				   ([ID_Website]
				   ,[ID_Categoria]
				   )
			 VALUES
				   (@ID_Website
				   ,@ID_Categoria
				   )



	END
END
GO
/****** Object:  StoredProcedure [dbo].[IsNullOrEmpty]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IsNullOrEmpty] 
		@SomeVarcharParm varchar(max)
AS
BEGIN
IF @SomeVarcharParm IS NOT NULL AND LEN(@SomeVarcharParm) > 0
    RETURN 0
ELSE
    RETURN 1
END
GO
/****** Object:  StoredProcedure [dbo].[UpSertProduct]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpSertProduct]
	@ID				 VARCHAR(100),
	@Name			 VARCHAR(200),
	@Brand			 VARCHAR(100),
	@ImageLink		 VARCHAR(512),
	@CurrentPrice	 FLOAT(6),
	@WebsiteURL		VARCHAR(512),
	@Category		VARCHAR(100),
	@SearchID		INT,
	@Popularity		INT


AS
BEGIN

DECLARE @ID_Preco INT

DECLARE @Param1 BIT;
DECLARE @Param2  BIT;
DECLARE @Param3  BIT;
DECLARE @Param4  BIT;
DECLARE @Param5  BIT;
DECLARE @Param6  BIT;
DECLARE @Param7  BIT;
DECLARE @Param8  BIT;
DECLARE @Param9  BIT;




EXECUTE  @Param1 = IsNullOrEmpty @ID			
EXECUTE  @Param2 = IsNullOrEmpty @Name		
EXECUTE  @Param3 = IsNullOrEmpty @Brand		
EXECUTE  @Param4 = IsNullOrEmpty @ImageLink	
IF @CurrentPrice >= 0 
	
	SET @Param5 = 0

 ELSE
	SET @Param5 = 1

EXECUTE  @Param6 = IsNullOrEmpty @WebsiteURL
EXECUTE  @Param7 = IsNullOrEmpty @Category
IF @SearchID >= 0 
	
	SET @Param8 = 0

 ELSE
	SET @Param8 = 1


IF @Popularity >= 0 
	
	SET @Param9 = 0

 ELSE
	SET @Param9 = 1


		-- Verificar se os paramatros foram enviados com sucesso
IF (@Param1 = 1 OR @Param2 = 1 OR @Param3 = 1 OR @Param4 = 1 OR @Param5 = 1 OR @Param6 = 1 OR @Param7 = 1 OR @Param8 = 1 OR @Param9 = 1  )
		BEGIN
		RAISERROR ('Empty Parameters',16,1)
		PRINT 'Nao foi possivel efetuar a operacao sem todos os parametros'
		END

		-- Os Parametros foram enviados com sucesso
ELSE	
		-- Preencher as tabelas
	BEGIN

	IF EXISTS (SELECT Product.ID FROM Product WHERE ID = @ID)
		BEGIN
		

		SELECT @ID_Preco = Product.ID_Preco FROM Product WHERE ID = @ID

		

			print (@CurrentPrice)
			--UPDATE
			UPDATE Preco
			SET PRECO.Preco_Atual = @CurrentPrice
			WHERE Preco.ID_Preco= @ID_Preco

			UPDATE Product
			SET Popularidade = @Popularity
			WHERE Product.ID = @ID

			print (@CurrentPrice)

			
		END

	ELSE
		BEGIN
			--INSERT
		
		
			INSERT INTO [dbo].[Preco]
						   (
						   [Preco_Atual]
						   )
				VALUES
						   (
						   @CurrentPrice
						   )
			


				-- dá null aqui
				SELECT @ID_Preco = ID_Preco FROM Preco WHERE ID_Preco = @@identity;
			
					INSERT INTO [dbo].[Preco_Variacoes]
						([ID_Preco]
						,[Preco]
						,[Data_Alteracao])
					VALUES
						(@ID_Preco
						,@CurrentPrice
						,GETDATE())




			INSERT INTO [dbo].[Product]
					   ([ID]
					   ,[Nome]
					   ,[Marca]
					   ,[ID_Categoria]
					   ,[Imagem]
					   ,[Website]
					   ,[ID_Preco]
					   ,[ID_Pesquisa]
					   ,[Popularidade])

				 VALUES
					   (@ID	
					   ,@Name
					   ,@Brand	
					   ,@Category
					   ,@ImageLink
					   ,@WebsiteURL
					   ,@ID_Preco
					   ,@SearchID
					   ,@Popularity)

		END
	END

	
		
END
GO
/****** Object:  Trigger [dbo].[AddPriceToTable]    Script Date: 20/09/2020 14:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create TRIGGER [dbo].[AddPriceToTable] 
ON [dbo].[Preco] 
INSTEAD OF INSERT 
AS
	BEGIN
		DECLARE @Valor_Atual		FLOAT(6)
		DECLARE @Valor_MaisBaixo	FLOAT(6)
		DECLARE @ID_Preco			INT
		DECLARE @Flag				BIT

		SELECT @ID_Preco = i.ID_Preco, @Valor_Atual =  I.Preco_Atual, @Valor_MaisBaixo = I.Preco_MaisBaixo  FROM INSERTED I
		IF (@Valor_MaisBaixo IS NULL)
			
			SET @Valor_MaisBaixo =  @Valor_Atual + 1
			

			
			-- INSERE NA TABELA OS DADOS
			INSERT INTO [dbo].[Preco]
					   (
					  
					 
					   [Preco_Atual],
					   [Preco_MaisBaixo],
					   [Preco_MaisBaixo_flag],
					   [New_Product],
					   [Data_Preco_Mais_Baixo],
					   [Soma],
					   [Contador]
					   ) VALUES (@Valor_Atual,@Valor_Atual, 1,1, GETDATE(), @Valor_Atual, 1)



	

		
			

	END
GO
ALTER TABLE [dbo].[Preco] ENABLE TRIGGER [AddPriceToTable]
GO
/****** Object:  Trigger [dbo].[CheckIfMinimumValueHasBeenReached]    Script Date: 20/09/2020 14:41:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[CheckIfMinimumValueHasBeenReached] 
ON [dbo].[Preco] 
INSTEAD OF UPDATE 
AS

		DECLARE @Valor_Anterior		FLOAT(6)
		DECLARE @Valor_Atual		FLOAT(6)
		DECLARE @Valor_MaisBaixo	FLOAT(6)
		DECLARE @ID_Preco			INT
		DECLARE @Flag				BIT
		DECLARE @New_Product		BIT


		
		
		


		SELECT @ID_Preco = i.ID_Preco, @Valor_Atual =  I.Preco_Atual  FROM INSERTED I 

		SELECT @Valor_Anterior= Preco.Preco_Atual,	@Valor_MaisBaixo = Preco.Preco_MaisBaixo, @New_Product= New_Product FROM Preco	WHERE ID_Preco = @ID_Preco

		
			
		IF (@Valor_Atual != @Valor_Anterior   )
			BEGIN


			INSERT INTO [dbo].[Preco_Variacoes]
						([ID_Preco]
						,[Preco]
						,[Data_Alteracao])
					VALUES
						(@ID_Preco
						,@Valor_Atual
						,GETDATE())
			



			IF(@New_Product=1)
				BEGIN 
					-- Alterou o preço pelo menos uma vez o que significa que o produto perde o estado de novo produto
					UPDATE PRECO
					SET  New_Product=0
					WHERE ID_Preco = @ID_Preco	
				END
			
				-- Atualizar o preço, caso este seja diferente do anterior
			UPDATE PRECO
			SET  Preco_Atual = @Valor_Atual, Soma= Soma+ @Valor_Atual , Contador= Contador + 1
			WHERE ID_Preco = @ID_Preco	

				
				


			IF(@Valor_Atual < @Valor_MaisBaixo)
			BEGIN
								
				UPDATE PRECO
				SET  Preco_MaisBaixo = @Valor_Atual, Preco_MaisBaixo_flag = 1, Data_Preco_Mais_Baixo = GETDATE()
				WHERE ID_Preco = @ID_Preco	
			END

			ELSE	
				BEGIN
				
					UPDATE PRECO
					SET  Preco_MaisBaixo_flag = 0
					WHERE ID_Preco = @ID_Preco	

				END


				
				
				
			
		END


		

GO
ALTER TABLE [dbo].[Preco] ENABLE TRIGGER [CheckIfMinimumValueHasBeenReached]
GO
USE [master]
GO
ALTER DATABASE [Produtos] SET  READ_WRITE 
GO
