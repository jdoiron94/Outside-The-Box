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
        private readonly SpriteFont font;

        private Vector2 location;
        private Rectangle displayBar;
        private Rectangle backBar;
        private Rectangle outlineBar;
        private Color color;

        private string text;

        private readonly Vector2 origLoc;

        public DisplayBar(Texture2D texture, SpriteFont font, Vector2 location, Texture2D gradient, int width, int height) {
            this.texture = texture;
            this.font = font;
            this.location = location;
            this.gradient = gradient;
            this.width = width;
            this.height = height;
            this.outlineBar = new Rectangle((int) location.X, (int) location.Y, width, height);
            this.backBar = new Rectangle((int) location.X, (int) location.Y + 5, width, height - 10);
            this.displayBar = new Rectangle((int) location.X, (int) location.Y + 5, width, height - 10);
            this.color = Color.Black;
            this.text = "100/100";
            this.origLoc = new Vector2((int) location.X, (int) location.Y);
        }

        /// <summary>
        /// Resets the display bar back to being centered over the entity
        /// </summary>
        public void reset() {
            this.location = origLoc;
            this.outlineBar.X = (int) origLoc.X;
            this.outlineBar.Y = (int) origLoc.Y;
            this.backBar.X = (int) origLoc.X;
            this.backBar.Y = (int) origLoc.Y + 5;
            this.displayBar.X = (int) origLoc.X;
            this.displayBar.Y = (int) origLoc.Y + 5;
        }

        /// <summary>
        /// Sets the display bar's back color
        /// </summary>
        /// <param name="color">The color to set</param>
        public void setColor(Color color) {
            this.color = color;
        }

        /// <summary>
        /// Sets the display bar's text
        /// </summary>
        /// <param name="text">The text to set</param>
        public void setText(string text) {
            this.text = text;
        }

        /// <summary>
        /// Updates the whole display bar
        /// </summary>
        /// <param name="current">The current value</param>
        /// <param name="max">The max value</param>
        public void update(int current, int max) {
            text = current + "/" + max;
            int width = (int) (((float) current / max) * this.width);
            displayBar.Width = width;
        }

        /// <summary>
        /// Derives the x coordinate bounds for all components
        /// </summary>
        /// <param name="x">The x amount to derive by</param>
        public void deriveX(int x) {
            location.X += x;
            outlineBar.X += x;
            backBar.X += x;
            displayBar.X += x;
        }

        /// <summary>
        /// Derives the y coordinate bounds for all components
        /// </summary>
        /// <param name="y">The y amount to derive by</param>
        public void deriveY(int y) {
            location.Y += y;
            outlineBar.Y += y;
            backBar.Y += y;
            displayBar.Y += y;
        }

        /// <summary>
        /// Draws the display bar
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            if (gradient != null) {
                batch.Draw(gradient, outlineBar, Color.White);
            }
            batch.Draw(texture, backBar, color);
            batch.Draw(texture, displayBar, Color.White);
            Vector2 size = font.MeasureString(text);
            Vector2 loc = new Vector2(location.X + ((width - size.X) / 2.0F), location.Y + ((height - size.Y) / 2.0F));
            batch.DrawString(font, text, loc, Color.GhostWhite);
        }
    }
}
