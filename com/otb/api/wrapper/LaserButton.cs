using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles laser buttons
    /// </summary>

    public class LaserButton : PressButton {

        private readonly Laser laser;

        public LaserButton(Texture2D[] textures, Vector2 location, SoundEffectInstance effect, bool deactivated, bool pushed, Laser laser) :
            base(textures, location, effect, deactivated, pushed) {
            this.laser = laser;
        }

        /// <summary>
        /// Handles updating the door button
        /// </summary>
        public override void update() {
            if (!isDeactivated()) {
                laser.setActivated(isPushed());
            } else {
                laser.setActivated(false);
            }
        }
    }
}
