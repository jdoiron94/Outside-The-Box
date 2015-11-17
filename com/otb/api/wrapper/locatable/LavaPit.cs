using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class LavaPit : Pit
    {
        private int damage; 
        
        public LavaPit(Texture2D texture, Vector2 location, int width, int height):
            base(texture, location, width, height)
        {
            damage = 5; 
        }

        public int getDamage()
        {
            return damage; 
        }

        public override void update(InputManager inputManager)
        {
            inputManager.getPlayerManager().damagePlayer(damage);
        }
    }
}
