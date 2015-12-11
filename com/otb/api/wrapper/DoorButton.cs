using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles door releasable buttons
    /// </summary>

    public class DoorButton : PressButton {

        private readonly Door door;

        public DoorButton(Texture2D[] Textures, Vector2 location, SoundEffectInstance effect, bool deactivated, bool pushed, Door door) :
            base(Textures, location, effect, deactivated, pushed) {
            this.door = door;
        }

        /// <summary>
        /// Handles updating the door button
        /// </summary>
        public override void update() {
            if (!isDeactivated()) {
                door.setUnlocked(isPushed());
            }
        }
    }
}
