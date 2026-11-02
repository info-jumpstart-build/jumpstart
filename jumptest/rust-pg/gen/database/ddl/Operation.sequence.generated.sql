CREATE SEQUENCE core.operation_identity AS BIGINT START WITH 1000 INCREMENT BY 1;
GRANT USAGE, SELECT ON SEQUENCE core.operation_identity TO jumptest;
