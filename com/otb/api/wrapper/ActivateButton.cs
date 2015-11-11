using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper
{
    public class ActivateButton : PressButton
    {
        private PressButton button; 

        public ActivateButton(Texture2D[] Textures, Vector2 location, bool deactivated, bool pushed, PressButton button) :
            base(Textures, location, deactivated, pushed)
        {
            this.button = button; 
        }

        public PressButton getButton()
        {
            return button;
        }

        public override void update()
        {
            if (!isDeactivated())
               button.setDeactivated(isPushed() ? false : true);
        }
    }
}
