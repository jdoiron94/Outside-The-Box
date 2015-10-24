using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents an exp token
    /// </summary>

    public class Token : GameObject {

        private int exp;
        private bool isCollected;

        private readonly TokenType type;
        private readonly Texture2D side;

        public Token(Texture2D texture, Texture2D side, Vector2 location, TokenType type) :
            base(texture, location) {
            this.type = type;
            this.side = side;
            exp = (int) type;
            isCollected = false;
        }

        /// <summary>
        /// Returns the token's granted exp
        /// </summary>
        /// <returns>Returns the amount of exp granted by the token</returns>
        public int getExp() {
            return exp;
        }

        /// <summary>
        /// Sets the token's collected status
        /// </summary>
        /// <param name="value">The collected status bool</param>
        public void setCollected(bool value) {
            isCollected = value;
        }

        /// <summary>
        /// Handles drawing of the token
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            if (!isCollected) {
                batch.Draw(getTexture(), getLocation(), Color.White);
            }
        }
    }
}
