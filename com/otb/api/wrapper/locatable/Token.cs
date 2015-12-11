using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents an exp token
    /// </summary>

    public class Token : Collectible {

        private readonly int exp;
        private readonly TokenType type;

        public Token(Texture2D texture, Vector2 location, SoundEffect effect, TokenType type) :
            base(texture, location, effect, false) {
            this.type = type;
            this.exp = (int) type;
        }

        /// <summary>
        /// Returns the token's granted exp
        /// </summary>
        /// <returns>Returns the amount of exp granted by the token</returns>
        public int getExp() {
            return exp;
        }

        /// <summary>
        /// Returns the token's mana increment
        /// </summary>
        /// <returns>Returns the token's mana increment</returns>
        public int getManaIncrementationValue() {
            if (type == TokenType.Bronze) {
                return 2;
            } else if (type == TokenType.Silver) {
                return 5;
            } else if (type == TokenType.Gold) {
                return 10;
            } else if (type == TokenType.Emerald) {
                return 15;
            } else if (type == TokenType.Ruby) {
                return 20;
            } else if (type == TokenType.Diamond) {
                return 50;
            }
            return 0;
        }
    }
}
