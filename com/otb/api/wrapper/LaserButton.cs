using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class LaserButton : PressButton {

        private readonly Laser laser;

        public LaserButton(Texture2D[] textures, Vector2 location, bool deactivated, bool pushed, Laser laser) :
            base(textures, location, deactivated, pushed) {
            this.laser = laser;
        }

        /// <summary>
        /// Returns the laser
        /// </summary>
        /// <returns>Returns the laser</returns>
        public Laser getLaser() {
            return laser;
        }

        /// <summary>
        /// Handles updating the door button
        /// </summary>
        public override void update() {
            if (!isDeactivated()) {
                laser.setActivated(!isPushed());
            }
        }
    }
}
