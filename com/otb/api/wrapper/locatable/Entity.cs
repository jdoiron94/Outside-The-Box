using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace OutsideTheBox {

    /// <summary>
    /// Base class which is used by NPC and Player
    /// </summary>

    public abstract class Entity {

        protected Texture2D texture;
        private Projectile projectile;
        protected Vector2 location;
        private Vector2 destination;
        private Direction direction;
        private Rectangle bounds;
        private Rectangle destinationBounds;

        private Texture2D[] northFacing;
        private Texture2D[] southFacing;
        private Texture2D[] westFacing;
        private Texture2D[] eastFacing;

        private readonly int maxHealth;

        private int velocity;
        private int currentHealth;
        private double lastFired;
        private int[] frames;
        private int ticks;

        private const byte WAIT = 0x5;

        public Entity(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, int maxHealth, int velocity) {
            this.texture = texture;
            this.projectile = projectile;
            this.location = location;
            this.direction = direction;
            this.maxHealth = maxHealth;
            this.velocity = velocity;
            destination = location;
            bounds = new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height);
            destinationBounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            northFacing = new Texture2D[4];
            southFacing = new Texture2D[4];
            westFacing = new Texture2D[4];
            eastFacing = new Texture2D[4];
            currentHealth = maxHealth;
            lastFired = -1;
            frames = new int[4];
            ticks = 0;
        }

        public Entity(Texture2D texture, Vector2 location, Direction direction, int health, int velocity) :
            this(texture, null, location, direction, health, velocity) {
        }

        /// <summary>
        /// Loads the necessary textures to show entity animation
        /// </summary>
        /// <param name="cm">The ContentManager to load sprites</param>
        public void loadTextures(ContentManager cm) {
            string projectilePrefix = "sprites/projectiles/";
            string[] projectileNames = { "Bullet", "Fire", "Ice", "Lightning", "Paralysis", "Confusion" };
            string prefix = "sprites/entities/player/";
            string[] names = { "Forward", "Back", "Left", "Right" };
            foreach (string s in names) {
                Texture2D[] array = s == "Forward" ? southFacing : s == "Back" ? northFacing : s == "Left" ? westFacing : eastFacing;
                for (int i = 1; i <= 4; i++) {
                    array[i - 1] = cm.Load<Texture2D>(prefix + s + i);
                }
            foreach (string p in projectileNames)
                {
                    Texture2D[] projectileArray = {};
                    for (int o = 1; o <= 5; o++)
                    {
                        //projectileArray[o - 1] = cm.Load<Texture2D>(projectilePrefix + p + "Orb");
                    }
                }
            }
        }

        /// <summary>
        /// Updates the entity's texture depending on the direction it's facing, using a busy wait
        /// </summary>
        public void updateMovement() {
            if (ticks >= WAIT) {
                if (direction == Direction.South) {
                    frames[0] = (frames[0] + 1) % 4;
                    texture = southFacing[frames[0]];
                } else if (direction == Direction.North) {
                    frames[1] = (frames[1] + 1) % 4;
                    texture = northFacing[frames[1]];
                } else if (direction == Direction.West) {
                    frames[2] = (frames[2] + 1) % 4;
                    texture = westFacing[frames[2]];
                } else {
                    frames[3] = (frames[3] + 1) % 4;
                    texture = eastFacing[frames[3]];
                }
                ticks = 0;
            } else {
                ticks++;
            }
        }

        /// <summary>
        /// Sets the entity's standing still texture, so that they are not legs spread apart while unmoving
        /// </summary>
        public void updateStill() {
            if (direction == Direction.South) {
                texture = southFacing[1];
            } else if (direction == Direction.North) {
                texture = northFacing[1];
            } else if (direction == Direction.West) {
                texture = westFacing[0];
            } else {
                texture = eastFacing[0];
            }
        }

        /// <summary>
        /// Returns the entity's texture
        /// </summary>
        /// <returns>Returns the entity's texture</returns>
        public Texture2D getTexture() {
            return texture;
        }

        /// <summary>
        /// Returns the entity's velocity
        /// </summary>
        /// <returns>Returns the entity's velocity</returns>
        public int getVelocity() {
            return velocity;
        }

        /// <summary>
        /// Returns the entity's projectile
        /// </summary>
        /// <returns>Returns the entity's projectile</returns>
        public Projectile getProjectile() {
            return projectile;
        }

        /// <summary>
        /// Returns the entity's location
        /// </summary>
        /// <returns>Returns the entity's location</returns>
        public Vector2 getLocation() {
            return location;
        }

        /// <summary>
        /// Returns the entity's destination
        /// </summary>
        /// <returns>Returns the entity's destination</returns>
        public Vector2 getDestination() {
            return destination;
        }

        /// <summary>
        /// Sets the entity's destination
        /// </summary>
        /// <param name="destination">The destination to set</param>
        public void setDestination(Vector2 destination) {
            this.destination = destination;
            destinationBounds.X = (int) destination.X;
            destinationBounds.Y = (int) destination.Y;
        }

        /// <summary>
        /// Returns the entity's destination bounds
        /// </summary>
        /// <returns>Returns the entity's destination bounds</returns>
        public Rectangle getDestinationBounds() {
            return destinationBounds;
        }

        /// <summary>
        /// Returns the entity's direction
        /// </summary>
        /// <returns>Returns the entity's direction</returns>
        public Direction getDirection() {
            return direction;
        }

        /// <summary>
        /// Returns the entity's bounds
        /// </summary>
        /// <returns>Returns the entity's bounds</returns>
        public Rectangle getBounds() {
            return bounds;
        }

        /// <summary>
        /// Returns the entity's maximum health
        /// </summary>
        /// <returns>Returns the entity's maximum health</returns>
        public int getMaxHealth() {
            return maxHealth;
        }

        /// <summary>
        /// Returns the entity's current health
        /// </summary>
        /// <returns>Returns the entity's current health</returns>
        public int getCurrentHealth() {
            return currentHealth;
        }

        /// <summary>
        /// Returns the entity's last projectile fire time
        /// </summary>
        /// <returns>Returns the entity's last projectile fire time in milliseconds</returns>
        public double getLastFired() {
            return lastFired;
        }

        /// <summary>
        /// Sets the entity's projectile
        /// </summary>
        /// <param name="projectile">The projectile to set</param>
        public void setProjectile(Projectile projectile) {
            this.projectile = projectile;
        }

        /// <summary>
        /// Derives the entity's x coordinate in location, bounds, and destination vectors
        /// </summary>
        /// <param name="x">The x amount for the vectors to be derived by</param>
        public void deriveX(int x) {
            location.X += x;
            bounds.X += x;
        }

        /// <summary>
        /// Derives the entity's y coordinate in location, bounds, and destination vectors
        /// </summary>
        /// <param name="y">The y amount for the vectors to be derived by</param>
        public void deriveY(int y) {
            location.Y += y;
            bounds.Y += y;
        }

        /// <summary>
        /// Sets the entity's x coordinate
        /// </summary>
        /// <param name="x">The x coordinate to set</param>
        public void setX(int x) {
            location.X = x;
            bounds.X = x;
        }

        /// <summary>
        /// Sets the entity's y coordinate
        /// </summary>
        /// <param name="y">The y coordinate to set</param>
        public void setY(int y) {
            location.Y = y;
            bounds.Y = y;
        }

        /// <summary>
        /// Sets the player's location
        /// </summary>
        /// <param name="location">The location to set</param>
        public void setLocation(Vector2 location) {
            this.location = location;
            bounds.X = (int) location.X;
            bounds.Y = (int) location.Y;
        }

        /// <summary>
        /// Sets the entity's velocity
        /// </summary>
        /// <param name="velocity">The velocity to set</param>
        public void setVelocity(int velocity) {
            this.velocity = velocity;
        }

        /// <summary>
        /// Sets the entity's direction
        /// </summary>
        /// <param name="direction">The direction to face</param>
        public void setDirection(Direction direction) {
            this.direction = direction;
        }

        /// <summary>
        /// Derives the entity's health
        /// </summary>
        /// <param name="health">The amount of health to be derived</param>
        public void deriveHealth(int health) {
            currentHealth += health;
        }

        /// <summary>
        /// Resets the entity's health to its max
        /// </summary>
        public void resetHealth() {
            currentHealth = maxHealth;
        }

        /// <summary>
        /// Returns a new projectile for the entity
        /// </summary>
        /// <param name="lastFired">The last projectile fire time</param>
        /// <returns>Returns a projectile with a new memory address for the entity</returns>
        public Projectile createProjectile(double lastFired) {
            this.lastFired = lastFired;
            return new Projectile(projectile.getOwner(), projectile.getTexture(), projectile.getVelocity(), projectile.getCooldown(), projectile.getRotationSpeed(), projectile.getSound());
        }

        /// <summary>
        /// Returns the distance to the specified entity
        /// </summary>
        /// <param name="e">The entity</param>
        /// <returns>Returns the Euclidean distance to the specified entity</returns>
        public int getDistance(Entity e) {
            return (int) Math.Sqrt(Math.Pow(e.getLocation().X - location.X, 2D) + Math.Pow(e.getLocation().Y - location.Y, 2D));
        }

        /// <summary>
        /// Returns the horizontal Euclidean distance to the specified entity
        /// </summary>
        /// <param name="e">The entity</param>
        /// <returns>Returns the horizontal Euclidean distance to the specified entity</returns>
        public int getHDistance(Entity e) {
            return (int) Math.Sqrt(Math.Pow(e.getLocation().X - location.X, 2D));
        }

        /// <summary>
        /// Returns the vertical Euclidean distance to the specified entity
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int getVDistance(Entity e) {
            return (int) Math.Sqrt(Math.Pow(e.getLocation().Y - location.Y, 2D));
        }

        /// <summary>
        /// Returns whether or not the entity is facing the specified entity
        /// </summary>
        /// <param name="e">The entity</param>
        /// <param name="scalar">The scaled distance to check</param>
        /// <returns>Returns true if the entity is facing the specified entity; otherwise, false</returns>
        public bool isFacing(Entity e, float scalar) {
            if (Math.Abs(e.getLocation().X - location.X) <= texture.Width * scalar) {
                return e.getLocation().Y >= location.Y ? direction == Direction.South : direction == Direction.North;
            }
            return e.getLocation().X >= location.X ? direction == Direction.East : direction == Direction.West;
        }

        /// <summary>
        /// Sets the entity to face the specified entity
        /// </summary>
        /// <param name="e">The entity</param>
        public void setFacing(Entity e) {
            if (Math.Abs(e.getLocation().X - location.X) <= texture.Width) {
                direction = e.getLocation().Y >= location.Y ? Direction.South : Direction.North;
            } else if (e.getLocation().X < location.X) {
                direction = Direction.West;
            } else {
                direction = Direction.East;
            }
        }

        /// <summary>
        /// Returns whether or not the entity is dead
        /// </summary>
        /// <returns>Returns true if the entity is dead; otherwise, false</returns>
        public bool isDead() {
            return currentHealth <= 0;
        }

        /// <summary>
        /// Returns whether or not the entity is currently on the game screen
        /// </summary>
        /// <param name="game">The game instance to check for viewport bounds</param>
        /// <returns>Returns true if the entity is currently on screen; otherwise, false</returns>
        public bool isOnScreen(Game1 game) {
            return location.X >= -texture.Width && location.X <= game.getWidth() && location.Y >= -texture.Height && location.Y <= game.getHeight();
        }

        /// <summary>
        /// Draws the entity
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(texture, location, Color.White);
        }
    }
}
