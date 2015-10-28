using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox { 

    public class PressButton : GameObject
    {
        private Texture2D texture;

        private Vector2 location;
        private Rectangle bounds;
        private bool pushable;
        private bool pushed;

        public PressButton(Texture2D texture,Vector2 location, bool pushable, bool pushed) :
            base(texture, location) {
                //this.texture = texture;
                this.pushable = pushable;
                //this.location = location;
                this.pushed = pushed;
            }
            

        /// <summary>
        /// Returns the game object's texture
        /// </summary>
        /// <returns>Returns the game object's texture</returns>
        public Texture2D getTexture()
        {
            return texture;
        }


        /// <summary>
        /// Returns the game object's location
        /// </summary>
        /// <returns>Returns the game object's location</returns>
        public Vector2 getLocation()
        {
            return location;
        }


        public void setLocation(Vector2 location)
        {
            this.location = location;
        }
    
        /// <summary>
        /// Returns the game object's bounds
        /// </summary>
        /// <returns></returns>
        public Rectangle getBounds()
        {
            return bounds;
        }

        public void setBounds(int y, int x)
        {
            bounds.Height = y;
            bounds.Width = x;
        }

        public void setPushed(bool pushed)
        {
            this.pushed = pushed;
        }

        /// <summary>
        /// Returns if the game object can be pushable
        /// </summary>
        /// <returns>Returns true if the game object is pushable; otherwise, false</returns>
        public bool isPushable()
        {
            return pushable;
        }

        /// <summary>
        /// Returns if the game object is pushed
        /// </summary>
        /// <returns>Returns true if the game object is pushed; otherwise, false</returns>
        public bool isPushed()
        {
            return pushed;
        }

  
        /// <summary>
        /// Derives the game object's x coordinate in location and bounds by the specified x amount
        /// </summary>
        /// <param name="x">The x amount to be derived by</param>
        public void deriveX(int x)
        {
            location.X += x;
            bounds.X += x;
        }

        /// <summary>
        /// Derives the game object's y coordinate in location and bounds by the specified y amount
        /// </summary>
        /// <param name="y">The y amount to be derived by</param>
        public void deriveY(int y)
        {
            location.Y += y;
            bounds.Y += y;
        }


        /// <summary>
        /// Returns whether or not the game object is currently on the screen
        /// </summary>
        /// <param name="game">The game instance to check its viewport bounds</param>
        /// <returns>Returns true if the game object is currently on the screen; otherwise, false</returns>
        public bool isOnScreen(Game1 game)
        {
            return location.X >= -texture.Width && location.X <= game.getWidth() && location.Y >= -texture.Height && location.Y <= game.getHeight();
        }

        /// <summary>
        /// Draws the game object
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        /// <param name="mode">The game's telekinesis mode to draw with respect to</param>
        public void draw(SpriteBatch batch, byte mode)
        {

                batch.Draw(texture, location, Color.White);

        }
    }
}
