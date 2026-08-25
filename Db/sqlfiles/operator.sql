DROP TABLE IF EXISTS `sw_operatormessages`;

DROP TABLE IF EXISTS `sw_operatorinfos`;

CREATE TABLE
  `sw_operatormessages` (
    id BIGINT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    for_all TINYINT UNSIGNED NOT NULL DEFAULT 0,
    userid BIGINT UNSIGNED NOT NULL,
    contents TEXT,
    item JSON,
    expire_time BIGINT NOT NULL
  );

CREATE TABLE
  `sw_operatorinfos` (
    uid BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    id BIGINT NOT NULL,
    param TEXT
  );