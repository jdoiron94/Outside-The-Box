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

        /// <summary>
        /// Returns the game object's texture
        /// </summary>
        /// <returns>Returns the game object's texture</returns>
        public Texture2D getTexture() {
            return texture;
        }

        /// <summary>
        /// Returns the game object's projectile
        /// </summary>
        /// <returns>Returns the game object's texture</returns>
        public Projectile getProjectile() {
            return projectile;
        }

        /// <summary>
        /// Returns the game object's location
        /// </summary>
        /// <returns>Returns the game object's location</returns>
        public Vector2 getLocation() {
            return location;
        }

        /// <summary>
        /// Returns the game object's destination
        /// </summary>
        /// <returns>Returns the game object's destination</returns>
        public Vector2 getDestination() {
            return destination;
        }

        /// <summary>
        /// Sets the game object's destination
        /// </summary>
        /// <param name="destination">The destination to be set</param>
        public void setDestination(Vector2 destination) {
            this.destination = destination;
        }

        /// <summary>
        /// Returns the game object's direction
        /// </summary>
        /// <returns>Returns the game object's direction</returns>
        public Direction getDirection() {
            return direction;
        }

        /// <summary>
        /// Returns the game object's bounds
        /// </summary>
        /// <returns></returns>
        public Rectangle getBounds() {
            return bounds;
        }

        /// <summary>
        /// Returns if the game object can be lifted
        /// </summary>
        /// <returns>Returns true if the game object is liftable; otherwise, false</returns>
        public bool isLiftable() {
            return liftable;
        }

        /// <summary>
        /// Returns if the game object is currently selected
        /// </summary>
        /// <returns>Returns true if the game object is currently selected; otherwise, false</returns>
        public bool isSelected() {
            return selected;
        }

        /// <summary>
        /// Sets the game object's selected state to the specified boolean
        /// </summary>
        /// <param name="selected">The selected state to be set</param>
        public void setSelected(bool selected) {
            this.selected = selected;
        }

        /// <summary>
        /// Returns the game object's last projectile fire time
        /// </summary>
        /// <returns>Returns the game object's last projectile fire time</returns>
        public double getLastFired() {
            return lastFired;
        }

        /// <summary>
        /// Sets the game object's projectile to the specified projectile
        /// </summary>
        /// <param name="projectile">The projectile to be set</param>
        public void setProjectile(Projectile projectile) {
            this.projectile = projectile;
        }

        /// <summary>
        /// Derives the game object's x coordinate in location and bounds by the specified x amount
        /// </summary>
        /// <param name="x">The x amount to be derived by</param>
        public void deriveX(int x) {
            location.X += x;
            bounds.X += x;
        }

        /// <summary>
        /// Derives the game object's y coordinate in location and bounds by the specified y amount
        /// </summary>
        /// <param name="y">The y amount to be derived by</param>
        public void deriveY(int y) {
            location.Y += y;
            bounds.Y += y;
        }

        /// <summary>
        /// Sets the game object's direction
        /// </summary>
        /// <param name="direction">The direction to be set</param>
        public void setDirection(Direction direction) {
            this.direction = direction;
        }

        /// <summary>
        /// Returns a new projectile for the game object
        /// </summary>
        /// <param name="lastFired">The last time the game object fired a projectile</param>
        /// <returns>Returns a projectile with a new memory address for the game object</returns>
        public Projectile createProjectile(double lastFired) {
            this.lastFired = lastFired;
            return new Projectile(projectile.getOwner(), projectile.getTexture(), projectile.getVelocity(), projectile.getCooldown(), projectile.getRotationSpeed());
        }

        /// <summary>
        /// Returns whether or not the game object is currently on the screen
        /// </summary>
        /// <param name="game">The game instance to check its viewport bounds</param>
        /// <returns>Returns true if the game object is currently on the screen; otherwise, false</returns>
        public bool isOnScreen(Game1 game) {
            return location.X >= -texture.Width && location.X <= game.getWidth() && location.Y >= -texture.Height && location.Y <= game.getHeight();
        }

        /// <summary>
        /// Draws the game object
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        /// <param name="mode">The game's telekinesis mode to draw with respect to</param>
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
