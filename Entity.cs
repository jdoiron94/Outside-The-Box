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
        private Vector2 moved;
        private Direction direction;
        private Rectangle bounds;
        private Rectangle windowBounds;

        private int health;
        private int speed;
        private double lastFired;

        private int movedX;
        private int movedY;

        public Entity(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, Rectangle bounds, Rectangle windowBounds, int health, int speed) {
            this.texture = texture;
            this.projectile = projectile;
            this.location = location;
            this.direction = direction;
            this.bounds = bounds;
            this.windowBounds = windowBounds;
            this.health = health;
            this.speed = speed;
            moved = new Vector2(0f, 0f);
            lastFired = -1;
        }

        public Entity(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, Rectangle windowBounds, int health, int speed) :
            this(texture, projectile, location, direction, new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height), windowBounds, health, speed) {
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
            movedX += x;
            Console.WriteLine("movedX: " + movedX);
            //bounds.X += x;
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
            movedY += y;
            Console.WriteLine("movedY: " + movedY);
            //bounds.Y += y;
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
            return new Projectile(projectile.getTexture(), projectile.getPosition(), projectile.getVelocity(), projectile.getCooldown(), projectile.getRotationSpeed());
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
