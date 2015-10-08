using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace KineticCamp {

    public class Entity {

        /*
         * Class meant to represent any NPCs, the player, etc. Contains all pertinent information as a
         * storage container.
         */

        private Texture2D texture;
        private Projectile projectile;
        private Vector2 location;
        private Vector2 destination;
        private Vector2 moved;
        private Direction direction;
        private AIPath path;
        private Rectangle bounds;
        private Rectangle windowBounds;

        private int health;
        private int speed;
        private int stepSize;
        private int movedX;
        private int movedY;
        private double lastFired;

        public Entity(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, Rectangle bounds, Rectangle windowBounds, int health, int speed, int stepSize) {
            this.texture = texture;
            this.projectile = projectile;
            this.location = location;
            this.destination = location;
            this.direction = direction;
            this.bounds = bounds;
            this.windowBounds = windowBounds;
            this.health = health;
            this.speed = speed;
            this.stepSize = stepSize;
            moved = new Vector2(0f, 0f);
            path = null;
            lastFired = -1;
        }

        public Entity(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, Rectangle windowBounds, int health, int speed) :
            this(texture, projectile, location, direction, new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height), windowBounds, health, speed, 4) {
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
         * Returns the entity's distance moved in both x/y directions
         */
        public Vector2 getMoved() {
            return moved;
        }

        /*
         * Returns the entity's current direction
         */
        public Direction getDirection() {
            return direction;
        }

        /*
         * Returns the entity's static AI path
         */
        public AIPath getPath() {
            return path;
        }

        /*
         * Returns the entity's bounding box
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
         * Returns the entity's current health level
         */
        public int getHealth() {
            return health;
        }

        /*
         * Returns the entity's moving speed
         */
        public int getSpeed() {
            return speed;
        }

        /*
         * Returns the entity's step size
         */
        public int getStepSize() {
            return stepSize;
        }

        /*
         * Returns the entity's last projectile fire time in ms
         */
        public double getLastFired() {
            return lastFired;
        }

        public void setPath(AIPath path) {
            this.path = path;
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
            movedX = x;
            bounds.X += x;
            destination.X += x;
        }

        /*
         * Returns the entity's distance moved on the x axis
         */
        public int getMovedX() {
            return movedX;
        }

        /*
         * Returns the entity's distance moved on the y axis
         */
        public int getMovedY() {
            return movedY;
        }

        /*
         * Derives the entity's location in terms of its y coordinate
         */
        public void deriveY(int y) {
            location.Y += y;
            movedY = y;
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
            this.health += health;
        }

        /*
         * Returns a new projectile, updating the lastFired time and creates a new projectile
         */
        public Projectile createProjectile(double lastFired) {
            this.lastFired = lastFired; 
            return new Projectile(projectile.getTexture(), location, projectile.getVelocity(), projectile.getCooldown(), projectile.getRotationSpeed());
        }

        /*
         * Returns true if the entity is dead; otherwise, false
         */
        public bool isDead() {
            return health <= 0;
        }

        /*
         * Returns true if the entity is currently on the screen; otherwise, false
         */
        public bool isOnScreen() {
            return location.X >= -texture.Width && location.X <= windowBounds.Width && location.Y >= -texture.Height && location.Y <= windowBounds.Height;
        }

        /*
         * Draws an entity, given a SpriteBatch
         */
        public void draw(SpriteBatch batch) {
            batch.Draw(texture, location, Color.White);
        }
    }
}
