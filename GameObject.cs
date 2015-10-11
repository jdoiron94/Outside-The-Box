using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class GameObject {

        /*
         * Class which represents various objects within the game, including doors, alarms, etc.
         */

        private readonly Texture2D texture;

        private Projectile projectile;
        private Vector2 location;
        private Vector2 destination;
        private Direction direction;
        private Rectangle bounds;

        private readonly bool liftable;

        private bool selected;
        private double lastFired;

        public GameObject(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, bool liftable) {
            this.texture = texture;
            this.projectile = projectile;
            this.location = location;
            this.direction = direction;
            this.liftable = liftable;
            destination = location;
            selected = false; 
            bounds = new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height);
            lastFired = -1;
        }

        public GameObject(Texture2D texture, Vector2 location, bool liftable) :
            this(texture, null, location, Direction.NONE, liftable) {
        }

        public GameObject(Texture2D texture, Vector2 location) :
            this(texture, location, false) {
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

        /*
         * Returns the object's destination
         */
        public Vector2 getDestination() {
            return destination;
        }

        /*
         * Sets the object's destination
         */
        public void setDestination(Vector2 destination) {
            this.destination = destination;
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
         * Returns if the object is liftable
         */
        public bool isLiftable() {
            return liftable;
        }

        /*
         * Returns if the object is currently selected
         */
        public bool isSelected() {
            return selected;
        }

        /*
         * Sets the object's selected boolean to the parameter
         */
        public void setSelected(bool selected) {
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

        /*
         * Sets the object's moving direction
         */
        public void setDirection(Direction direction) {
            this.direction = direction;
        }

        /*
         * Returns a new projectile, updating the lastFired time and creates a new projectile
         */
        public Projectile createProjectile(double lastFired) {
            this.lastFired = lastFired;
            return new Projectile(projectile.getTexture(), projectile.getPosition(), projectile.getVelocity(), projectile.getCooldown(), projectile.getRotationSpeed());
        }

        /*
         * Returns true if the object is currently on the screen; otherwise, false
         */
        public bool isOnScreen(Game1 game) {
            return location.X >= -texture.Width && location.X <= game.getWidth() && location.Y >= -texture.Height && location.Y <= game.getHeight();
        }

        /*
         * Draws the object on the screen, given a SpriteBatch
         */
        public void draw(SpriteBatch batch, byte mode) {
            if (mode == 0) {
                batch.Draw(texture, location, Color.White);
            } else if (mode == 1) {
                batch.Draw(texture, location, (liftable ? Color.LightGreen : Color.White));
            } else {
                batch.Draw(texture, location, (selected ? Color.IndianRed : Color.White));
            }
        }
    }
}
