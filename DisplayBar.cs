using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp
{
    public class DisplayBar {

        private readonly Texture2D texture;
        private readonly Texture2D gradient; 
        private readonly Vector2 location;
        private readonly Rectangle backBar;
        private readonly Rectangle outlineBar;
        private readonly Rectangle bounds;
        private readonly Color displayColor;

        private Rectangle displayBar;

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

        /*
         * Returns the display bar's texture
         */
        public Texture2D getTexture() {
            return texture; 
        } 

        /*
         * Returns the display bar's location
         */
        public Vector2 getLocation() {
            return location; 
        }

        /*
         * Returns the display bar's bounding box
         */
        public Rectangle getDisplayBar() {
            return displayBar; 
        }

        /*
         * Returns the display bar's color
         */
        public Color getDisplayColor() {
            return displayColor; 
        }

        /*
         * Sets the display bar's width
         */
        public void setWidth(int width) {
            displayBar.Width = width;
        }

        /*
         * Draws the display bar, given a SpriteBatch
         */
        public void draw(SpriteBatch batch) {
            batch.Draw(gradient, outlineBar, Color.White);
            batch.Draw(texture, backBar, Color.Black);
            batch.Draw(texture, displayBar, Color.White);
        }
    }
}
