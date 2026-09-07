
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;
USE [jumptest];
GO

CREATE INDEX rwk_core_principal_org ON core.principal_org (org_id, principal_id);


