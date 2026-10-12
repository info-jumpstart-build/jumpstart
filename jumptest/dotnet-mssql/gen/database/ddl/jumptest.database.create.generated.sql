
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;
USE [master];
GO

-- Create the database only if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'jumptest')
BEGIN
    CREATE DATABASE [jumptest];
END

GO

-- Create a user in the database for the login (only if it doesn't exist)
USE [jumptest];
IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = 'jumptest')
BEGIN
    CREATE USER [jumptest] FOR LOGIN [jumptest];
END

-- Grant db_owner role to the user (this will work even if user already exists)
IF NOT EXISTS (SELECT 1 FROM sys.database_role_members drm
               INNER JOIN sys.database_principals dp ON drm.member_principal_id = dp.principal_id
               INNER JOIN sys.database_principals rp ON drm.role_principal_id = rp.principal_id
               WHERE dp.name = 'jumptest' AND rp.name = 'db_owner')
BEGIN
    ALTER ROLE db_owner ADD MEMBER [jumptest];
END

GO

-- Drop all user-created objects if database exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'jumptest')
BEGIN
    USE [jumptest];
    
    -- Disable foreign key constraints temporarily
    EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT ALL";
    
    -- Drop all user tables (excluding system tables)
    DECLARE @sql NVARCHAR(MAX) = '';
    SELECT @sql = @sql + 'DROP TABLE [' + SCHEMA_NAME(schema_id) + '].[' + name + '];' + CHAR(13)
    FROM sys.tables 
    WHERE type = 'U' -- User tables only
    AND name NOT LIKE 'sys%' -- Exclude system tables
    AND name NOT LIKE 'INFORMATION_SCHEMA%'; -- Exclude information schema
    
    IF LEN(@sql) > 0
    BEGIN
        EXEC sp_executesql @sql;
    END
    
    -- Drop all user sequences (excluding system sequences)
    DECLARE @seqSql NVARCHAR(MAX) = '';
    SELECT @seqSql = @seqSql + 'DROP SEQUENCE [' + SCHEMA_NAME(schema_id) + '].[' + name + '];' + CHAR(13)
    FROM sys.sequences 
    WHERE name NOT LIKE 'sys%' -- Exclude system sequences
    AND name NOT LIKE 'INFORMATION_SCHEMA%'; -- Exclude information schema
    
    IF LEN(@seqSql) > 0
    BEGIN
        EXEC sp_executesql @seqSql;
    END
    
    -- Re-enable foreign key constraints (for any remaining tables)
    EXEC sp_MSforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL";
END
GO

-- Create history schema only if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name = 'core')
BEGIN
    DECLARE @sql NVARCHAR(MAX) = 'CREATE SCHEMA [core]';
    EXEC sp_executesql @sql;
END
GO

use [jumptest];

CREATE SCHEMA core;
GO

CREATE TABLE core.log (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    timestamp DATETIME NOT NULL,
    level VARCHAR(50) NOT NULL,
    principalname VARCHAR(50) NOT NULL,
    program TEXT NOT NULL,
    filepath TEXT NOT NULL,
    linenumber INTEGER NOT NULL,
    membername TEXT NOT NULL,
    message TEXT NOT NULL
  
);
GO
