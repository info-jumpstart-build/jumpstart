
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;
USE [jumptest];
GO

<<<<<<< HEAD
CREATE INDEX rwk_core_principal ON core.principal (email, enabled);
=======
CREATE INDEX rwk_core_principal ON core.principal (email, principal_status_id);
>>>>>>> main


