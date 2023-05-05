CREATE SCHEMA caf;

CREATE TABLE caf.anonymized_faces (
    identifier UUID PRIMARY KEY,
    embedding DOUBLE PRECISION[] NOT NULL
);