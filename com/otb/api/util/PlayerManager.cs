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
        private readonly PowerBar powerbar;
        private KeyBox keyBox;
        private List<BasePower> powers;

        private int health;
        private int mana;
        private int totalMana;
        private int experience;
        private int healthCooldown;
        private int manaCooldown;
        private int manaDrainRate;
        private int manaDrainMax;

        private const int MAX_HEALTH = 100;
        private const int MAX_MANA = 500;

        private bool manaLimit;
        private bool healthLimit;

        public PlayerManager(Player player, ContentManager cm, int health, int mana, int experience, DisplayBar healthBar, DisplayBar manaBar, KeyBox keyBox, PowerBar powerbar) {
            this.player = player;
            this.health = health;
            this.mana = mana;
            this.experience = experience;
            this.healthBar = healthBar;
            this.manaBar = manaBar;
            this.keyBox = keyBox;
            this.powerbar = powerbar;
            this.totalMana = 100;
            SlowTime slow = new SlowTime(20, 200, 200);
            slow.setEffect(cm.Load<SoundEffect>("audio/Sound Effects/slowSound"));
            slow.setPlayerManager(this);
            Dash dash = new Dash(5, 20, 15);
            dash.setEffect(cm.Load<SoundEffect>("audio/Sound Effects/dashSound"));
            dash.setPlayerManager(this);
            Confuse confuse = new Confuse(20, 200, 50);
            confuse.setEffect(cm.Load<SoundEffect>("audio/Sound Effects/paralyzeSound"));
            confuse.setPlayerManager(this);
            powers = new List<BasePower> { slow, dash, confuse };
            this.manaLimit = true;
            this.healthLimit = true;
        }

        public PlayerManager(Player player, ContentManager cm, DisplayBar healthBar, DisplayBar manaBar, KeyBox keyBox, PowerBar powerbar) :
            this(player, cm, 100, 100, 0, healthBar, manaBar, keyBox, powerbar) {
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
        /// Returns the player's current experience
        /// </summary>
        /// <returns>Returns the player's current experience</returns>
        public int getExperience() {
            return experience;
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
        /// Returns the key box
        /// </summary>
        /// <returns>eturns the key box</returns>
        public KeyBox getKeyBox() {
            return keyBox;
        }

        /// <summary>
        /// Returns the power bar
        /// </summary>
        /// <returns>Returns the power bar</returns>
        public PowerBar getPowerBar() {
            return powerbar;
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
        /// Sets the mana limit
        /// </summary>
        /// <param name="value">The value to set</param>
        public void setManaLimit(bool value) {
            manaLimit = value;
        }

        /// <summary>
        /// Returns the mana limit
        /// </summary>
        /// <returns>Returns the mana limit</returns>
        public bool getManaLimit() {
            return manaLimit;
        }

        /// <summary>
        /// Sets the health limit
        /// </summary>
        /// <param name="value">The value to set</param>
        public void setHealthLimit(bool value) {
            healthLimit = value;
        }

        /// <summary>
        /// Returns the health limit
        /// </summary>
        /// <returns>Returns the health limit</returns>
        public bool getHealthLimit() {
            return healthLimit;
        }

        /// <summary>
        /// Damages the player by the specified amount
        /// </summary>
        /// <param name="damage">The amount of damage to inflict</param>
        public void damagePlayer(int damage) {
            health = Math.Max(0, health - damage);
            healthBar.update(health, MAX_HEALTH);
        }

        /// <summary>
        /// Regenerates the appropriate amount of health for the player, based on their total experience
        /// </summary>
        public void regenerateHealth() {
            health = Math.Min(MAX_HEALTH, health + 1);
            healthBar.update(health, MAX_HEALTH);
        }

        /// <summary>
        /// Updates the player's health cooldown
        /// </summary>
        public void updateHealthCooldown() {
            healthCooldown = (healthCooldown + 1) % 36;
        }

        /// <summary>
        /// Sets the player's current exp
        /// </summary>
        /// <param name="experience">The exp to set</param>
        public void setExperience(int experience) {
            this.experience = experience;
        }

        /// <summary>
        /// Sets the player's health
        /// </summary>
        /// <param name="health">The health to set</param>
        public void setHealth(int health) {
            this.health = health;
        }

        /// <summary>
        /// Sets the player's mana
        /// </summary>
        /// <param name="mana">The mana to set</param>
        public void setMana(int mana) {
            this.mana = mana;
        }

        /// <summary>
        /// Depletes the specified amount of mana
        /// </summary>
        /// <param name="mana">The amount of mana to deplete</param>
        public void depleteMana(int mana) {
            this.mana = Math.Max(0, this.mana - mana);
            manaBar.update(this.mana, totalMana);
        }

        /// <summary>
        /// Regenerates the appropriate amount of mana for the player, based on their total experience
        /// </summary>
        public void regenerateMana() {
            int regeneration = (int) (1 + experience * .001);
            mana = Math.Min(totalMana, mana + regeneration);
            manaBar.update(mana, totalMana);
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
        /// <param name="mana">The percent to up mana by</param>
        public void levelMana(int mana) {
            totalMana = Math.Min(MAX_MANA, (totalMana + mana));
        }

        /// <summary>
        /// Adds to the player's exp
        /// </summary>
        /// <param name="experience">The amount of exp to increment</param>
        public void incrementExperience(int experience) {
            this.experience += experience;
        }
    }
}
