using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles managing a display bar
    /// </summary>

    public class DisplayBar {

        private readonly Texture2D texture;
        private readonly Texture2D gradient;
        private readonly Vector2 location;
        private readonly Rectangle bounds;
        private readonly Color displayColor;

        private Rectangle displayBar;
        private Rectangle backBar;
        private Rectangle outlineBar;

        public DisplayBar(Texture2D texture, Vector2 location, Color displayColor, Texture2D gradient) {
            this.texture = texture;
            this.gradient = gradient;
            this.location = location;
            displayBar = new Rectangle((int) location.X, (int) location.Y + 5, 200, 10);
            backBar = new Rectangle((int) location.X, (int) location.Y + 5, 200, 10);
            outlineBar = new Rectangle((int) location.X - 10, (int) location.Y, 220, 20);
            this.displayColor = displayColor;
            bounds = new Rectangle((int) location.X, (int) location.Y, 1, 1);
        }

        public DisplayBar(Texture2D texture, Vector2 location, Color displayColor, Texture2D gradient, int width)
        {
            this.texture = texture;
            this.gradient = gradient;
            this.location = location;
            displayBar = new Rectangle((int)location.X, (int)location.Y + 5, width, 10);
            backBar = new Rectangle((int)location.X, (int)location.Y + 5, width, 10);
            outlineBar = new Rectangle((int)location.X - 10, (int)location.Y, width+20, 20);
            this.displayColor = displayColor;
            bounds = new Rectangle((int)location.X, (int)location.Y, 1, 1);
        }

        /// <summary>
        /// Returns the display bar's texture
        /// </summary>
        /// <returns>Returns the display bar's texture</returns>
        public Texture2D getTexture() {
            return texture;
        }

        /// <summary>
        /// Returns the display bar's location
        /// </summary>
        /// <returns>Returns the display bar's location</returns>
        public Vector2 getLocation() {
            return location;
        }

        /// <summary>
        /// Returns the display bar's bounds
        /// </summary>
        /// <returns>Returns the display bar's bounds</returns>
        public Rectangle getDisplayBar() {
            return displayBar;
        }

        /// <summary>
        /// Returns the display bar's color
        /// </summary>
        /// <returns>Returns the display bar's color</returns>
        public Color getDisplayColor() {
            return displayColor;
        }

        /// <summary>
        /// Returns the width of the display bar
        /// </summary>
        /// <returns>Returns the display bar's width</returns>
        public int getWidth() {
            return displayBar.Width;
        }

        /// <summary>
        /// Sets the display bar's width
        /// </summary>
        /// <param name="width">The width to set</param>
        public void setWidth(int width) {
            displayBar.Width = width;
        }

        public void increaseSize(int width)
        {
            //displayBar.Width = width;
            backBar.Width = width;
            outlineBar.Width = width + 20; 

        }

        /// <summary>
        /// Draws the display bar
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(gradient, outlineBar, Color.White);
            batch.Draw(texture, backBar, Color.Black);
            batch.Draw(texture, displayBar, Color.White);
        }
    }
}
