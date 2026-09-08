DROP TABLE IF EXISTS `sw_endlessleaguedata`;

DROP TABLE IF EXISTS `sw_quickleaguedata`;

CREATE TABLE
    `sw_endlessleaguedata` (
        league_id BIGINT UNSIGNED NOT NULL PRIMARY KEY,
        group_id BIGINT UNSIGNED NOT NULL PRIMARY KEY,
        start_time BIGINT UNSIGNED NOT NULL DEFAULT 0,
        end_time BIGINT UNSIGNED NOT NULL DEFAULT 604800,
        num_up INTEGER UNSIGNED NOT NULL DEFAULT 0,
        num_dowm INTEGER UNSIGNED NOT NULL DEFAULT 0,
        num_in_group INTEGER UNSIGNED NOT NULL DEFAULT 0,
        num_in_league BIGINT UNSIGNED NOT NULL DEFAULT 18446744073709551615
    );

INSERT INTO
    `sw_endlessleaguedata` (league_id, num_up, num_down, num_in_group)
VALUES
    ('0', '40', '0', '50'),
    ('1', '35', '0', '50'),
    ('2', '35', '0', '50'),
    ('3', '30', '0', '50'),
    ('4', '30', '0', '50'),
    ('5', '30', '0', '50'),
    ('6', '25', '0', '50'),
    ('7', '25', '0', '50'),
    ('8', '25', '0', '50'),
    ('9', '20', '7', '50'),
    ('10', '20', '7', '50'),
    ('11', '20', '7', '50'),
    ('12', '18', '8', '50'),
    ('13', '18', '8', '50'),
    ('14', '18', '8', '50'),
    ('15', '25', '25', '50'),
    ('16', '25', '25', '50'),
    ('17', '25', '25', '50'),
    ('18', '25', '25', '50'),
    ('19', '25', '25', '50'),
    ('20', '0', '25', '50');

CREATE TABLE
    `sw_quickleaguedata` (
        league_id BIGINT UNSIGNED NOT NULL PRIMARY KEY,
        group_id BIGINT UNSIGNED NOT NULL PRIMARY KEY,
        start_time BIGINT UNSIGNED NOT NULL DEFAULT 0,
        end_time BIGINT UNSIGNED NOT NULL DEFAULT 604800,
        num_up INTEGER UNSIGNED NOT NULL DEFAULT 0,
        num_dowm INTEGER UNSIGNED NOT NULL DEFAULT 0,
        num_in_group INTEGER UNSIGNED NOT NULL DEFAULT 0,
        num_in_league BIGINT UNSIGNED NOT NULL DEFAULT 18446744073709551615
    );

INSERT INTO
    `sw_quickleaguedata` (league_id, num_up, num_down, num_in_group)
VALUES
    ('0', '40', '0', '50'),
    ('1', '35', '0', '50'),
    ('2', '35', '0', '50'),
    ('3', '30', '0', '50'),
    ('4', '30', '0', '50'),
    ('5', '30', '0', '50'),
    ('6', '25', '0', '50'),
    ('7', '25', '0', '50'),
    ('8', '25', '0', '50'),
    ('9', '20', '7', '50'),
    ('10', '20', '7', '50'),
    ('11', '20', '7', '50'),
    ('12', '18', '8', '50'),
    ('13', '18', '8', '50'),
    ('14', '18', '8', '50'),
    ('15', '25', '25', '50'),
    ('16', '25', '25', '50'),
    ('17', '25', '25', '50'),
    ('18', '25', '25', '50'),
    ('19', '25', '25', '50'),
    ('20', '0', '25', '50');
