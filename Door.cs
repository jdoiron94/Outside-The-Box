using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles door object functionality
    /// </summary>

    public class Door : GameObject {

        private bool next;
        private bool unlocked;
        private Rectangle rect;

        public Door(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, bool liftable, bool next, int width, int height) :
            base(texture, projectile, location, direction, liftable) {
            this.next = next;
            rect = new Rectangle((int) location.X, (int) location.Y, width, height);
            unlocked = false;
        }

        /// <summary>
        /// Sets the door's next bool
        /// </summary>
        /// <param name="next">Bool telling us if door leads to a new level</param>
        public void setNext(bool next) {
            this.next = next;
        }

        /// <summary>
        /// Returns whether or not there is a next level
        /// </summary>
        /// <returns>Returns true if the door leads to a new level; otherwise, false</returns>
        public bool getNext() {
            return next;
        }

        /// <summary>
        /// Returns the bounding rectangle for the door
        /// </summary>
        /// <returns>Returns the bounding rectangle</returns>
        public override Rectangle getBounds() {
            return rect;
        }

        /// <summary>
        /// Returns whether or not the door is unlocked
        /// </summary>
        /// <returns>Returns true if the door is unlocked; otherwise, false</returns>
        public bool isUnlocked() {
            return unlocked;
        }

        /// <summary>
        /// Handles unlocking of the door
        /// </summary>
        /// <param name="value">Bool to determine the door's unlocked property</param>
        public void unlockDoor(bool value) {
            unlocked = value;
        }

        /// <summary>
        /// Handles drawing the door
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(getTexture(), rect, Color.White);
        }
    }
}
