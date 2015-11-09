using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class Key : GameObject
    {
        private bool collected;

        public Key(Texture2D Texture, Vector2 Location, bool liftable):
        base(Texture, Location, liftable)
        {
            collected = false; 
        }

        /// <summary>
        /// Sets the token's collected status
        /// </summary>
        /// <param name="value">The collected status bool</param>
        public void setCollected(bool value)
        {
            collected = value;
        }

        /// <summary>
        /// Gets the token's collected status
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
        public void draw(SpriteBatch batch)
        {
            if (!collected)
            {
                batch.Draw(getTexture(), getLocation(), Color.White);
            }
        }
    }
}
