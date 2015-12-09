using System.Collections.Generic;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles death of the player
    /// </summary>

    public class DeathManager {

        private int health;
        private int mana;
        private int totalMana;
        private int levelMode;

        private readonly InputManager inputManager;

        public DeathManager(InputManager inputManager) {
            this.inputManager = inputManager;
            this.health = inputManager.getPlayerManager().getHealth();
            this.mana = inputManager.getPlayerManager().getMana();
            this.totalMana = inputManager.getPlayerManager().getTotalMana();
            this.levelMode = inputManager.getLevel().getMode();
        }

        /// <summary>
        /// Handles resetting the game
        /// </summary>
        public void resetGame(int deaths) {
            resetPlayer(deaths);
            resetLevel(inputManager.getLevel(), deaths);
        }

        /// <summary>
        /// Handles resetting the player
        /// </summary>
        public void resetPlayer(int deaths) {
            if (deaths == 3) {
                health = 100;
                mana = 100;
                totalMana = 100;
                levelMode = 0;
            }
            int exp = deaths == 3 ? 0 : inputManager.getPlayerManager().getExperience() / 2;
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
        public void resetLevel(Level level, int deaths) {
            level.resetNpcs();
            level.resetObjects();
            if (deaths != 3) {
                level.resetDoors();
            }
            level.resetCollectibles();
        }
    }
}
