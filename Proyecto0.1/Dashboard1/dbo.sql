USE [master]
GO

/****** Object:  Database [dbo]    Script Date: 19/05/2019 10:10:01 p. m. ******/
CREATE DATABASE [dbo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\dbo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\dbo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [dbo] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [dbo] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [dbo] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [dbo] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [dbo] SET ARITHABORT OFF 
GO

ALTER DATABASE [dbo] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [dbo] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [dbo] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [dbo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [dbo] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [dbo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [dbo] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [dbo] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [dbo] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [dbo] SET  DISABLE_BROKER 
GO

ALTER DATABASE [dbo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [dbo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [dbo] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [dbo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [dbo] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [dbo] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [dbo] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [dbo] SET RECOVERY FULL 
GO

ALTER DATABASE [dbo] SET  MULTI_USER 
GO

ALTER DATABASE [dbo] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [dbo] SET DB_CHAINING OFF 
GO

ALTER DATABASE [dbo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [dbo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [dbo] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [dbo] SET QUERY_STORE = OFF
GO

ALTER DATABASE [dbo] SET  READ_WRITE 
GO

