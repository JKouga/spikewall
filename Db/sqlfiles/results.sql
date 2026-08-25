DROP TABLE IF EXISTS `sw_gameresults`;

DROP TABLE IF EXISTS `sw_quickgameresults`;

DROP TABLE IF EXISTS `sw_userresults`;

CREATE TABLE
  `sw_gameresults` (
    gameid BIGINT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    uid BIGINT UNSIGNED NOT NULL,
    score BIGINT NOT NULL,
    rings BIGINT NOT NULL,
    red_rings BIGINT NOT NULL,
    distance BIGINT NOT NULL,
    daily_challenge_value BIGINT NOT NULL,
    daily_challenge_complete TINYINT NOT NULL,
    animals BIGINT NOT NULL,
    max_combo INTEGER NOT NOULL,
    closed INYINT NOT NULL,
    boss_destroyed TINYINT NOT NULL,
    chapter_clear TINYINT NOT NULL,
    get_chao_egg TINYINT NOT NULL,
    boss_hits BIGINT NOT NULL,
    reach_point BIGINT NOT NULL,
    event_id BIGINT,
    event_value INTEGER,
    cheat_result VARCHAR(8) NOT NULL
  )
CREATE TABLE
  `sw_quickgameresults` (
    gameid BIGINT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    uid BIGINT UNSIGNED NOT NULL,
    score BIGINT NOT NULL,
    rings BIGINT NOT NULL,
    red_rings BIGINT NOT NULL,
    distance BIGINT NOT NULL,
    daily_challenge_value BIGINT NOT NULL,
    daily_challenge_complete TINYINT NOT NULL,
    animals BIGINT NOT NULL,
    max_combo INTEGER NOT NOULL,
    closed TINYINT NOT NULL,
    cheat_result VARCHAR(8) NOT NULL
  )
CREATE TABLE
  `sw_userresults` (
    id BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    high_total_score UNSIGNED BIGINT,
    high_quick_total_score UNSIGNED BIGINT,
    total_rings UNSIGNED BIGINT,
    total_red_rings UNSIGNED BIGINT,
    chao_roulette_spin_count UNSIGNED BIGINT,
    roulette_spin_count UNSIGNED BIGINT,
    num_jackpots UNSIGNED BIGINT,
    best_jackpot UNSIGNED BIGINT,
    support INTEGER
  );