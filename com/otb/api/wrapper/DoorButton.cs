using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class DoorButton : PressButton {

        private readonly Door door;

        public DoorButton(Texture2D[] Textures, Vector2 location, SoundEffectInstance effect, bool deactivated, bool pushed, Door door) :
            base(Textures, location, effect, deactivated, pushed) {
            this.door = door;
        }

        /// <summary>
        /// Returns the door
        /// </summary>
        /// <returns>Returns the door</returns>
        public Door getDoor() {
            return door;
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
