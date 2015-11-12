using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class Barrier : GameObject {

        private readonly Texture2D open;
        private readonly Texture2D closed;

        private bool state;

        public Barrier(Texture2D[] textures, Vector2 location) :
            base(textures[0], location) {
            open = textures[0];
            closed = textures[1];
            state = false;
        }

        public Barrier(Texture2D[] textures, Vector2 location, bool state) :
            base(textures[0], location) {
            open = textures[0];
            closed = textures[1];
            this.state = state;
        }

        /// <summary>
        /// Sets the barrier's state
        /// </summary>
        /// <param name="value">The boolean to be set</param>
        public void setState(bool value) {
            state = value;
        }

        /// <summary>
        /// Returns whether or not the barrier is open
        /// </summary>
        /// <returns>Returns true if the barrier is open; otherwise, false</returns>
        public bool isOpen() {
            return state;
        }

        /// <summary>
        /// Draws the barrier
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(state ? open : closed, getLocation(), Color.White);
        }
    }
}
