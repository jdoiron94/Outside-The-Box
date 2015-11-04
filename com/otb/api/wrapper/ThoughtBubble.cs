using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents thought bubbles from npcs
    /// </summary>

    public class ThoughtBubble : GameObject {

        private readonly Npc npc;
        private SpriteFont font;
        private bool revealed;
        private bool key;


        public ThoughtBubble(Texture2D texture, SpriteFont font, Vector2 Location, Npc npc, bool revealed, bool key) :
            base(texture, Location) {
            this.npc = npc;
            setThoughtLocation(npc.getLocation());
            revealed = false;
            key = false;
            this.font = font;
        }

        /// <summary>
        /// Sets the thought bubble's location
        /// </summary>
        /// <param name="location">The location to set</param>
        public void setThoughtLocation(Vector2 location) {
            setLocation(new Vector2(location.X + 25F, location.Y - 80F));
        }

        /// <summary>
        /// Returns whether or not the thought bubble has been revealed
        /// </summary>
        /// <returns>Returns true if the thought bubble has been revealed; otherwise, false</returns>
        public bool isRevealed() {
            return revealed;
        }

        /// <summary>
        /// Updates the thought bubble's location relative to the owner's location
        /// </summary>
        public void updateLocation() {
            setThoughtLocation(npc.getLocation());
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool isKey() {
            return key;
        }

        /// <summary>
        /// Updates the revealed status
        /// </summary>
        /// <param name="r">The reveal status to set</param>
        public void reveal(bool r) {
            revealed = r;
        }

        /// <summary>
        /// Sets the thought bubble's key status
        /// </summary>
        public void setKey() {
            key = true;
        }

        /// <summary>
        /// Handles drawing of the thought bubble
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            if (revealed) {
                batch.Draw(getTexture(), getLocation(), Color.White);
                batch.DrawString(font, "ayylmao", getLocation(), Color.Black);

            }
        }
    }
}
