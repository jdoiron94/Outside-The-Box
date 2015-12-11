using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles lasers
    /// </summary>

    public class Laser : Pit {

        private bool activated;
        private bool defaultValue;

        public Laser(Texture2D texture, Vector2 Location, SoundEffectInstance effect, int height, int width) :
            base(texture, Location, effect, width, height) {
            this.activated = true;
            this.defaultValue = true;
        }

        public Laser(Texture2D texture, Vector2 Location, SoundEffectInstance effect, int height, int width, bool activated) :
            base(texture, Location, effect, width, height) {
            this.activated = activated;
            if (activated) {
                this.defaultValue = true;
            } else {
                this.defaultValue = false;
            }
        }

        /// <summary>
        /// Returns whether or not a laser is activated
        /// </summary>
        /// <returns>Returns true if the laser is activated; otherwise, false</returns>
        public bool isActivated() {
            return activated;
        }

        /// <summary>
        /// Sets the laser's activated status
        /// </summary>
        /// <param name="value">The status to set</param>
        public void setActivated(bool value) {
            if (!defaultValue) {
                activated = value;
            } else {
                activated = !value;
            }
        }

        public override void draw(SpriteBatch batch) {
            if (activated) {
                batch.Draw(getTexture(), getSize(), Color.White);
            }
        }
    }
}
