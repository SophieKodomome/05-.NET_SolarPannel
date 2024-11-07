CREATE USER solar WITH PASSWORD 'solar';
ALTER USER solar WITH SUPERUSER;

CREATE DATABASE solar;

\c solar;

CREATE TABLE residence (
    id SERIAL PRIMARY KEY,
    address VARCHAR
);

CREATE TABLE residence_device (
    id SERIAL PRIMARY KEY,
    id_residence INT REFERENCES residence(id),
    device VARCHAR,
    consumption INT,
    start_hour INT,
    end_hour INT
);

CREATE TABLE semester (
    id SERIAL PRIMARY KEY,
    name VARCHAR,
    start_date DATE,
    end_date DATE
);

CREATE TABLE hourly_efficiency (
    id SERIAL PRIMARY KEY,
    id_semester INT REFERENCES semester(id),
    start_hour INT,
    end_hour INT,
    percentile_efficiency INT
);

CREATE TABLE solar_panel (
    id SERIAL PRIMARY KEY,
    name VARCHAR,
    price_per_watt INT
);

CREATE TABLE battery (
    id SERIAL PRIMARY KEY,
    name VARCHAR,
    price_per_watt INT,
    a_plat DECIMAL(10,2)
);

CREATE TABLE residence_consumption (
    id SERIAL PRIMARY KEY,
    id_residence INT REFERENCES residence(id),
    id_semester INT REFERENCES semester(id),
    solar_panel_power INT,
    battery_charge INT
);

CREATE TABLE bill(
    id SERIAL PRIMARY KEY,
    id_residence INT REFERENCES residence(id),
    id_semester INT REFERENCES semester(id),
    id_solar_panel INT REFERENCES solar_panel(id),
    solar_panel_price INT,
    id_battery INT REFERENCES battery(id),
    battery_price INT,
    total_price INT
);

INSERT INTO semester(name,start_date,end_date) VALUES ('semester 1','2000-03-15','2000-07-14');
INSERT INTO semester(name,start_date,end_date) VALUES ('semester 2','2000-07-15','2000-03-14');

INSERT INTO hourly_efficiency(id_semester,start_hour,end_hour,percentile_efficiency) VALUES (1,6,8,10);
INSERT INTO hourly_efficiency(id_semester,start_hour,end_hour,percentile_efficiency) VALUES (1,9,10,20);
INSERT INTO hourly_efficiency(id_semester,start_hour,end_hour,percentile_efficiency) VALUES (1,11,13,60);
INSERT INTO hourly_efficiency(id_semester,start_hour,end_hour,percentile_efficiency) VALUES (1,14,16,30);
INSERT INTO hourly_efficiency(id_semester,start_hour,end_hour,percentile_efficiency) VALUES (1,17,5,0);

INSERT INTO hourly_efficiency(id_semester,start_hour,end_hour,percentile_efficiency) VALUES (2,6,8,20);
INSERT INTO hourly_efficiency(id_semester,start_hour,end_hour,percentile_efficiency) VALUES (2,9,10,40);
INSERT INTO hourly_efficiency(id_semester,start_hour,end_hour,percentile_efficiency) VALUES (2,11,13,80);
INSERT INTO hourly_efficiency(id_semester,start_hour,end_hour,percentile_efficiency) VALUES (2,14,16,60);
INSERT INTO hourly_efficiency(id_semester,start_hour,end_hour,percentile_efficiency) VALUES (2,17,5,0);

INSERT INTO solar_panel(name,price_per_watt) VALUES ('Solar panel',400);

INSERT INTO battery(name,price_per_watt,a_plat) VALUES ('Lithium',800,0.7);
INSERT INTO battery(name,price_per_watt,a_plat) VALUES ('Gel',400,0.4);
INSERT INTO battery(name,price_per_watt,a_plat) VALUES ('simple',900,0.8);
