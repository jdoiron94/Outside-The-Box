using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Entity {

        private Texture2D texture;
        private Projectile projectile;
        private Vector2 location;
        private Direction direction;
        private Rectangle bounds;
        private Rectangle windowBounds;

        private int health;
        private int speed;
        private double lastFired;

        public Entity(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, Rectangle bounds, Rectangle windowBounds, int health, int speed) {
            this.texture = texture;
            this.projectile = projectile;
            this.location = location;
            this.direction = direction;
            this.bounds = bounds;
            this.windowBounds = windowBounds;
            this.health = health;
            this.speed = speed;
            lastFired = -1;
        }

        public Entity(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, Rectangle windowBounds, int health, int speed) :
            this(texture, projectile, location, direction, new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height), windowBounds, health, speed) {
        }

        // will need to change to texture2d array for all directions
        // stills: facing north, south, west, east
        // movement: l/r north, l/r south, l/r west, l/r east
        // 12 sprites minimum
        public Texture2D getTexture() {
            return texture;
        }

        public Projectile getProjectile() {
            return projectile;
        }

        public Vector2 getLocation() {
            return location;
        }

        public Direction getDirection() {
            return direction;
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

        public double getLastFired() {
            return lastFired;
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

        // update sprite to respective sprite facing the correct direction
        public void setDirection(Direction direction) {
            this.direction = direction;
        }

        public void deriveHealth(int health) {
            this.health += health;
        }

        public Projectile createProjectile(double lastFired) {
            this.lastFired = lastFired; 
            return new Projectile(projectile.getTexture(), projectile.getPosition(), projectile.getVelocity(), projectile.getCooldown(), projectile.getRotationSpeed());
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
