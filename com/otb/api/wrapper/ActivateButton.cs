using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles activatable buttons
    /// </summary>

    public class ActivateButton : PressButton {

        private readonly PressButton button;

        public ActivateButton(Texture2D[] Textures, Vector2 location, SoundEffectInstance effect, bool deactivated, bool pushed, PressButton button) :
            base(Textures, location, effect, deactivated, pushed) {
            this.button = button;
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
