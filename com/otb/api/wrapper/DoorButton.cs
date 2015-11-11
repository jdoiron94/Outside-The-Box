using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper
{
    public class DoorButton : PressButton
    {
        private Door door;

        public DoorButton(Texture2D[] Textures, Vector2 location, bool deactivated, bool pushed, Door barrier) :
            base(Textures, location, deactivated, pushed)
        {
            this.door = barrier;
        }

        public Door getDoor()
        {
            return door;
        }

        public override void update()
        {
            if (!isDeactivated())
                door.unlockDoor(isPushed() ? true : false); 
        }
    }
}
