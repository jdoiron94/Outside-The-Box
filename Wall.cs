using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class Wall : GameObject {

        private Rectangle rect;

        public Wall(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, bool liftable, bool next, int width, int height) :
            base(texture, projectile, location, direction, false, width, height) {
            rect = new Rectangle((int) location.X, (int) location.Y, width, height);
        }

        public Rectangle getWallBounds() {
            return rect;
        }

        public void draw(SpriteBatch batch) {
            batch.Draw(getTexture(), rect, Color.White);
        }
    }
}
