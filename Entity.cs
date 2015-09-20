using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Entity {

        private Texture2D texture;
        private Projectile projectile;
        private Vector2 location;
        private Rectangle bounds;
        private Rectangle windowBounds;

        private int health;
        private int speed;

        public Entity(Texture2D texture, Projectile projectile, Vector2 location, Rectangle bounds, Rectangle windowBounds, int health, int speed) {
            this.texture = texture;
            this.projectile = projectile;
            this.bounds = bounds;
            this.windowBounds = windowBounds;
            this.location = location;
            this.health = health;
            this.speed = speed;
        }

        public Entity(Texture2D texture, Projectile projectile, Vector2 location, Rectangle windowBounds, int health, int speed) :
            this(texture, projectile, location, new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height), windowBounds, health, speed) {
        }

        public Texture2D getTexture() {
            return texture;
        }

        public Projectile getProjectile() {
            return projectile;
        }

        public Vector2 getLocation() {
            return location;
        }

        public Rectangle getBounds() {
            return bounds;
        }

        public Rectangle getWindowBounds() {
            return windowBounds;
        }

        public int getHealth() {
            return health;
        }

        public int getSpeed() {
            return speed;
        }

        public void setProjectile(Projectile projectile) {
            this.projectile = projectile;
        }

        public void deriveX(int x) {
            location.X += x;
        }

        public void deriveY(int y) {
            location.Y += y;
        }

        public void deriveHealth(int health) {
            this.health += health;
        }

        public bool isDead() {
            return health <= 0;
        }

        public bool isOnScreen() {
            return location.X <= windowBounds.Width && location.Y <= windowBounds.Height;
        }

        public void draw(SpriteBatch batch) {
            batch.Draw(texture, location, Color.White);
        }
    }
}
