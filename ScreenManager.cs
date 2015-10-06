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

        public ScreenManager(Screen activeScreen, List<Screen> screens) {
            this.activeScreen = activeScreen;
           //this.screens = new List<Screen>(screens.Count);
            //this.screens.AddRange(screens);
            this.screens = screens; 
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
         * Sets the active screen to the screen at the specified index
         */
        public void setActiveScreen(int index) {
            if (!activeScreen.Equals(screens[index])) {
                activeScreen.setActive(false);
                activeScreen = screens[index];
                activeScreen.setActive(true);
            }
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
