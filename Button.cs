using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which holds information to construct a button
    /// </summary>

    public class Button {

        private Texture2D texture;
        private Vector2 location;
        private Rectangle bounds;

        public Button(Texture2D texture, Vector2 location) {
            this.texture = texture;
            this.location = location;
            bounds = new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height);
        }

        /// <summary>
        /// Returns the button's texture
        /// </summary>
        /// <returns>Returns the button's texture</returns>
        public Texture2D getTexture() {
            return texture;
        }

        /// <summary>
        /// Returns the button's location
        /// </summary>
        /// <returns>Returns the button's location</returns>
        public Vector2 getLocation() {
            return location;
        }

        /// <summary>
        /// Returns the button's bounds
        /// </summary>
        /// <returns>Returns the button's bounds</returns>
        public Rectangle getBounds() {
            return bounds;
        }

        /// <summary>
        /// Handles exiting of the menu
        /// </summary>
        /// <param name="inputManager">The input manager to control screens with</param>
        public void exitMenu(InputManager inputManager) {
            inputManager.getScreenManager().setActiveScreen(1);
            inputManager.getMenu().setActive(false);
            inputManager.getLevel().setActive(true);
        }

        /// <summary>
        /// Unlocks a power, if you have enough EXP; otherwise, do nothing.
        /// The method should be passed a Power object in the future
        /// </summary>
        /// <param name="inputManager">The input manager to check player experience with</param>
        public void unlockPower(InputManager inputManager) {
            int amount = 500;
            if (!inputManager.getPlayerManager().spendExperience(amount)) {
                return;
            } else {
                //
            }
        }
    }
}
