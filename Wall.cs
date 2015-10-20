using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp
{
    public class Wall : GameObject
    {

        private Rectangle rect; 

        public Wall(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, bool liftable, bool next, int width, int height) : 
        base(texture, projectile, location, direction, false, width, height)
        {
            rect = new Rectangle((int)location.X, (int)location.Y, width, height);
            
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(getTexture(), rect, Color.White);
        }
    }
}
