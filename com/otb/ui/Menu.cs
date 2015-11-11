using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents a menu
    /// </summary>

    public class Menu {

        private Texture2D background;
        private Button[] buttons;
        private InputManager inputManager;
        private bool active;

        public Menu(Texture2D texture, Button[] buttons) {
            this.background = texture;
            this.buttons = buttons;
            this.active = false;
        }

        /// <summary>
        /// Sets the input manager
        /// </summary>
        /// <param name="inputManager">The input manager to set</param>
        public void setInputManager(InputManager inputManager) {
            this.inputManager = inputManager;
        }

        /// <summary>
        /// Returns whether or not the menu is active
        /// </summary>
        /// <returns>Returns true if the menu is active; otherwise, false</returns>
        public bool isActive() {
            return active;
        }

        /// <summary>
        /// Returns the menu's button array
        /// </summary>
        /// <returns>Returns the menu's buttons</returns>
        public Button[] getButtons() {
            return buttons;
        }

        /// <summary>
        /// Sets the menu's active status
        /// </summary>
        /// <param name="active">The active status to set</param>
        public void setActive(bool active) {
            this.active = active;
        }

        /// <summary>
        /// Handles mouse input
        /// </summary>
        public void reactToMouseClick() {
            for (int i = 0; i < buttons.Length; i++) {
                if (buttons[i].getBounds().Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) {
                    int id = buttons[i].getActionID();
                    switch (id) {
                        case 0:
                            //unlock power 0
                            buttons[i].unlockPower(inputManager, 0);
                            break;
                        case 1:
                            buttons[i].unlockPower(inputManager, 1);
                            break;
                        case 2:
                            buttons[i].unlockPower(inputManager, 2);
                            break;
                        case 3:
                            buttons[i].unlockPower(inputManager, 2);
                            break;
                        case 4:
                            buttons[i].displayInstructions(inputManager);
                            break;
                        case 5:
                            buttons[i].unlockPower(inputManager, 2);
                            break;
                        case 6:
                            buttons[i].unlockPower(inputManager, 2);
                            break;
                        case 7:
                            buttons[i].unlockPower(inputManager, 2);
                            break;
                        case 8:
                            buttons[i].displayInstructions(inputManager);
                            Console.WriteLine("hfjsdhfksdkfjbsdf");
                            break;
                        case 9:
                            buttons[i].displayInstructions(inputManager);
                            Console.WriteLine("hfjsdhfksdkfjbsdf");
                            break;
                        case 10:
                            buttons[i].displayInstructions(inputManager);
                            break;
                        default:
                            Console.WriteLine("oops");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles drawing of the menu
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(background, Vector2.Zero, Color.White);
            foreach (Button b in buttons) {
                batch.Draw(b.getTexture(), b.getLocation(), Color.White);
            }
        }
    }
}
