using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class Pit : GameObject
    {
        private Rectangle size;

        public Pit(Texture2D texture, Vector2 location, int width, int height):
            base(texture, location)
        {
            size = new Rectangle((int) getLocation().X, (int) getLocation().Y, width, height); 
        }

        public virtual void update(InputManager inputManager)
        {

        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(getTexture(), size, Color.White);
        }
    }
}
