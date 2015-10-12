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

        /*
         * Returns the player instance
         */
        public Player getPlayerEntity() {
            return player;
        }

        /*
         * Returns the player's health
         */
        public int getHealth() {
            return health;
        }

        /*
         * Returns the player's mana
         */
        public int getMana() {
            return mana;
        }

        /*
         * Returns the player's total experience
         */
        public int getTotalExperience() {
            return totalExp;
        }

        /*
         * Returns the player's current experience
         */
        public int getCurrentExperience() {
            return currentExp;
        }

        /*
         * Returns the health bar
         */
        public DisplayBar getHealthBar() {
            return healthBar; 
        }

        /*
         * Returns the mana bar
         */
        public DisplayBar getManaBar() {
            return manaBar;
        }

        /*
         * Returns the health cooldown
         */
        public int getHealthCooldown() {
            return healthCooldown; 
        }

        /*
         * Returns the mana drain rate
         */
        public int getManaDrainRate() {
            return manaDrainRate;
        }

        /*
         * Sets the mana drain rate to the given rate
         */
        public void setManaDrainRate(int manaDrainRate) {
            this.manaDrainRate = manaDrainRate;
            manaDrainMax = manaDrainRate + 1;
        }

        /*
         * Damaes the player with the appropriate amount of damage
         */
        public void damagePlayer(int damage) {
            healthBar.setWidth((health = Math.Max(0, health - damage)) * 2);
        }

        /*
         * Regenerates the player's health accordingly
         */
        public void regenerateHealth() {
            healthBar.setWidth((health = Math.Min(MAX_HEALTH, health + 1 + Math.Min(9, totalExp / 100))) * 2);
        }

        /*
         * Updates the player's heath cooldown
         */
        public void updateHealthCooldown() {
            healthCooldown = (healthCooldown + 1) % 36;
        }

        /*
         * Depletes the player's mana according to the passed parameter
         */
        public void depleteMana(int mana) {
            manaBar.setWidth((this.mana = Math.Max(0, this.mana - mana)) * 2);
        }

        /*
         * Regenerates the player's mana accordingly
         */
        public void regenerateMana() {
            manaBar.setWidth((mana = Math.Min(totalMana, mana + 1 + Math.Min(9, totalExp / 100))) * 2);
        }

        /*
         * Updates the mana cooldown
         */
        public void updateManaCooldown() {
            manaCooldown = (manaCooldown + 1) % 36; 
        }

        /*
         * Updates the mana drain rate accordingly
         */
        public void updateManaDrainRate() {
            manaDrainRate = (manaDrainRate + 1) % manaDrainMax; 
        }
    }
}