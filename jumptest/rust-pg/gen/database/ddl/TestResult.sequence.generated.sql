CREATE SEQUENCE app.test_result_identity AS BIGINT START WITH 1000 INCREMENT BY 1;
GRANT USAGE, SELECT ON SEQUENCE app.test_result_identity TO jumptest;
