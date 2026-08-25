DROP TABLE IF EXISTS `sw_wheeloptions`;

DROP TABLE IF EXISTS `sw_wheeldata`;

CREATE TABLE
    `sw_wheeloptions` (
        user_id BIGINT UNSIGNED NOT NULL PRIMARY KEY,
        next_free_spin BIGINT NOT NULL DEFAULT 0,
        num_free_spins TINYINT NOT NULL DEFAULT 3,
        item_won INTEGER NOT NULL,
        roulette_rank TINYINT NOT NULL DEFAULT 0,
        num_jackpot_ring BIGINT NOT NULL DEFAULT 50000
    );

CREATE TABLE
    `sw_wheeldata` (
        id BIGINT UNSIGNED NOT NULL PRIMARY KEY,
        login_roulette_id INTEGER,
        roulette_period_end BIGINT NOT NULL,
        roulette_count_in_period INTEGER,
        got_jackpot_this_period INTEGER
    );