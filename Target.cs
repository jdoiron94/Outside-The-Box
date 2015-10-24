using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents the targeting system
    /// </summary>

    public class Target {

        private Texture2D texture;
        private InputManager inputManager;

        private bool active;

        public Target(Texture2D texture) {
            this.texture = texture;
            this.active = false;
        }

        /// <summary>
        /// Returns the target's texture
        /// </summary>
        /// <returns>Returns the target's reticle</returns>
        public Texture2D getTexture() {
            return texture;
        }

        /// <summary>
        /// Sets the input manager
        /// </summary>
        /// <param name="inputManager">The input manager to set</param>
        public void setInputManager(InputManager inputManager) {
            this.inputManager = inputManager;
        }

        /// <summary>
        /// Returns whether or not the targeting system is active
        /// </summary>
        /// <returns>Returns true if the system is active; otherwise, false</returns>
        public bool isActive() {
            return active;
        }

        /// <summary>
        /// Sets the targeting system's active status
        /// </summary>
        /// <param name="active">The active status to set</param>
        public void setActive(bool active) {
            this.active = active;
        }
    }
}
