using System;

namespace KineticCamp {

    public class PlayerManager {

        private readonly Player player;
        private readonly DisplayBar healthBar;
        private readonly DisplayBar manaBar;

        private int health;
        private int mana;
        private int totalMana;
        private int totalExp;
        private int currentExp;
        private int healthCooldown;
        private int manaCooldown;
        private int manaDrainRate;
        private int manaDrainMax;

        private const byte MAX_HEALTH = 0x64;

        public PlayerManager(Player player, int health, int mana, int totalExp, int currentExp, DisplayBar healthBar, DisplayBar manaBar) {
            this.player = player;
            this.health = health;
            this.mana = mana;
            this.totalExp = totalExp;
            this.currentExp = currentExp; 
            this.healthBar = healthBar;
            this.manaBar = manaBar;
            healthCooldown = 0;
            manaCooldown = 0;
            totalMana = 100;
        }

        public PlayerManager(Player player, DisplayBar healthBar, DisplayBar manaBar) :
            this(player, 100, 100, 0, 0, healthBar, manaBar) {
        }

        /// <summary>
        /// Returns an instance of the player
        /// </summary>
        /// <returns>Returns an instance of the player</returns>
        public Player getPlayer() {
            return player;
        }

        /// <summary>
        /// Returns the player's health
        /// </summary>
        /// <returns>Returns the player's health</returns>
        public int getHealth() {
            return health;
        }

        /// <summary>
        /// Returns the player's mana
        /// </summary>
        /// <returns>Returns the player's mana</returns>
        public int getMana() {
            return mana;
        }

        /// <summary>
        /// Returns the player's total experience
        /// </summary>
        /// <returns>Returns the player's total experience</returns>
        public int getTotalExperience() {
            return totalExp;
        }

        /// <summary>
        /// Returns the player's current experience
        /// </summary>
        /// <returns>Returns the player's current experience</returns>
        public int getCurrentExperience() {
            return currentExp;
        }

        /// <summary>
        /// Returns the player's health bar
        /// </summary>
        /// <returns>Returns the player's health bar</returns>
        public DisplayBar getHealthBar() {
            return healthBar; 
        }

        /// <summary>
        /// Returns the player's manabar
        /// </summary>
        /// <returns>Returns the player's mana bar</returns>
        public DisplayBar getManaBar() {
            return manaBar;
        }

        /// <summary>
        /// Returns the player's health cooldown
        /// </summary>
        /// <returns>Returns the player's health cooldown</returns>
        public int getHealthCooldown() {
            return healthCooldown; 
        }

        /// <summary>
        /// Returns the player's mana drain rate
        /// </summary>
        /// <returns>Returns the player's mana drain rate</returns>
        public int getManaDrainRate() {
            return manaDrainRate;
        }

        /// <summary>
        /// Sets the player's mana drain rate
        /// </summary>
        /// <param name="manaDrainRate">The mana drain rate to be set</param>
        public void setManaDrainRate(int manaDrainRate) {
            this.manaDrainRate = manaDrainRate;
            manaDrainMax = manaDrainRate + 1;
        }

        /// <summary>
        /// Damages the player by the specified amount
        /// </summary>
        /// <param name="damage">The amount of damage to inflict</param>
        public void damagePlayer(int damage) {
            healthBar.setWidth((health = Math.Max(0, health - damage)) * 2);
        }

        /// <summary>
        /// Regenerates the appropriate amount of health for the player, based on their total experience
        /// </summary>
        public void regenerateHealth() {
            healthBar.setWidth((health = Math.Min(MAX_HEALTH, health + 1 + Math.Min(9, totalExp / 100))) * 2);
        }

        /// <summary>
        /// Updates the player's health cooldown
        /// </summary>
        public void updateHealthCooldown() {
            healthCooldown = (healthCooldown + 1) % 36;
        }

        /// <summary>
        /// Depletes the specified amount of mana
        /// </summary>
        /// <param name="mana">The amount of mana to deplete</param>
        public void depleteMana(int mana) {
            manaBar.setWidth((this.mana = Math.Max(0, this.mana - mana)) * 2);
        }

        /// <summary>
        /// Regenerates the appropriate amount of mana for the player, based on their total experience
        /// </summary>
        public void regenerateMana() {
            manaBar.setWidth((mana = Math.Min(totalMana, mana + 1 + Math.Min(9, totalExp / 100))) * 2);
        }

        /// <summary>
        /// Updates the player's mana cooldown
        /// </summary>
        public void updateManaCooldown() {
            manaCooldown = (manaCooldown + 1) % 36; 
        }

        /// <summary>
        /// Updates the player's mana drain rate
        /// </summary>
        public void updateManaDrainRate() {
            manaDrainRate = (manaDrainRate + 1) % manaDrainMax; 
        }
    }
}
