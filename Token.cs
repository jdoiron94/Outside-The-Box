using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp
{
    public class Token : GameObject
    {
        private TokenType Type;
        private int expValue;
        private Texture2D side; 

        public Token(Texture2D texture, Vector2 location, TokenType Type, Texture2D side):base(texture, location)
        {
            this.Type = Type;
            expValue = setExp(Type);
            this.side = side; 
        }

        public int setExp(TokenType Type)
        {
            switch((int) Type)
            {
                case 0: return 100;
                case 1: return 250; 
                case 2: return 500;
                case 3: return 1000;
                case 4: return 2000;
                case 5: return 5000;
                case 6: return 10000;
                default: return 0;  
            }
        }

        public int getExp()
        {
            return expValue; 
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(getTexture(), getLocation(), Color.White);
        }
    }
}
