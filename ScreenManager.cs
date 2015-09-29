using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace KineticCamp {

    public class ScreenManager {

        /*
         * Class which represents a screen state manager, determining which screen is currently active.
         */

        private Screen activeScreen;
        private List<Screen> screens;

        public ScreenManager(Screen activeScreen, Screen[] screens) {
            this.activeScreen = activeScreen;
            this.screens = new List<Screen>(screens.Length);
            this.screens.AddRange(screens);
        }

        /*
         * Returns the active screen
         */
        public Screen getActiveScreen() {
            return activeScreen;
        }

        /*
         * Returns a list of all game screens
         */
        public List<Screen> getScreens() {
            return screens;
        }

        /*
         * Updates the active screen
         */
        public void update(GameTime time) {
            activeScreen.update(time);
        }

        /*
         * Draws the active screen
         */
        public void draw(SpriteBatch batch) {
            activeScreen.draw(batch);
        }
    }
}
