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
        private bool liftable;
        private bool selected;

        private double lastFired;

        public GameObject(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, Rectangle windowBounds, bool liftable) {
            this.texture = texture;
            this.projectile = projectile;
            this.location = location;
            this.direction = direction;
            this.windowBounds = windowBounds;
            this.liftable = liftable;
            this.selected = false; 
            bounds = new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height);
            lastFired = -1;
        }

        public GameObject(Texture2D texture, Projectile projectile, Vector2 location, Rectangle windowBounds, bool liftable) :
            this(texture, projectile, location, Direction.NONE, windowBounds, liftable) {
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

        public bool isLiftable()
        {
            return liftable;
        }

        public bool isSelected()
        {
            return selected;
        }

        public void setSelected(bool selected)
        {
            this.selected = selected;
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
            bounds.X += x;
        }

        /*
         * Derives the object's location in terms of its y coordinate
         */
        public void deriveY(int y) {
            location.Y += y;
            bounds.Y += y;
        }

        public void setDirection(Direction direction)
        {
            this.direction = direction;
        }

        public void setLocation(int x, int y) {
            location = new Vector2(x, y);
            bounds = new Rectangle(x, y, texture.Width, texture.Height);
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

        public void drawSelectable(SpriteBatch batch)
        {
            batch.Draw(texture, location, Color.LightGreen);
        }

        public void drawSelected(SpriteBatch batch)
        {
            batch.Draw(texture, location, Color.IndianRed);
        }
    }
}
