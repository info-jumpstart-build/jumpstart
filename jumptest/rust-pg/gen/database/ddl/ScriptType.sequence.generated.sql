CREATE SEQUENCE core.script_type_identity AS BIGINT START WITH 1000 INCREMENT BY 1;
GRANT USAGE, SELECT ON SEQUENCE core.script_type_identity TO jumptest;
