
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;
USE [jumptest];
GO

CREATE INDEX rwk_core_op_role_map ON core.op_role_map (op_id, op_role_id);


