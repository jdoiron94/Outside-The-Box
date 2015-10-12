using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public abstract class Entity {

        /*
         * Base class which is extended by NPC and Player. Contains all pertinent information as a
         * storage container.
         */

        protected Texture2D texture;
        private Projectile projectile;
        protected Vector2 location;
        private Vector2 destination;
        private Direction direction;
        private Rectangle bounds;

        private readonly int maxHealth;

        private int velocity;
        private int currentHealth;
        private double lastFired;

        public Entity(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, int maxHealth, int velocity) {
            this.texture = texture;
            this.projectile = projectile;
            this.location = location;
            this.direction = direction;
            this.maxHealth = maxHealth;
            this.velocity = velocity;
            destination = location;
            bounds = new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height);
            currentHealth = maxHealth;
            lastFired = -1;
        }

        public Entity(Texture2D texture, Vector2 location, Direction direction, int health, int velocity) :
            this(texture, null, location, direction, health, velocity) {
        }

        // will need to change to texture2d array for all directions
        // stills: facing north, south, west, east
        // movement: l/r north, l/r south, l/r west, l/r east
        // 12 sprites minimum

        /*
         * Returns the entity's texture
         */
        public Texture2D getTexture() {
            return texture;
        }

        /*
         * Returns the entity's velocity
         */
        public int getVelocity() {
            return velocity;
        }

        /*
         * Returns the entity's given projectile
         */
        public Projectile getProjectile() {
            return projectile;
        }

        /*
         * Returns the entity's current location
         */
        public Vector2 getLocation() {
            return location;
        }

        /*
         * Returns the entity's destination
         */
        public Vector2 getDestination() {
            return destination;
        }

        /*
         * Sets the entity's destination
         */
        public void setDestination(Vector2 destination) {
            this.destination = destination;
        }

        /*
         * Returns the entity's current direction
         */
        public Direction getDirection() {
            return direction;
        }

        /*
         * Returns the entity's bounding box
         */
        public Rectangle getBounds() {
            return bounds;
        }

        /*
         * Returns the entity's max health
         */
        public int getMaxHealth() {
            return maxHealth;
        }

        /*
         * Returns the entity's current health
         */
        public int getCurrentHealth() {
            return currentHealth;
        }

        /*
         * Returns the entity's last projectile fire time in ms
         */
        public double getLastFired() {
            return lastFired;
        }

        /*
         * Sets the entity's projectile to the given projectile
         */
        public void setProjectile(Projectile projectile) {
            this.projectile = projectile;
        }

        /*
         * Derives the entity's location in terms of its x coordinate
         */
        public void deriveX(int x) {
            location.X += x;
            bounds.X += x;
            destination.X += x;
        }

        /*
         * Derives the entity's location in terms of its y coordinate
         */
        public void deriveY(int y) {
            location.Y += y;
            bounds.Y += y;
            destination.Y += y;
        }

        // update sprite to respective sprite facing the correct direction

        /*
         * Sets the entity's current direction
         */
        public void setDirection(Direction direction) {
            this.direction = direction;
        }

        /*
         * Derives the entity's health, allowing for health regen or degredation
         */
        public void deriveHealth(int health) {
            currentHealth += health;
        }

        /*
         * Returns a new projectile, updating the lastFired time and creates a new projectile
         */
        public Projectile createProjectile(double lastFired) {
            this.lastFired = lastFired;
            return new Projectile(projectile.getOwner(), projectile.getTexture(), projectile.getVelocity(), projectile.getCooldown(), projectile.getRotationSpeed());
        }

        /*
         * Returns true if the entity is dead; otherwise, false
         */
        public bool isDead() {
            return currentHealth <= 0;
        }

        /*
         * Returns true if the entity is currently on the screen; otherwise, false
         */
        public bool isOnScreen(Game1 game) {
            return location.X >= -texture.Width && location.X <= game.getWidth() && location.Y >= -texture.Height && location.Y <= game.getHeight();
        }

        /*
         * Draws an entity, given a SpriteBatch
         */
        public void draw(SpriteBatch batch) {
            batch.Draw(texture, location, Color.White);
        }
    }
}
