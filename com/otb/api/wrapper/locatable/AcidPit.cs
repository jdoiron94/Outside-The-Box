using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class AcidPit : Pit
    {
        private int damage;

        public AcidPit(Texture2D texture, Vector2 location, int width, int height) :
            base(texture, location, width, height)
        {
            damage = 2;
        }

        public int getDamage()
        {
            return damage;
        }

        public override void update(InputManager inputManager)
        {
            inputManager.getPlayerManager().depleteMana(damage);
        }
    }
}

