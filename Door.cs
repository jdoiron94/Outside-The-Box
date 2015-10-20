using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp 
{
    public class Door : GameObject
    {

        private bool next;
        private bool unlocked;
        private Rectangle rect;
        

        public Door(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, bool liftable, bool next, int width, int height) : 
        base(texture, projectile, location, direction, liftable)
        {
            this.next = next;
            rect = new Rectangle((int)location.X, (int)location.Y, width, height);
            unlocked = false; 
        }

        public void setNext(bool next)
        {
            this.next = next; 
        }

        public bool getNext()
        {
            return next; 
        }

        public Rectangle getRect()
        {
            return rect; 
        }

        public bool isUnlocked()
        {
            return unlocked; 
        }

        public void unlockDoor()
        {
            unlocked = true; 
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(getTexture(), rect, Color.White);
        }
    }
}
