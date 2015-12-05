using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles the numberpad puzzle
    /// </summary>

    public class Numberpad : Screen {

        private readonly Texture2D numberpad;
        private readonly Texture2D cursor;
        private readonly SpriteFont font;
        private readonly Vector2 location;

        private MouseState prevMouse;
        private MouseState curMouse;
        private KeyboardState prevKey;
        private KeyboardState curKey;

        private string pass;
        private string enteredPass;
        private string actualPass;

        private readonly Keys[] numbers;
        private readonly Rectangle[] numberBounds;

        public Numberpad(Texture2D numberpad, Texture2D cursor, SpriteFont font, string name, bool active) :
            base(name, active) {
            this.numberpad = numberpad;
            this.cursor = cursor;
            this.font = font;
            this.pass = "";
            this.actualPass = "1776";
            this.location = new Vector2(253.0F, 15.0F);
            this.numbers = new Keys[] { Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4,
                Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9, Keys.D0, Keys.D1, Keys.D2,
                Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9 };
            this.numberBounds = new Rectangle[] { new Rectangle(358, 408, 83, 83), new Rectangle(258, 108, 83, 83), new Rectangle(358, 108, 83, 83), new Rectangle(458, 108, 83, 83),
                new Rectangle(258, 208, 83, 83), new Rectangle(358, 208, 83, 83), new Rectangle(458, 208, 83, 83),
                new Rectangle(258, 308, 83, 83), new Rectangle(358, 308, 83, 83), new Rectangle(458, 308, 83, 83), new Rectangle(458, 408, 83, 83)};
        }

        /// <summary>
        /// Returns whether or not the numberpad was successfully cracked
        /// </summary>
        /// <returns>Returns true if successfully solved; otherwise, false</returns>
        public bool solved() {
            return enteredPass == actualPass;
        }

        /// <summary>
        /// Handles updating of the numberpad
        /// </summary>
        /// <param name="time">The GameTime to respect</param>
        public override void update(GameTime time) {
            prevMouse = curMouse;
            curMouse = Mouse.GetState();
            prevKey = curKey;
            curKey = Keyboard.GetState();
            if (prevMouse.LeftButton == ButtonState.Pressed && curMouse.LeftButton == ButtonState.Released) {
                for (int i = 0; i < numberBounds.Length; i++) {
                    if (numberBounds[i].Contains(curMouse.Position)) {
                        if (i != 10 && pass.Length < 4) {
                            pass += i;
                        } else if (i == 10) {
                            if (pass == actualPass) {
                                setActive(false);
                                enteredPass = pass;
                                pass = "";
                            } else {
                                pass = "";
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < numbers.Length; i++) {
                Keys k = numbers[i];
                if (prevKey.IsKeyDown(k) && curKey.IsKeyUp(k)) {
                    if (pass.Length < 4) {
                        if (i >= 10) {
                            pass += i - 10;
                            break;
                        }
                        pass += i;
                        break;
                    }
                }
            }
            if (prevKey.IsKeyDown(Keys.Enter) && curKey.IsKeyUp(Keys.Enter)) {
                if (pass == actualPass) {
                    setActive(false);
                    enteredPass = pass;
                    pass = "";
                } else {
                    pass = "";
                }
            } else if (prevKey.IsKeyDown(Keys.Escape) && curKey.IsKeyUp(Keys.Escape)) {
                setActive(false);
                enteredPass = pass;
                pass = "";
            }
        }

        /// <summary>
        /// Handles drawing of the numberpad
        /// </summary>
        /// <param name="batch">TheSpriteBatch to draw with</param>
        public override void draw(SpriteBatch batch) {
            batch.Draw(numberpad, Vector2.Zero, Color.White);
            Vector2 size = font.MeasureString(pass);
            Vector2 loc = new Vector2(location.X + ((292.0F - size.X) / 2.0F), location.Y + ((63.0F - size.Y) / 2.0F));
            batch.DrawString(font, pass, loc, Color.GhostWhite);
            batch.Draw(cursor, new Vector2(curMouse.X, curMouse.Y), Color.White);
        }
    }
}
