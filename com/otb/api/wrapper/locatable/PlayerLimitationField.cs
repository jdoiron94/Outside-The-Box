using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class PlayerLimitationField : Pit
    {
       public PlayerLimitationField(Texture2D texture, Vector2 location, int width, int height) :
           base(texture, location, width, height)
        {
           
        }

        public override void update(InputManager inputManager)
        {
            inputManager.getPlayerManager().setHealthLimit(false);
            inputManager.getPlayerManager().setManaLimit(false);
        }
    }
}
