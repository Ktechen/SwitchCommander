USE master;
GO

-- Check if the database already exists and drop it if it does
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Hangfire')
BEGIN
    ALTER DATABASE Hangfire
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Hangfire;
END

-- Create the Hangfire database
CREATE DATABASE Hangfire;
GO
