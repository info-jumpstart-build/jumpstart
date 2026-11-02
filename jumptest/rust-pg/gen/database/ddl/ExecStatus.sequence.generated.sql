CREATE SEQUENCE core.exec_status_identity AS BIGINT START WITH 1000 INCREMENT BY 1;
GRANT USAGE, SELECT ON SEQUENCE core.exec_status_identity TO jumptest;
