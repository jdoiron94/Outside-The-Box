using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OutsideTheBox.com.otb.api.wrapper.locatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper
{
    public class BarrierButton : PressButton
    {
        private barrier barrier; 

        public BarrierButton(Texture2D[] Textures, Vector2 location, bool deactivated, bool pushed, barrier barrier):
            base(Textures, location, deactivated, pushed)
        {
            this.barrier = barrier; 
        }

        public barrier getBarrier()
        {
            return barrier;
        }

        public override void update()
        {
            if (!isDeactivated())
                barrier.setState(isPushed() ? true : false);
            else
                barrier.setState(false);
        }
    }
}
