using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Screen {

        /*
         * Class which is meant to represent a given screen.
         */

        private string name;
        private bool active;

        public Screen(string name, bool active) {
            this.name = name;
            this.active = active;
        }


        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
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
