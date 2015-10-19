using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KineticCamp
{
    class Menu
    {
        private Texture2D background;
        private Button[] buttons;
        private InputManager inputManager;
        private bool active;

        public Menu(Texture2D texture, Button[] buttons)
	    {
            this.background = texture;
            this.buttons = buttons;
            this.active = false;
            //this.inputManager = inputManager;
	    }

        public void setInputManager(InputManager inputManager)
        {
            this.inputManager = inputManager;
        }

        public bool isActive()
        {
            //return true;
            return active;
        }

        public void setActive(bool active)
        {
            this.active = active;
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(background, new Vector2(50, 100), Color.White);

            foreach (Button b in buttons) {
                batch.Draw(b.getTexture(), b.getLocation(), Color.White);
            }
        }

    }
}

