CREATE TABLE IF NOT EXISTS "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL
);

INSERT INTO "Users"("Name") VALUES ('John Doe');
INSERT INTO "Users"("Name") VALUES ('Jane Smith');
INSERT INTO "Users"("Name") VALUES ('Bob Marely');
