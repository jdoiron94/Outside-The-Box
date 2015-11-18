using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class LavaPit : Pit
    {
        private int damage; 
        
        public LavaPit(Texture2D texture, Vector2 location, int width, int height, SoundEffect sound):
            base(texture, location, width, height, sound)
        {
            damage = 2;
            setEffect(sound);
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
