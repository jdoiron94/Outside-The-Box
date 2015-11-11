using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class barrier : GameObject
    {
        private Texture2D open;
        private Texture2D closed;
        private bool state; 

        public barrier(Texture2D[] textures, Vector2 location) : 
            base(textures[0], location)
        {
            open = textures[0];
            closed = textures[1];
            state = false; 
        }

        public barrier(Texture2D[] textures, Vector2 location, bool state) :
            base(textures[0], location)
        {
            open = textures[0];
            closed = textures[1];
            this.state = state;
        }

        public void setState(bool value)
        {
            state = value; 
        }

        public bool isOpen()
        {
            return state; 
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(state ? open : closed, getLocation(), Color.White);
        }

    }
}
