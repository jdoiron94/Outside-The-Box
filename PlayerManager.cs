using System;

namespace KineticCamp {

    public class PlayerManager {

        private readonly Player player;
        private readonly DisplayBar healthBar;
        private readonly DisplayBar ManaBar;

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

        public PlayerManager(Player player, int health, int mana, int totalExp, int currentExp, DisplayBar healthBar, DisplayBar ManaBar) {
            this.player = player;
            this.health = health;
            this.mana = mana;
            this.totalExp = totalExp;
            this.currentExp = currentExp; 
            this.healthBar = healthBar;
            this.ManaBar = ManaBar;
            healthCooldown = 0;
            manaCooldown = 0;
            totalMana = 100; 
        }

        public PlayerManager(Player player, DisplayBar healthBar, DisplayBar ManaBar) :
            this(player, 100, 100, 0, 0, healthBar, ManaBar) {
        }

        #region gets and sets

        public Player getPlayerEntity() {
            return player;
        }

        public int getHealth() {
            return health;
        }

        public int getMana() {
            return mana;
        }

        public int getTotalExperience() {
            return totalExp;
        }

        public int getCurrentExperience() {
            return currentExp;
        }

        public DisplayBar getHealthBar() {
            return healthBar; 
        }

        public DisplayBar getManaBar()
        {
            return ManaBar; 
        }

        public int getHealthCooldown() {
            return healthCooldown; 
        }

        public int getManaDrainRate()
        {
            return manaDrainRate; 
        }

        public void setManaDrainRate(int manaDrainRate)
        {
            this.manaDrainRate = manaDrainRate;
            this.manaDrainMax = manaDrainRate + 1; 
        }

        #endregion

        #region Health Update Methods
        public void damagePlayer(int damage) {
            health = Math.Max(0, health - damage);
            healthBar.setWidth(Math.Max(0, (healthBar.getDisplayBar().Width - ((int) (damage * (200D / MAX_HEALTH))))));
        }

        public void regenerateHealth() {
            health = Math.Min(MAX_HEALTH, health + 1);
            healthBar.setWidth(Math.Min(health * 2, (healthBar.getDisplayBar().Width + ((int) (1 * (200D / MAX_HEALTH))))));
        }

        public void updateHealthCooldown() {
            healthCooldown = (healthCooldown + 1) % 36;
        }
        #endregion

        #region Mana Update Methods

        public void depleteMana(int cost)
        {
            mana = Math.Max(0, mana - cost);
            ManaBar.setWidth(Math.Max(0, (ManaBar.getDisplayBar().Width - ((int)(cost * (200D / MAX_HEALTH))))));
        }

        public void regenerateMana()
        {
            mana = Math.Min(totalMana, mana + 1);
            ManaBar.setWidth(Math.Min(mana * 2, (ManaBar.getDisplayBar().Width + ((int)(1 * (200D / totalMana))))));
        }

        public void updateManaCooldown()
        {
            manaCooldown = (manaCooldown + 1) % 36; 
        }

        public void updateManaDrainRate()
        {
            manaDrainRate = (manaDrainRate + 1) % (manaDrainMax); 
        }

        #endregion

    }
}