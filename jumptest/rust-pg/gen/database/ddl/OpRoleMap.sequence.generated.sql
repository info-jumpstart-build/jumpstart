CREATE SEQUENCE core.op_role_map_identity AS BIGINT START WITH 1000 INCREMENT BY 1;
GRANT USAGE, SELECT ON SEQUENCE core.op_role_map_identity TO jumptest;
