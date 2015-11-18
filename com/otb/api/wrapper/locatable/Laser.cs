using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class Laser : Pit
    {
        private bool Activated;
        public Laser(Texture2D texture, Vector2 Location, int height, int width, bool Activated):
            base(texture, Location, width, height)
        {
            this.Activated = Activated; 
        }

        public bool isActivated()
        {
            return Activated; 
        }

        public void setActivated(bool value)
        {
            Activated = value; 
        }

        public override void draw(SpriteBatch batch)
        {
            if(Activated)
                batch.Draw(getTexture(), getSize(), Color.White);
        }
    }
}
