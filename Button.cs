using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp
{
    public class Button
    {

        private Texture2D texture;
        private Vector2 location;
        private Rectangle bounds;

        public Button(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            this.location = location;
            bounds = new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height);
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public Vector2 getLocation()
        {
            return location;
        }

        public Rectangle getBounds()
        {
            return bounds;
        }

        public void exitMenu(ScreenManager screenManager)
        {
            screenManager.setActiveScreen(1);
        }


    }
}
