using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Screen {

        /*
         * Class which is meant to represent a given screen.
         */

        private readonly string name;

        private bool active;

        public Screen(string name, bool active) {
            this.name = name;
            this.active = active;
        }

        /*
         * Returns the screen's name
         */
        public string getName() {
            return name;
        }

        /*
         * Returns true if the screen is currently active; otherwise, false
         */
        public bool isActive() {
            return active;
        }

        /*
         * Sets the screen's active status to the given boolean
         */
        public void setActive(bool active) {
            this.active = active;
        }

        /*
         * Overridable method which handles updating the screen
         */
        public virtual void update(GameTime time) {
        }

        /*
         * Overridable method which handles drawing the screen
         */
        public virtual void draw(SpriteBatch batch) {
        }
    }
}
