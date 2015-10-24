using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class Player : Entity {

        /*
         * Class representing the player
         */

        private readonly int maxMana;

        private int experience;
        private int currentMana;

        public Player(Texture2D texture, Vector2 location, Direction direction, int maxHealth, int maxMana, int experience, int velocity) :
            base(texture, location, direction, maxHealth, velocity) {
            this.maxMana = maxMana;
            this.experience = experience;
            this.location = location;
            currentMana = maxMana;
        }

        /// <summary>
        /// Returns the player's maximum mana
        /// </summary>
        /// <returns>Returns the player's maximum mana</returns>
        public int getMaxMana() {
            return maxMana;
        }

        /// <summary>
        /// Returns the player's experience
        /// </summary>
        /// <returns>Returns the player's experience</returns>
        public int getExperience() {
            return experience;
        }

        /// <summary>
        /// Returns the player's current mana
        /// </summary>
        /// <returns>Returns the player's current mana</returns>
        public int getCurrentMana() {
            return currentMana;
        }
    }
}
