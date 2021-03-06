USE [master]
GO
/****** Object:  Database [BrandsGoods_Stock]    Script Date: 22/10/2021 20:34:18 ******/
CREATE DATABASE [BrandsGoods_Stock]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BrandsGoods_Stock', FILENAME = N'C:\temp\BrandsGoods_Stock.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BrandsGoods_Stock_log', FILENAME = N'C:\temp\BrandsGoods_Stock_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BrandsGoods_Stock] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BrandsGoods_Stock].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BrandsGoods_Stock] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET ARITHABORT OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BrandsGoods_Stock] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BrandsGoods_Stock] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BrandsGoods_Stock] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BrandsGoods_Stock] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET RECOVERY FULL 
GO
ALTER DATABASE [BrandsGoods_Stock] SET  MULTI_USER 
GO
ALTER DATABASE [BrandsGoods_Stock] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BrandsGoods_Stock] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BrandsGoods_Stock] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BrandsGoods_Stock] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BrandsGoods_Stock] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BrandsGoods_Stock] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BrandsGoods_Stock', N'ON'
GO
ALTER DATABASE [BrandsGoods_Stock] SET QUERY_STORE = OFF
GO
USE [BrandsGoods_Stock]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 22/10/2021 20:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[articleID] [int] IDENTITY(1,1) NOT NULL,
	[articleCode] [int] NOT NULL,
	[articleName] [nvarchar](50) NOT NULL,
	[articlePrice] [float] NOT NULL,
	[articleAmount] [int] NOT NULL,
	[articleState] [bit] NOT NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[articleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 22/10/2021 20:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[clientID] [int] IDENTITY(1,1) NOT NULL,
	[clientCode] [char](4) NOT NULL,
	[clientName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Client_1] PRIMARY KEY CLUSTERED 
(
	[clientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Request]    Script Date: 22/10/2021 20:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request](
	[requestID] [int] IDENTITY(1,1) NOT NULL,
	[clientID] [int] NOT NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[requestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestDetail]    Script Date: 22/10/2021 20:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestDetail](
	[requestDetailID] [int] IDENTITY(1,1) NOT NULL,
	[requestID] [int] NOT NULL,
	[articleID] [int] NOT NULL,
	[articleQuantity] [int] NOT NULL,
 CONSTRAINT [PK_RequestDetail_1] PRIMARY KEY CLUSTERED 
(
	[requestDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 22/10/2021 20:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[userNumber] [int] NOT NULL,
	[passWord] [nchar](6) NOT NULL,
	[admin] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Article] ON 

INSERT [dbo].[Article] ([articleID], [articleCode], [articleName], [articlePrice], [articleAmount], [articleState]) VALUES (1, 1, N'Álcool gel', 0.8, 50, 1)
INSERT [dbo].[Article] ([articleID], [articleCode], [articleName], [articlePrice], [articleAmount], [articleState]) VALUES (2, 2, N'Respiradores FFP2', 1.5, 0, 1)
INSERT [dbo].[Article] ([articleID], [articleCode], [articleName], [articlePrice], [articleAmount], [articleState]) VALUES (4, 3, N'Máscaras Cirúrgicas', 0.9, 10, 1)
SET IDENTITY_INSERT [dbo].[Article] OFF
GO
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([clientID], [clientCode], [clientName]) VALUES (3, N'1   ', N'Primavera BSS')
INSERT [dbo].[Client] ([clientID], [clientCode], [clientName]) VALUES (4, N'2   ', N'Oracle')
INSERT [dbo].[Client] ([clientID], [clientCode], [clientName]) VALUES (5, N'3   ', N'Google')
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Request] ON 

INSERT [dbo].[Request] ([requestID], [clientID]) VALUES (1, 5)
SET IDENTITY_INSERT [dbo].[Request] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([userID], [userName], [userNumber], [passWord], [admin]) VALUES (2, N'Joao Admin', 1, N'123456', 1)
INSERT [dbo].[User] ([userID], [userName], [userNumber], [passWord], [admin]) VALUES (4, N'Joao Standard', 2, N'654321', 0)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User]    Script Date: 22/10/2021 20:34:19 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [IX_User] UNIQUE NONCLUSTERED 
(
	[userName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Client] FOREIGN KEY([clientID])
REFERENCES [dbo].[Client] ([clientID])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Client]
GO
ALTER TABLE [dbo].[RequestDetail]  WITH CHECK ADD  CONSTRAINT [FK_RequestDetail_Article] FOREIGN KEY([articleID])
REFERENCES [dbo].[Article] ([articleID])
GO
ALTER TABLE [dbo].[RequestDetail] CHECK CONSTRAINT [FK_RequestDetail_Article]
GO
ALTER TABLE [dbo].[RequestDetail]  WITH CHECK ADD  CONSTRAINT [FK_RequestDetail_Request] FOREIGN KEY([requestID])
REFERENCES [dbo].[Request] ([requestID])
GO
ALTER TABLE [dbo].[RequestDetail] CHECK CONSTRAINT [FK_RequestDetail_Request]
GO
USE [master]
GO
ALTER DATABASE [BrandsGoods_Stock] SET  READ_WRITE 
GO
