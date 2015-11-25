using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class ActivateButton : PressButton {

        private readonly PressButton button;

        public ActivateButton(Texture2D[] Textures, Vector2 location, SoundEffectInstance effect, bool deactivated, bool pushed, PressButton button) :
            base(Textures, location, effect, deactivated, pushed) {
            this.button = button;
        }

        /// <summary>
        /// Returns the button
        /// </summary>
        /// <returns>Returns the button</returns>
        public PressButton getButton() {
            return button;
        }

        /// <summary>
        /// Handles updating the button
        /// </summary>
        public override void update() {
            if (!isDeactivated()) {
                button.setDeactivated(!isPushed());
            } else {
                //button.setDeactivated(true);
            }
        }
    }
}
