using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class TwoInputBarrier : Barrier
    {
        private PressButton input_1;
        private PressButton input_2;

        public TwoInputBarrier(Texture2D[] Textures, Vector2 Location, PressButton input_1, PressButton input_2):
            base(Textures, Location)
        {
            this.input_1 = input_1;
            this.input_2 = input_2; 
        }

        public void update()
        {
            if(input_1.isPushed() && input_2.isPushed())
            {
                setState(true);
            }
            else
            {
                setState(false);
            }
        }
    }
}
