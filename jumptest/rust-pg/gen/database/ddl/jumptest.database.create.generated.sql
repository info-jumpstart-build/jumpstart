

DROP DATABASE IF EXISTS jumptest WITH ( FORCE );

CREATE DATABASE jumptest
    WITH
    OWNER = jumptest
    ENCODING = 'UTF8'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
    
GRANT CONNECT ON DATABASE jumptest TO jumptest;

\c jumptest

CREATE SCHEMA core; 
GRANT USAGE ON SCHEMA core TO jumptest; 
GRANT SELECT, UPDATE, INSERT, DELETE ON ALL TABLES IN SCHEMA core TO jumptest;

CREATE TABLE core.log (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    timestamp TIMESTAMPTZ NOT NULL,
    level TEXT NOT NULL,
    principalname  TEXT NOT NULL,
    program TEXT NOT NULL,
    filepath TEXT NOT NULL,
    linenumber INTEGER NOT NULL,
    membername TEXT NOT NULL,
    message TEXT NOT NULL
  
);
