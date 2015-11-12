using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class DoorButton : PressButton {

        private readonly Door door;

        public DoorButton(Texture2D[] Textures, Vector2 location, bool deactivated, bool pushed, Door barrier) :
            base(Textures, location, deactivated, pushed) {
            this.door = barrier;
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
                door.unlockDoor(isPushed());
            }
        }
    }
}
