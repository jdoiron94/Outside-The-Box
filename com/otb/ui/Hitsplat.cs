using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class Hitsplat {

        private readonly SpriteFont font;
        private readonly Texture2D splat;
        private Vector2 location;
        private readonly SoundEffectInstance effect;

        private string hit;

        public Hitsplat(SpriteFont font, Texture2D splat, Vector2 location, SoundEffectInstance effect) {
            this.font = font;
            this.splat = splat;
            this.location = location;
            this.effect = effect;
        }

        public SoundEffectInstance getEffect() {
            return effect;
        }

        /// <summary>
        /// Returns the hitsplat's texture
        /// </summary>
        /// <returns>Returns the hitsplat's texture</returns>
        public Texture2D getSplat() {
            return splat;
        }

        /// <summary>
        /// Returns the hitsplat's hit
        /// </summary>
        /// <returns>Returns the hitsplat's hit</returns>
        public string getHit() {
            return hit;
        }

        /// <summary>
        /// Sets the hitsplat's hit
        /// </summary>
        /// <param name="hit">The hit to set</param>
        public void setHit(string hit) {
            this.hit = hit;
        }

        /// <summary>
        /// Derives the x coordinate bounds
        /// </summary>
        /// <param name="x">The x amount to derive by</param>
        public void deriveX(int x) {
            location.X += x;
        }

        /// <summary>
        /// Derives the y coordinate bounds
        /// </summary>
        /// <param name="x">The y amount to derive by</param>
        public void deriveY(int y) {
            location.Y += y;
        }

        public void playEffect() {
            if (effect != null && effect.State != SoundState.Playing) {
                effect.Play();
            }
        }

        /// <summary>
        /// Draws the hitsplat
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(splat, location, Color.White);
            Vector2 size = font.MeasureString(hit);
            Vector2 loc = new Vector2(location.X + ((splat.Width - size.X) / 2), location.Y + ((splat.Height - size.Y) / 2));
            batch.DrawString(font, hit, loc, Color.GhostWhite);
        }
    }
}
