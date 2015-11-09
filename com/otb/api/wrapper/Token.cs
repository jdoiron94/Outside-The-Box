using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OutsideTheBox.com.otb.api.wrapper.locatable;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents an exp token
    /// </summary>

    public class Token : Collectible {

        private int exp;

        private readonly TokenType type;
        private readonly Texture2D side;

        public Token(Texture2D texture, Texture2D side, Vector2 location, TokenType type) :
            base(texture, location, false)
        {
            this.type = type;
            this.side = side; 
            exp = (int) type;
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
    }
}
