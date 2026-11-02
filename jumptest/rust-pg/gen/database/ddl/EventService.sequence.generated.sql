CREATE SEQUENCE core.event_service_identity AS BIGINT START WITH 1000 INCREMENT BY 1;
GRANT USAGE, SELECT ON SEQUENCE core.event_service_identity TO jumptest;
