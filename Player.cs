using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Player : Entity {

        private readonly int maxMana;

        private int experience;
        private int currentMana;

        public Player(Texture2D texture, Vector2 location, Direction direction, int maxHealth, int maxMana, int experience, int velocity) :
            base(texture, location, direction, maxHealth, velocity) {
            this.maxMana = maxMana;
            this.experience = experience;
            currentMana = maxMana;
        }

        public int getMaxMana() {
            return maxMana;
        }

        public int getExperience() {
            return experience;
        }

        public int getCurrentMana() {
            return currentMana;
        }
    }
}
