using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp
{
    class Button
    {

        private Texture2D texture;
        private Vector2 location;

        public Button(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            this.location = location;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public Vector2 getLocation()
        {
            return location;
        }

        public void exitMenu(ScreenManager screenManager)
        {
            screenManager.setActiveScreen(1);
        }





    }
}
