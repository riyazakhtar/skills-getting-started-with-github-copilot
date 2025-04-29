CREATE TABLE IF NOT EXISTS _migrations (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk__migrations PRIMARY KEY (migration_id)
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM _migrations WHERE "migration_id" = '20241101133644_Init') THEN
    CREATE TABLE products (
        id uuid NOT NULL,
        name text,
        CONSTRAINT pk_products PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM _migrations WHERE "migration_id" = '20241101133644_Init') THEN
    INSERT INTO _migrations (migration_id, product_version)
    VALUES ('20241101133644_Init', '9.0.1');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM _migrations WHERE "migration_id" = '20250212143557_AddProductType') THEN
    ALTER TABLE products DROP COLUMN name;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM _migrations WHERE "migration_id" = '20250212143557_AddProductType') THEN
    ALTER TABLE products ADD product_name text NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM _migrations WHERE "migration_id" = '20250212143557_AddProductType') THEN
    ALTER TABLE products ADD product_type_id uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM _migrations WHERE "migration_id" = '20250212143557_AddProductType') THEN
    CREATE TABLE product_names (
        id uuid NOT NULL,
        name text NOT NULL,
        CONSTRAINT pk_product_names PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM _migrations WHERE "migration_id" = '20250212143557_AddProductType') THEN
    CREATE TABLE product_types (
        id uuid NOT NULL,
        name text NOT NULL,
        CONSTRAINT pk_product_types PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM _migrations WHERE "migration_id" = '20250212143557_AddProductType') THEN
    CREATE INDEX ix_products_product_type_id ON products (product_type_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM _migrations WHERE "migration_id" = '20250212143557_AddProductType') THEN
    ALTER TABLE products ADD CONSTRAINT fk_products_product_types_product_type_id FOREIGN KEY (product_type_id) REFERENCES product_types (id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM _migrations WHERE "migration_id" = '20250212143557_AddProductType') THEN
    INSERT INTO _migrations (migration_id, product_version)
    VALUES ('20250212143557_AddProductType', '9.0.1');
    END IF;
END $EF$;
COMMIT;

