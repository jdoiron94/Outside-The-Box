using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles keys
    /// </summary>

    public class Key : Collectible {

        private bool unlocked;

        public Key(Texture2D Texture, Vector2 Location, SoundEffect effect) :
            base(Texture, Location, effect, true) {
        }

        /// <summary>
        /// Handles unlocking of the key
        /// </summary>
        /// <param name="value">The boolean to be set</param>
        public void setUnlocked(bool value) {
            unlocked = value;
        }

        /// <summary>
        /// Returns whether or not the key is unlocked
        /// </summary>
        /// <returns>Returns true if the key is unlocked; otherwise, false</returns>
        public bool isUnlocked() {
            return unlocked;
        }
    }
}