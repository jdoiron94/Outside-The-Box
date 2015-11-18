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
        private Rectangle bounds; 

        public Pit(Texture2D texture, Vector2 location, int width, int height):
            base(texture, location)
        {
            size = new Rectangle((int) getLocation().X, (int) getLocation().Y, width, height);
            bounds = new Rectangle((int)getLocation().X, (int)getLocation().Y, width, height);

            setBounds(bounds);
            setDestinationBounds(bounds);
        }

        public Rectangle getSize()
        {
            return size; 
        }

        public Rectangle getPitBounds()
        {
            return bounds; 
        }

        public void setPitBounds(bool orientation)
        {
            if (orientation == true)
            {
                bounds = new Rectangle((int)getLocation().X, (int)getLocation().Y, 1, bounds.Y);
            }
            else
            {
                bounds = new Rectangle((int)getLocation().X, (int)getLocation().Y, bounds.X, 1);
            }
        }

        public virtual void update(InputManager inputManager)
        {

        }

        public virtual void draw(SpriteBatch batch)
        {
            batch.Draw(getTexture(), size, Color.White);
        }
    }
}
