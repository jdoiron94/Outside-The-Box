using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents the targeting system
    /// </summary>

    public class Target {

        private readonly Texture2D texture;

        private InputManager inputManager;

        public Target(Texture2D texture) {
            this.texture = texture;
        }

        /// <summary>
        /// Returns the target's texture
        /// </summary>
        /// <returns>Returns the target's reticle</returns>
        public Texture2D getTexture() {
            return texture;
        }
    }
}
