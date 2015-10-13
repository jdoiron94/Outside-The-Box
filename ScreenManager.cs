using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class ScreenManager {

        /*
         * Class which represents a screen state manager, determining which screen is currently active.
         */

        private Screen activeScreen;

        private readonly Screen[] screens;

        public ScreenManager(Screen activeScreen, Screen[] screens) {
            this.activeScreen = activeScreen;
            this.screens = screens; 
        }

        /// <summary>
        /// Returns the active screen
        /// </summary>
        /// <returns>Returns the active screen</returns>
        public Screen getActiveScreen() {
            return activeScreen;
        }

        /// <summary>
        /// Returns all of the screens
        /// </summary>
        /// <returns>Returns a screen array of the screens it manages</returns>
        public Screen[] getScreens() {
            return screens;
        }

        /// <summary>
        /// Sets the active screen to the screen found at the specified index
        /// </summary>
        /// <param name="index">The screen's index to be set</param>
        public void setActiveScreen(int index) {
            if (!activeScreen.Equals(screens[index])) {
                activeScreen.setActive(false);
                activeScreen = screens[index];
                activeScreen.setActive(true);
            }
        }
        
        /// <summary>
        /// Updates the active screen
        /// </summary>
        /// <param name="time">The game time to respect</param>
        public void update(GameTime time) {
            activeScreen.update(time);
        }

        /// <summary>
        /// Draws the active screen
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            activeScreen.draw(batch);
        }
    }
}
