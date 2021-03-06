﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles collectible objects
    /// </summary>

    public class Collectible : GameObject {

        private readonly SoundEffect effect;

        private bool collected;

        public Collectible(Texture2D Texture, Vector2 Location, SoundEffect effect, bool liftable) :
            base(Texture, Location, liftable) {
            this.effect = effect;
        }

        /// <summary>
        /// Sets the Collectible's collected status
        /// </summary>
        /// <param name="collected">The collected status bool</param>
        public void setCollected(bool collected) {
            this.collected = collected;
        }

        /// <summary>
        /// Gets the Collectible's collected status
        /// </summary>
        /// <returns>Returns the token's collected status</returns>
        public bool isCollected() {
            return collected;
        }

        /// <summary>
        /// Plays the collectible's sound effect
        /// </summary>
        public void playEffect() {
            if (effect != null) {
                effect.Play();
            }
        }

        /// <summary>
        /// Handles drawing of the token
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public override void draw(SpriteBatch batch, int mode) {
            if (!collected) {
                if (mode == 0) {
                    batch.Draw(getTexture(), getLocation(), Color.White);
                } else if (mode == 1) {
                    batch.Draw(getTexture(), getLocation(), (isLiftable() ? Color.LightGreen : Color.White));
                } else {
                    batch.Draw(getTexture(), getLocation(), (isSelected() ? Color.IndianRed : Color.White));
                }
            }
        }
    }
}