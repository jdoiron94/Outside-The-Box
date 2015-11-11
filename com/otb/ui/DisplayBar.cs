using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles managing a display bar
    /// </summary>

    public class DisplayBar {

        private readonly int width;
        private readonly int height;

        private readonly Texture2D texture;
        private readonly Texture2D gradient;
        private readonly Vector2 location;
        private readonly Rectangle bounds;
        private readonly Color displayColor;
        private readonly SpriteFont font;

        private Rectangle displayBar;
        private Rectangle backBar;
        private Rectangle outlineBar;

        private string text;

        public DisplayBar(Texture2D texture, SpriteFont font, Vector2 location, Color displayColor, Texture2D gradient, int width, int height) {
            this.texture = texture;
            this.font = font;
            this.location = location;
            this.displayColor = displayColor;
            this.gradient = gradient;
            this.width = width;
            this.height = height;
            displayBar = new Rectangle((int) location.X, (int) location.Y + 5, width, 10);
            backBar = new Rectangle((int) location.X, (int) location.Y + 5, width, 10);
            outlineBar = new Rectangle((int) location.X, (int) location.Y, width, 20);
            bounds = new Rectangle((int) location.X, (int) location.Y, 1, 1);
            text = "100/100";
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

        /// <summary>
        /// Sets the display bar's text
        /// </summary>
        /// <param name="text">The text to set</param>
        public void setText(string text) {
            this.text = text;
        }

        /// <summary>
        /// Draws the display bar
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(gradient, outlineBar, Color.White);
            batch.Draw(texture, backBar, Color.Black);
            batch.Draw(texture, displayBar, Color.White);
            Vector2 size = font.MeasureString(text);
            Vector2 loc = new Vector2(location.X + ((width - size.X) / 2), location.Y + ((height - size.Y) / 2));
            batch.DrawString(font, text, loc, Color.White);
        }
    }
}
