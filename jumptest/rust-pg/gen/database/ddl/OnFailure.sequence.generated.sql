CREATE SEQUENCE core.on_failure_identity AS BIGINT START WITH 1000 INCREMENT BY 1;
GRANT USAGE, SELECT ON SEQUENCE core.on_failure_identity TO jumptest;
