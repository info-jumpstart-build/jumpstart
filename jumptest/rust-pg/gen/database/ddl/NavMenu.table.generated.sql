

CREATE TABLE core.nav_menu (
		id BIGINT  not null,
		parent_id BIGINT  not null,
		ordinal INTEGER  not null,
		name VARCHAR(1000)  not null,
		link VARCHAR(1000) ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.nav_menu (id, is_active);
