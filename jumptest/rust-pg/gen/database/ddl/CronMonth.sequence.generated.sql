CREATE SEQUENCE core.cron_month_identity AS BIGINT START WITH 1000 INCREMENT BY 1;
GRANT USAGE, SELECT ON SEQUENCE core.cron_month_identity TO jumptest;
