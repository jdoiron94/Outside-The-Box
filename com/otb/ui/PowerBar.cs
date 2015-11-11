using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OutsideTheBox {

    public class PowerBar {

        private readonly Texture2D texture;
        private readonly Vector2 location;
        private readonly Rectangle bounds;

        public PowerBar(Texture2D texture, Vector2 location) {
            this.texture = texture;
            this.location = location;
            bounds = new Rectangle((int) location.X, (int) location.Y, 1, 1);
        }

        public PowerBar(Texture2D texture, Vector2 location, int width) {
            this.texture = texture;
            this.location = location;
            bounds = new Rectangle((int) location.X, (int) location.Y, 1, 1);
        }

        /// <summary>
        /// Returns the power bar's texture
        /// </summary>
        /// <returns>Returns the power bar's texture</returns>
        public Texture2D getTexture() {
            return texture;
        }

        /// <summary>
        /// Returns the power bar's location
        /// </summary>
        /// <returns>Returns the power bar's location</returns>
        public Vector2 getLocation() {
            return location;
        }

        /// <summary>
        /// Draws the display bar
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(texture, location, Color.White);

        }
    }
}
