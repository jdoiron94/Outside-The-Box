using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;

namespace OutsideTheBox {

    public class Hint : Screen {

        private readonly Texture2D gradient;
        private readonly Texture2D cursor;
        private readonly SpriteFont font1;
        private readonly SpriteFont font2;

        private readonly Rectangle bounds;

        private readonly string text;

        private MouseState prevMouse;
        private MouseState curMouse;
        private KeyboardState prevKey;
        private KeyboardState curKey;

        private int index;
        private bool showing;

        public Hint(Texture2D gradient, Texture2D cursor, SpriteFont font1, SpriteFont font2, string text, string name, bool active) :
            base(name, active) {
            this.gradient = gradient;
            this.cursor = cursor;
            this.font1 = font1;
            this.font2 = font2;
            this.text = wrapText(font1, text, 780);
            this.index = 1;
            this.showing = true;
            this.bounds = new Rectangle(710, 470, 90, 50);
        }

        /// <summary>
        /// Returns whether or not the hint is currently showing
        /// </summary>
        /// <returns>Returns true if the hint is currently showing; otherwise, false</returns>
        public bool isShowing() {
            return showing;
        }

        /// <summary>
        /// Sets the hint's showing status
        /// </summary>
        /// <param name="showing">The showing status to set</param>
        public void setShowing(bool showing) {
            this.showing = showing;
        }

        /// <summary>
        /// Adds a drop shadow to the specified text
        /// </summary>
        /// <param name="text">The text to shadow</param>
        /// <param name="font">The SpriteFont to draw with</param>
        /// <param name="location">The location to draw the text</param>
        /// <param name="batch">The SpriteBatch to draw with</param>
        private void shadowText(string text, SpriteFont font, Vector2 location, SpriteBatch batch) {
            batch.DrawString(font, text, new Vector2(location.X + 2, location.Y + 2), Color.DarkSlateGray);
            batch.DrawString(font, text, location, Color.White);
        }

        /// <summary>
        /// Adds newlines to the text so it will wrap around and not go off-screen horizontally
        /// </summary>
        /// <param name="spriteFont">The SpriteFont to draw with</param>
        /// <param name="text">The text to be drawn</param>
        /// <param name="maxWidth">The maximum length of the text before a newline is added</param>
        /// <returns>Returns the resulting wrapped text</returns>
        private string wrapText(SpriteFont spriteFont, string text, float maxWidth) {
            string result = "";
            float curWidth = 0.0F;
            float spaceWidth = spriteFont.MeasureString(" ").X;
            foreach (string word in text.Split()) {
                Vector2 size = spriteFont.MeasureString(word);
                if (curWidth + size.X < maxWidth) {
                    result += word + " ";
                    curWidth += size.X + spaceWidth;
                } else {
                    result += "\n" + word + " ";
                    curWidth = size.X + spaceWidth;
                }
            }
            return result;
        }

        public override void draw(SpriteBatch batch) {
            batch.Draw(gradient, Vector2.Zero, Color.White);
            Vector2 size = font1.MeasureString(text);
            Vector2 loc = new Vector2(400.0F - ((size.X - font1.MeasureString(" ").X) / 2.0F), 260.0F - (size.Y / 2.0F));
            batch.DrawString(font1, text, loc, Color.GhostWhite);
            if (index == 1) {
                shadowText("Back", font2, new Vector2(725F, 475F), batch);
            } else {
                batch.DrawString(font2, "Back", new Vector2(725F, 475F), Color.GhostWhite);
            }
            batch.Draw(cursor, new Vector2(curMouse.Position.X, curMouse.Position.Y), Color.White);
        }

        public override void update(GameTime time) {
            prevMouse = curMouse;
            curMouse = Mouse.GetState();
            prevKey = curKey;
            curKey = Keyboard.GetState();
            if (prevMouse.RightButton == ButtonState.Pressed && curMouse.RightButton == ButtonState.Pressed) {
                Console.WriteLine("Position: " + curMouse.Position);
            }
            bool hover = false;
            if (bounds.Contains(curMouse.Position)) {
                index = 1;
                hover = true;
            }
            if (!hover) {
                index = -1;
            }
            if (prevMouse.LeftButton == ButtonState.Pressed && curMouse.LeftButton == ButtonState.Released) {
                if (bounds.Contains(curMouse.Position)) {
                    showing = false;
                    return;
                }
            }
            if (prevKey.IsKeyDown(Keys.M) && curKey.IsKeyUp(Keys.M)) {
                showing = false;
                return;
            }
        }
    }
}
