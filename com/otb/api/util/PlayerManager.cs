using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents a player manager
    /// </summary>

    public class PlayerManager {
        
        private readonly Player player;
        private readonly DisplayBar healthBar;
        private readonly DisplayBar manaBar;
        private List<BasePower> powers;

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

        public PlayerManager(Player player, ContentManager cm, int health, int mana, int totalExp, int currentExp, DisplayBar healthBar, DisplayBar manaBar) {
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
            Dash dash = new Dash(true, false);
            dash.setSoundEffect(cm.Load<SoundEffect>("audio/Sound Effects/dashSound"));
            powers = new List<BasePower> { new SlowTime(true, false), dash, new Confuse(true, false) /*, new Mindread(true, false, inputManager)*/};
        }

        public PlayerManager(Player player, ContentManager cm, DisplayBar healthBar, DisplayBar manaBar) :
            this(player, cm, 100, 100, 0, 0, healthBar, manaBar) {
        }

        /// <summary>
        /// Returns an instance of the player
        /// </summary>
        /// <returns>Returns an instance of the player</returns>
        public Player getPlayer() {
            return player;
        }

        /// <summary>
        /// Returns a list of the player's abilities
        /// </summary>
        /// <returns>Returns the player's abilities</returns>
        public List<BasePower> getPowers() {
            return powers;
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
        /// Returns the player's total mana
        /// </summary>
        /// <returns></returns>
        public int getTotalMana() {
            return totalMana;
        }

        /// <summary>
        /// Sets the player's total mana
        /// </summary>
        /// <param name="totalMana">The mana to set</param>
        public void setTotalMana(int totalMana) {
            this.totalMana = totalMana;
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
            player.deriveHealth(-2);
            healthBar.setWidth((health = Math.Max(0, health - damage)) * 2);
        }

        /// <summary>
        /// Regenerates the appropriate amount of health for the player, based on their total experience
        /// </summary>
        public void regenerateHealth() {
            player.deriveHealth(2);
            healthBar.setWidth((health = Math.Min(MAX_HEALTH, health + 1 + Math.Min(9, totalExp / 100))) * 2);
        }

        /// <summary>
        /// Updates the player's health cooldown
        /// </summary>
        public void updateHealthCooldown() {
            healthCooldown = (healthCooldown + 1) % 36;
        }

        /// <summary>
        /// Sets the player's total exp
        /// </summary>
        /// <param name="exp">The exp to set</param>
        public void setTotalExp(int exp) {
            totalExp = exp;
        }

        /// <summary>
        /// Sets the player's current exp
        /// </summary>
        /// <param name="exp">The exp to set</param>
        public void setCurrentExp(int exp) {
            currentExp = exp;
        }

        /// <summary>
        /// Sets the player's health
        /// </summary>
        /// <param name="h">The health to set</param>
        public void setHealth(int h) {
            health = h;
        }

        /// <summary>
        /// Sets the player's mana
        /// </summary>
        /// <param name="m">The mana to set</param>
        public void setMana(int m) {
            mana = m;
        }

        /// <summary>
        /// Depletes the specified amount of mana
        /// </summary>
        /// <param name="mana">The amount of mana to deplete</param>
        public void depleteMana(int damage) {
            mana = Math.Max(0, mana - damage);
            int w_mod = (int) (200D * damage) / totalMana;
            int width = manaBar.getWidth();
            manaBar.setWidth(Math.Max(0, (width) - w_mod));
        }

        /// <summary>
        /// Regenerates the appropriate amount of mana for the player, based on their total experience
        /// </summary>
        public void regenerateMana() {
            int regeneration = (int) (totalMana * .01);
            mana = Math.Min(totalMana, mana + regeneration);
            manaBar.setWidth(Math.Min(200, (manaBar.getWidth() + ((int) (regeneration * (200D / totalMana))))));
        }

        /// <summary>
        /// Updates the player's mana cooldown
        /// </summary>
        public void updateManaCooldown() {
            manaCooldown = (manaCooldown + 1) % manaDrainMax;
        }

        /// <summary>
        /// Updates the player's mana drain rate
        /// </summary>
        public void updateManaDrainRate() {
            manaDrainRate = (manaDrainRate + 1) % manaDrainMax;
        }

        /// <summary>
        /// Levels up the player's mana
        /// </summary>
        /// <param name="percentageValue">The percent to up mana by</param>
        public void levelMana(double percentageValue) {
            int newValue = totalMana + (int) (totalMana * percentageValue);
            setTotalMana(newValue);
        }

        /// <summary>
        /// Adds to the player's exp
        /// </summary>
        /// <param name="bonus">The amount of exp to increment</param>
        public void incrementExperience(int bonus) {
            totalExp += bonus;
            currentExp += bonus;
        }

        /// <summary>
        /// Depletes spendable exp if you have enough
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>Returns true if the player has enough exp to spend; otherwise, false</returns>
        public bool spendExperience(int amount) {
            if ((currentExp - amount) < 0) {
                Console.WriteLine("Not enough EXP to spend");
                return false;
            } else {
                currentExp -= amount;
                return true;
            }
        }
    }
}
