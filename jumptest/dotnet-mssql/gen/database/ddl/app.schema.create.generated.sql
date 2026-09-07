
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;
USE [jumptest];
GO

-- Create schema only if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name = 'app')
BEGIN
    DECLARE @sql NVARCHAR(MAX) = 'CREATE SCHEMA [app]';
    EXEC sp_executesql @sql;
END 


