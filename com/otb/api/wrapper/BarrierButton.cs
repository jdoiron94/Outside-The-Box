using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class BarrierButton : PressButton {

        private readonly Barrier barrier;

        public BarrierButton(Texture2D[] Textures, Vector2 location, SoundEffectInstance effect, bool deactivated, bool pushed, Barrier barrier) :
            base(Textures, location, effect, deactivated, pushed) {
            this.barrier = barrier;
        }

        /// <summary>
        /// Returns the barrier button
        /// </summary>
        /// <returns>Returns the barrier button</returns>
        public Barrier getBarrier() {
            return barrier;
        }

        /// <summary>
        /// Handles updating the barrier button
        /// </summary>
        public override void update() {
            if (!isDeactivated()) {
                barrier.setState(isPushed());
            } else {
                barrier.setState(false);
            }
        }
    }
}
