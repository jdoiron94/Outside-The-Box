using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles death of the player
    /// </summary>

    public class DeathManager {

        private InputManager inputManager;

        private int totalExp;
        private int currentExp;
        private int health;
        private int mana;
        private int totalMana;

        private int levelMode;
        private List<Npc> npcs;
        private List<Vector2> npcLocations;
        private List<Vector2> objectLocations;
        private List<bool> doorsUnlocked;

        public DeathManager(InputManager inputManager) {
            this.inputManager = inputManager;
            totalExp = inputManager.getPlayerManager().getTotalExperience();
            currentExp = inputManager.getPlayerManager().getCurrentExperience();
            health = inputManager.getPlayerManager().getHealth();
            mana = inputManager.getPlayerManager().getMana();
            totalMana = inputManager.getPlayerManager().getTotalMana();
            levelMode = inputManager.getLevel().getMode();
            npcs = new List<Npc>();
            npcLocations = new List<Vector2>();
            populateNpcList();
            objectLocations = new List<Vector2>();
            populateObjectLocationsList();
            doorsUnlocked = new List<bool>();
            populateDoorsUnlockedList();
        }

        /// <summary>
        /// Handles populating the npc list
        /// </summary>
        private void populateNpcList() {
            foreach (Npc n in inputManager.getLevel().getNpcs()) {
                npcs.Add(n);
                npcLocations.Add(n.getLocation());
            }
        }

        /// <summary>
        /// Handles populating the object list
        /// </summary>
        private void populateObjectLocationsList() {
            foreach (GameObject o in inputManager.getLevel().getObjects()) {
                objectLocations.Add(o.getLocation());
            }
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
            inputManager.getPlayer().setLocation(inputManager.getLevel().getPlayerOrigin());
            inputManager.getPlayerManager().setTotalExp(totalExp);
            inputManager.getPlayerManager().setCurrentExp(currentExp);
            inputManager.getPlayerManager().setHealth(health);
            inputManager.getPlayerManager().setMana(mana);
            inputManager.getPlayerManager().setTotalMana(totalMana);
        }

        /// <summary>
        /// Handles resetting the level
        /// </summary>
        public void resetLevel() {
            inputManager.getLevel().resetNpcs(npcs, npcLocations);
            inputManager.getLevel().resetObjects(objectLocations);
            inputManager.getLevel().resetDoors(doorsUnlocked);
            inputManager.getLevel().resetCollectibles();
        }
    }
}
