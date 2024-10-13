USE [master]
GO
/****** Object:  Database [Company_Info]    Script Date: 8/12/2024 3:21:56 PM ******/
CREATE DATABASE [Company_Info]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Company_Info', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SAMIKSHA_SQLSERV\MSSQL\DATA\Company_Info.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Company_Info_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SAMIKSHA_SQLSERV\MSSQL\DATA\Company_Info_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Company_Info] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Company_Info].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Company_Info] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Company_Info] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Company_Info] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Company_Info] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Company_Info] SET ARITHABORT OFF 
GO
ALTER DATABASE [Company_Info] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Company_Info] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Company_Info] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Company_Info] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Company_Info] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Company_Info] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Company_Info] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Company_Info] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Company_Info] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Company_Info] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Company_Info] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Company_Info] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Company_Info] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Company_Info] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Company_Info] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Company_Info] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Company_Info] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Company_Info] SET RECOVERY FULL 
GO
ALTER DATABASE [Company_Info] SET  MULTI_USER 
GO
ALTER DATABASE [Company_Info] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Company_Info] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Company_Info] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Company_Info] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Company_Info] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Company_Info] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Company_Info', N'ON'
GO
ALTER DATABASE [Company_Info] SET QUERY_STORE = OFF
GO
USE [Company_Info]
GO
/****** Object:  Table [dbo].[category]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[CategoryName] [nvarchar](50) NULL,
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NULL,
	[description] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[UserName] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[userId] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategory]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCategory]
    @CategoryId INT
AS
BEGIN
    DELETE FROM category
    WHERE CategoryId = @CategoryId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProduct]
    @ProductId INT
AS
BEGIN
    DELETE FROM product
    WHERE ProductId = @ProductId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteUser]
    @UserId INT
AS
BEGIN
    DELETE FROM users
    WHERE UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCategories]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllCategories]
AS
BEGIN
    SELECT * FROM category
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllProducts]
AS
BEGIN
    SELECT * FROM product
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
    SELECT * FROM users
END
GO
/****** Object:  StoredProcedure [dbo].[GetCategoryById]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCategoryById]
    @categoryId INT
AS
BEGIN
    SELECT * FROM category WHERE categoryId = @categoryId
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProductById]
    @productId INT
AS
BEGIN
    SELECT * FROM product WHERE productId = @productId
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserById]
    @userId INT
AS
BEGIN
    SELECT * FROM users WHERE userId = @userId
END
GO
/****** Object:  StoredProcedure [dbo].[InsertCategory]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCategory]
    @CategoryName NVARCHAR(50)
AS
BEGIN
    INSERT INTO category (CategoryName)
    VALUES (@CategoryName);
END
GO
/****** Object:  StoredProcedure [dbo].[InsertProduct]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertProduct]
    @ProductName NVARCHAR(50),
    @Description NVARCHAR(100)
AS
BEGIN
    INSERT INTO product (ProductName, Description)
    VALUES (@ProductName, @Description);
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUser]
   
    @UserName NVARCHAR(50),
    @Address NVARCHAR(100)
AS
BEGIN
    INSERT INTO users (UserName, Address)
    VALUES (@UserName, @Address);
END
GO
/****** Object:  StoredProcedure [dbo].[SelectCategory]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectCategory]
    @CategoryId INT
AS
BEGIN
    SELECT * FROM category
    WHERE CategoryId = @CategoryId;
END
GO
/****** Object:  StoredProcedure [dbo].[SelectProduct]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectProduct]
    @ProductId INT
AS
BEGIN
    SELECT * FROM product
    WHERE ProductId = @ProductId;
END
GO
/****** Object:  StoredProcedure [dbo].[SelectUser]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectUser]
    @UserId INT
AS
BEGIN
    SELECT * FROM users
    WHERE UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCategory]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCategory]
    @CategoryId INT,
    @CategoryName NVARCHAR(50)
AS
BEGIN
    UPDATE category
    SET CategoryName = @CategoryName
    WHERE CategoryId = @CategoryId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateProduct]
    @ProductId INT,
    @ProductName NVARCHAR(50),
    @Description NVARCHAR(100)
AS
BEGIN
    UPDATE product
    SET ProductName = @ProductName, Description = @Description
    WHERE ProductId = @ProductId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 8/12/2024 3:21:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUser]
    @UserId INT,
    @UserName NVARCHAR(50),
    @Address NVARCHAR(100)
AS
BEGIN
    UPDATE users
    SET UserName = @UserName, Address = @Address
    WHERE UserId = @UserId;
END
GO
USE [master]
GO
ALTER DATABASE [Company_Info] SET  READ_WRITE 
GO
    
