﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles barrier buttons
    /// </summary>

    public class BarrierButton : PressButton {

        private readonly Barrier barrier;

        public BarrierButton(Texture2D[] Textures, Vector2 location, SoundEffectInstance effect, bool deactivated, bool pushed, Barrier barrier) :
            base(Textures, location, effect, deactivated, pushed) {
            this.barrier = barrier;
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
