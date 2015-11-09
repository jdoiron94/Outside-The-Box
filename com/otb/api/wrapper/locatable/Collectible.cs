using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class Collectible : GameObject
    {
        private bool collected; 

        public Collectible(Texture2D Texture, Vector2 Location, bool liftable):
            base(Texture, Location, liftable)
        {
            collected = false;  
        }

        /// <summary>
        /// Sets the Collectible's collected status
        /// </summary>
        /// <param name="value">The collected status bool</param>
        public void setCollected(bool value)
        {
            collected = value; 
        }

        /// <summary>
        /// Gets the Collectible's collected status
        /// </summary>
        /// <returns>Returns the token's collected status</returns>
        public bool isCollected()
        {
            return collected; 
        }

        /// <summary>
        /// Handles drawing of the token
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public override void draw(SpriteBatch batch, byte mode)
        {
            if(!collected)
            {
                if (mode == 0)
                {
                    batch.Draw(getTexture(), getLocation(), Color.White);
                }
                else if (mode == 1)
                {
                    batch.Draw(getTexture(), getLocation(), (isLiftable() ? Color.LightGreen : Color.White));
                }
                else
                {
                    batch.Draw(getTexture(), getLocation(), (isSelected() ? Color.IndianRed : Color.White));
                }
            }
        }
    }
}
