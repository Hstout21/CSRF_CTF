USE [master]
GO

DROP DATABASE [SoftwareSecurityFinalProject]
GO

/* Create Database */

CREATE DATABASE [SoftwareSecurityFinalProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SoftwareSecurityFinalProject', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SoftwareSecurityFinalProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SoftwareSecurityFinalProject_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SoftwareSecurityFinalProject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SoftwareSecurityFinalProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET ARITHABORT OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET  DISABLE_BROKER 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET  MULTI_USER 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET DB_CHAINING OFF 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET QUERY_STORE = ON
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [SoftwareSecurityFinalProject] SET  READ_WRITE 
GO

/* Create Tables */

USE [SoftwareSecurityFinalProject]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Forum](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[message] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](max) NOT NULL,
	[password] [varchar](max) NOT NULL,
	[isAdmin] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/* Populate Tables */

USE [SoftwareSecurityFinalProject]
GO

INSERT INTO [dbo].[Users]
           ([email]
           ,[password]
           ,[isAdmin])
     VALUES
           ('hunter@admin.com','dummy',1),
		   ('joe@user.com','dummy',0),
		   ('sue@user.com','dummy',0),
		   ('derrick01@user.com','dummy',0),
		   ('boss@admin.com','dummy',1),
		   ('undercoveruser@suspicious.com','dummy',0)
GO

INSERT INTO [dbo].[Forum]
           ([userId]
           ,[message])
     VALUES
           (5,'Friendly reminder, please be in the office by 8am. Have a good night everyone.')
		   (2,'There are donuts in the breakroom!')
		   (3,'Hello, Im looking for volunteers for our local food drive this weekend! Please feel free to reach out.')
		   (5,'Hello everyone, excellent work this week. Have a good weekend and be safe!')
		   (4,'Hello everyone, Ive been getting amazing feedback from the buisness, keep up the good work.')
		   (3,'Hi guys, we are having an incident on the production line, please check your emails.')
		   (2,'Hello everyone! we will be having a party after work today.')
GO



