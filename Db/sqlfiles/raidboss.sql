DROP TABLE IF EXISTS `sw_raidbossstates`
CREATE TABLE
  `sw_raidbossstates` (
    uid BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    raidboss_rings INTEGER NOT NULL,
    raid_energy INTEGER NOT NULL,
    raid_energy_max INTEGER NOT NULL DEFAULT 3,
    num_beated_encounter INTEGER NOT NULL,
    num_beated_enterprise INTEGER NOT NULL,
    energy_renews_at BIGINT NOT NULL,
    score_until_next_raidboss BIGINT NOT NULL
  );