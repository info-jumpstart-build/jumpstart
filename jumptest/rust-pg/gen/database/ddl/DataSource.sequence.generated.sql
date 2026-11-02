CREATE SEQUENCE core.data_source_identity AS BIGINT START WITH 1000 INCREMENT BY 1;
GRANT USAGE, SELECT ON SEQUENCE core.data_source_identity TO jumptest;
