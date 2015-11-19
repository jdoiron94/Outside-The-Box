using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class ManaLaser : Laser
    {
        public ManaLaser(Texture2D texture, Vector2 location, int height, int width, bool Activated):
            base(texture, location, height, width, Activated)
        {

        }

        public override void update(InputManager inputManager)
        {
            if(isActivated())
                inputManager.getPlayerManager().depleteMana(50);
        }
    }
}
