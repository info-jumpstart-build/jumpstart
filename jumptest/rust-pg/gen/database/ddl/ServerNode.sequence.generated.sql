CREATE SEQUENCE core.server_node_identity AS BIGINT START WITH 1000 INCREMENT BY 1;
GRANT USAGE, SELECT ON SEQUENCE core.server_node_identity TO jumptest;
