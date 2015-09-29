using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class GameObject {

        /*
         * Class which represents various objects within the game, including doors, alarms, etc.
         */

        private Texture2D texture;
        private Projectile projectile;
        private Vector2 location;
        private Direction direction;
        private Rectangle bounds;
        private Rectangle windowBounds;

        private double lastFired;

        public GameObject(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, Rectangle windowBounds) {
            this.texture = texture;
            this.projectile = projectile;
            this.location = location;
            this.direction = direction;
            this.windowBounds = windowBounds;
            bounds = new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height);
            lastFired = -1;
        }

        public GameObject(Texture2D texture, Projectile projectile, Vector2 location, Rectangle windowBounds) :
            this(texture, projectile, location, Direction.NONE, windowBounds) {
        }

        /* Returns the object's texture
         */
        public Texture2D getTexture() {
            return texture;
        }

        /*
         * Returns the object's projectile
         */
        public Projectile getProjectile() {
            return projectile;
        }

        /*
         * Returns the object's current location
         */
        public Vector2 getLocation() {
            return location;
        }

        /* Returns the object's direction
         */
        public Direction getDirection() {
            return direction;
        }

        /*
         * Returns the objects bounding box
         */
        public Rectangle getBounds() {
            return bounds;
        }

        /*
         * Returns the game's window bounds
         */
        public Rectangle getWindowBounds() {
            return windowBounds;
        }

        /*
         * Returns the object's last projectile fire time in ms
         */
        public double getLastFired() {
            return lastFired;
        }

        /*
         * Sets the object's projectile to the given projectile
         */
        public void setProjectile(Projectile projectile) {
            this.projectile = projectile;
        }

        /*
         * Derives the object's location in terms of its x coordinate
         */
        public void deriveX(int x) {
            location.X += x;
        }

        /*
         * Derives the object's location in terms of its y coordinate
         */
        public void deriveY(int y) {
            location.Y += y;
        }

        /*
         * Returns a new projectile, updating the lastFired time and creates a new projectile
         */
        public Projectile createProjectile(double lastFired) {
            this.lastFired = lastFired;
            return new Projectile(projectile.getTexture(), projectile.getPosition(), projectile.getVelocity(), projectile.getCooldown(), projectile.getRotationSpeed());
        }

        /*
         * REturns true if the object is currently on the screen; otherwise, false
         */
        public bool isOnScreen() {
            return location.X >= -texture.Width && location.X <= windowBounds.Width && location.Y >= -texture.Height && location.Y <= windowBounds.Height;
        }

        /*
         * Draws the object on the screen, given a SpriteBatch
         */
        public void draw(SpriteBatch batch) {
            batch.Draw(texture, location, Color.White);
        }
    }
}
