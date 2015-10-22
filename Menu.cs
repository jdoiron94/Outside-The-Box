using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KineticCamp {

    public class Menu {

        private Texture2D background;
        private Button[] buttons;
        private InputManager inputManager;
        private bool active;

        public Menu(Texture2D texture, Button[] buttons) {
            this.background = texture;
            this.buttons = buttons;
            this.active = false;
            //this.inputManager = inputManager;
        }

        public void setInputManager(InputManager inputManager) {
            this.inputManager = inputManager;
        }

        public bool isActive() {
            //return true;
            return active;
        }

        public Button[] getButtons() {
            return buttons;
        }

        public void setActive(bool active) {
            this.active = active;
        }

        public void reactToMouseClick() {
            for (int i = 0; i < buttons.Length; i++) {
                if (buttons[i] != null) {
                    Point p = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                    if (p != null && buttons[i].getBounds().Contains(p)) {
                        switch (i) {
                            case 0:
                                buttons[i].exitMenu(inputManager);
                                break;
                            case 1:
                                buttons[i].unlockPower(inputManager);
                                break;
                            default:
                                Console.WriteLine("oops");
                                break;
                        }
                    }
                }
            }
        }

        public void draw(SpriteBatch batch) {
            batch.Draw(background, new Vector2(0, 0), Color.White);
            foreach (Button butt in buttons) {
                batch.Draw(butt.getTexture(), butt.getLocation(), Color.White);
            }
        }
    }
}
