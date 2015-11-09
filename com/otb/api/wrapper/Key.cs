using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OutsideTheBox.com.otb.api.wrapper.locatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper
{
    public class Key : Collectible
    {
        private bool unlocked; 

        public Key(Texture2D Texture, Vector2 Location):
            base(Texture, Location, true)
        {
            unlocked = false; 
        }

        public void setUnlocked(bool value)
        {
            unlocked = value; 
        }

        public bool isUnlocked()
        {
            return unlocked; 
        }
    }
}
