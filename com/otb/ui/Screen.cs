using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents a screen
    /// </summary>

    public class Screen {

        private readonly string name;
        private readonly Song song;

        private bool active;

        public Screen(string name, Song song, bool active) {
            this.name = name;
            this.song = song;
            this.active = active;
        }

        public Screen(string name, bool active) :
            this(name, null, active) {
        }

        public Screen(string name) :
            this(name, false) {
        }

        /// <summary>
        /// Returns the screen's name
        /// </summary>
        /// <returns>Returns the screen's name</returns>
        public string getName() {
            return name;
        }

        /// <summary>
        /// Returns the screen's song
        /// </summary>
        /// <returns>Returns the screen's song</returns>
        public Song getSong() {
            return song;
        }

        /// <summary>
        /// Returns whether or not the screen is currently active
        /// </summary>
        /// <returns>Returns true if the screen is currently active; otherwise, false</returns>
        public bool isActive() {
            return active;
        }

        /// <summary>
        /// Sets the screen's active status to the specified boolean
        /// </summary>
        /// <param name="active">The active status to set</param>
        public void setActive(bool active) {
            this.active = active;
        }

        /// <summary>
        /// Overridable update method to handle screen updates
        /// </summary>
        /// <param name="time">The game time to respect</param>
        public virtual void update(GameTime time) {
        }

        /// <summary>
        /// Overridable draw method to handle drawing of the screen
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public virtual void draw(SpriteBatch batch) {
        }
    }
}
