
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;
USE [jumptest];
GO

CREATE INDEX rwk_core_event_service ON core.event_service (event_type, objectname_filter, methodname_filter, script_id);


