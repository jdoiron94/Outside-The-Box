using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents an exp token
    /// </summary>

    public class Token : GameObject {

        private int exp;
        private bool collected;

        private readonly TokenType type;
        private readonly Texture2D side;

        public Token(Texture2D texture, Texture2D side, Vector2 location, TokenType type) :
            base(texture, location)
        {
            this.type = type;
            this.side = side; 
            exp = (int) type;
            collected = false;
        }

        /// <summary>
        /// Returns the token's granted exp
        /// </summary>
        /// <returns>Returns the amount of exp granted by the token</returns>
        public int getExp() {
            return exp;
        }

        public int getManaIncrementationValue()
        {
            if(type == TokenType.Bronze)
            {
                return 2; 
            }else if(type == TokenType.Silver)
            {
                return 5; 
            }else if(type == TokenType.Gold)
            {
                return 10; 
            }else if(type == TokenType.Emerald)
            {
                return 15; 
            }else if(type == TokenType.Ruby)
            {
                return 20; 
            }else if(type == TokenType.Diamond)
            {
                return 50; 
            }
            return 0; 
        }

        /// <summary>
        /// Sets the token's collected status
        /// </summary>
        /// <param name="value">The collected status bool</param>
        public void setCollected(bool value) {
            collected = value;
        }

        /// <summary>
        /// Gets the token's collected status
        /// </summary>
        /// <returns>Returns the token's collected status</returns>
        public bool isCollected()
        {
            return collected; 
        }

        /// <summary>
        /// Handles drawing of the token
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            if (!collected) {
                batch.Draw(getTexture(), getLocation(), Color.White);
            }
        }
    }
}
