using System;

namespace KineticCamp {

    public class PlayerManager {

        private readonly Player player;
        private readonly DisplayBar healthBar;
        //private DisplayBar ManaBar;

        private int health;
        private int mana;
        private int totalExp;
        private int currentExp;
        private int healthCooldown;

        private const byte MAX_HEALTH = 0x64;

        public PlayerManager(Player player, int health, int mana, int totalExp, int currentExp, DisplayBar healthBar) {
            this.player = player;
            this.health = health;
            this.mana = mana;
            this.totalExp = totalExp;
            this.currentExp = currentExp; 
            this.healthBar = healthBar;
            healthCooldown = 0; 
        }

        public PlayerManager(Player player, DisplayBar healthBar) :
            this(player, 100, 100, 0, 0, healthBar) {
        }

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

        public int getHealthCooldown() {
            return healthCooldown; 
        }

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
    }
}