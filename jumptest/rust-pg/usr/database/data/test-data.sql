-- jumptest seed data (hand-written; usr/database/data).
--
-- Applied automatically by `make database` (via load-usr.sh, after the
-- generated static data). Can also be run by hand:
--   cd usr/database/data && ./load-usr.sh
-- or directly:
--   psql "<your-connection>" -f usr/database/data/test-data.sql
--
-- IDs are explicit and < 1000 (the identity sequences start at 1000), so they
-- do not collide with application-generated rows. Audited tables carry txn_id;
-- the global audit columns (is_active/created_by/last_updated/last_updated_by)
-- are set here the same way the generated static-data seed sets them.
--
-- Every insert is idempotent (ON CONFLICT (txn_id) DO NOTHING; txn_id is the
-- primary key on these audited tables), so this file is safe to apply
-- repeatedly and on every build.

-- Required: the three result statuses referenced by the test-run generate op.
insert into app.test_result_status (id, txn_id, name, is_active, created_by, last_updated, last_updated_by)
  values (1, 1, 'Unexecuted', 1, current_user, current_timestamp, current_user)
  on conflict (txn_id) do nothing;
insert into app.test_result_status (id, txn_id, name, is_active, created_by, last_updated, last_updated_by)
  values (2, 2, 'Pass', 1, current_user, current_timestamp, current_user)
  on conflict (txn_id) do nothing;
insert into app.test_result_status (id, txn_id, name, is_active, created_by, last_updated, last_updated_by)
  values (3, 3, 'Fail', 1, current_user, current_timestamp, current_user)
  on conflict (txn_id) do nothing;

-- Sample test plan.
insert into app.test_plan (id, txn_id, name, description, is_active, created_by, last_updated, last_updated_by)
  values (1, 1, 'Rust Backend Smoke Plan', 'Initial smoke coverage for the Rust server tier.', 1, current_user, current_timestamp, current_user)
  on conflict (txn_id) do nothing;

-- Sample test cases under plan 1.
insert into app.test_case (id, txn_id, test_plan_id, code, area, title, preconditions, steps, expected_result, priority, component, is_active, created_by, last_updated, last_updated_by)
  values (1, 1, 1, 'CRUD-01', 'CRUD', 'Create record via API', 'API running; authorized principal', 'POST /api/<obj> with a JSON view body (no id)', '200; new id returned; row persisted with is_active=1', 'P1', 'api+logic+persist', 1, current_user, current_timestamp, current_user)
  on conflict (txn_id) do nothing;
insert into app.test_case (id, txn_id, test_plan_id, code, area, title, preconditions, steps, expected_result, priority, component, is_active, created_by, last_updated, last_updated_by)
  values (2, 2, 1, 'RT-01', 'Real-time', 'SSE stream connects', 'API running', 'curl -N /api/notification/stream then update a record', 'Prints : connected, then a PropertyUpdated event on update', 'P1', 'api+logic', 1, current_user, current_timestamp, current_user)
  on conflict (txn_id) do nothing;

-- Sample test run for plan 1. On the Test Plans page, right-click a plan and
-- choose Generate to create a Test Run plus one Unexecuted result per case.
insert into app.test_run (id, txn_id, name, test_plan_id, run_at, run_by, notes, is_active, created_by, last_updated, last_updated_by)
  values (1, 1, 'Smoke Run 1', 1, current_timestamp, current_user, 'Seeded sample run.', 1, current_user, current_timestamp, current_user)
  on conflict (txn_id) do nothing;
