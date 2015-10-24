using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents an exp token
    /// </summary>

    public class Token : GameObject {

        private int expValue;
        private bool isCollected;

        private TokenType Type;
        private Texture2D side;

        public Token(Texture2D texture, Vector2 location, TokenType Type, Texture2D side) : base(texture, location) {
            this.Type = Type;
            expValue = setExp(Type);
            this.side = side;
            isCollected = false;
        }

        /// <summary>
        /// Sets the token's exp relative to its type
        /// </summary>
        /// <param name="type">The token's type</param>
        /// <returns>Returns the exp granted for taking the token</returns>
        public int setExp(TokenType type) {
            switch ((int) type) {
                case 0:
                    return 100;
                case 1:
                    return 250;
                case 2:
                    return 500;
                case 3:
                    return 1000;
                case 4:
                    return 2000;
                case 5:
                    return 5000;
                case 6:
                    return 10000;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Returns the token's granted exp
        /// </summary>
        /// <returns>Returns the amount of exp granted by the token</returns>
        public int getExp() {
            return expValue;
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
