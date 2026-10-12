
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;
USE [jumptest];
GO

CREATE INDEX rwk_app_test_case ON app.test_case (test_plan_id, code, title);


