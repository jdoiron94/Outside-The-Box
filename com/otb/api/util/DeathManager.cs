using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles death of the player
    /// </summary>

    public class DeathManager {

        private InputManager inputManager;
        
        private int currentExp;
        private int health;
        private int mana;
        private int totalMana;

        private int levelMode;
        private List<Npc> npcs;
        private List<bool> doorsUnlocked;

        public DeathManager(InputManager inputManager) {
            this.inputManager = inputManager;
            this.currentExp = inputManager.getPlayerManager().getExperience();
            this.health = inputManager.getPlayerManager().getHealth();
            this.mana = inputManager.getPlayerManager().getMana();
            this.totalMana = inputManager.getPlayerManager().getTotalMana();
            this.levelMode = inputManager.getLevel().getMode();
            this.npcs = new List<Npc>();
            this.doorsUnlocked = new List<bool>();
            this.populateDoorsUnlockedList();
        }

        /// <summary>
        /// Handles populating the door list
        /// </summary>
        private void populateDoorsUnlockedList() {
            foreach (Door d in inputManager.getLevel().getDoors()) {
                doorsUnlocked.Add(d.isUnlocked());
            }
        }

        /// <summary>
        /// Handles resetting the game
        /// </summary>
        public void resetGame() {
            resetPlayer();
            resetLevel();
        }

        /// <summary>
        /// Handles resetting the player
        /// </summary>
        public void resetPlayer() {
            int exp = inputManager.getPlayerManager().getExperience() / 2;
            inputManager.getPlayer().setLocation(inputManager.getLevel().getPlayerOrigin());
            inputManager.getPlayerManager().setExperience(exp);
            PauseMenu pause = (PauseMenu) inputManager.getLevel().getScreen("Pause");
            pause.setExperience(exp);
            inputManager.getPlayerManager().setHealth(health);
            inputManager.getPlayerManager().setMana(mana);
            inputManager.getPlayerManager().setTotalMana(totalMana);
            inputManager.getPlayer().updateStill();
        }

        /// <summary>
        /// Handles resetting the level
        /// </summary>
        public void resetLevel() {
            inputManager.getLevel().resetNpcs();
            inputManager.getLevel().resetObjects();
            inputManager.getLevel().resetDoors(doorsUnlocked);
            inputManager.getLevel().resetCollectibles();
        }
    }
}
