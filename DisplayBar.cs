using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace KineticCamp
{
    public class DisplayBar
    {
        private Texture2D texture;
        private Texture2D borderText; 
        private Vector2 location;
        private Rectangle displayBar;
        private Rectangle backBar;
        private Rectangle OutlineBar; 
        private Rectangle bounds; 
        private Color displayColor;

        #region Constructors

        public DisplayBar(Texture2D texture, Vector2 location, Color displayColor, Texture2D borderText)
        {
            this.texture = texture;
            this.borderText = borderText;
            this.location = location;
            this.displayBar = new Rectangle((int)location.X, (int)location.Y+5, 200, 10);
            this.backBar = new Rectangle((int)location.X, (int)location.Y+5, 200, 10);
            this.OutlineBar = new Rectangle((int)location.X-10, (int)location.Y, 220, 20);
            this.displayColor = displayColor;
            bounds = new Rectangle((int)location.X, (int)location.Y, 1, 1);
        }

        #endregion

        #region get Methods

        public Texture2D getTexture()
        {
            return texture; 
        } 

        public Vector2 getLocation()
        {
            return location; 
        }

        public Rectangle getDisplayBar()
        {
            return displayBar; 
        }

        public Color getDisplayColor()
        {
            return displayColor; 
        }

        #endregion

        #region set Methods

        public void setTexture(Texture2D texture)
        {
            this.texture = texture; 
        }

        public void setLocation(Vector2 location)
        {
            this.location = location; 
        }

        public void setDisplayBar(Rectangle displayBar)
        {
            this.displayBar = displayBar; 
        }

        public void setDisplayColor(Color displayColor)
        {
            this.displayColor = displayColor;
        }

        #endregion

        #region ScreenManagement

        public void draw(SpriteBatch batch)
        {
            batch.Draw(borderText, OutlineBar, Color.White);
            batch.Draw(texture, backBar, Color.Black);
            batch.Draw(texture, displayBar, Color.White);
        }
        #endregion
    }
}
