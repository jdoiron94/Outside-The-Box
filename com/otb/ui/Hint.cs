using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
            this.text = text;
            this.index = 1;
            this.showing = true;
            this.bounds = new Rectangle(710, 470, 90, 50);
        }

        public Texture2D getGradient() {
            return gradient;
        }

        public Texture2D getCursor() {
            return cursor;
        }

        public SpriteFont getFont1() {
            return font1;
        }

        public SpriteFont getFont2() {
            return font2;
        }

        public string getText() {
            return text;
        }

        public bool isShowing() {
            return showing;
        }

        public void setShowing(bool showing) {
            this.showing = showing;
        }

        private void shadowText(string text, SpriteFont font, Vector2 location, SpriteBatch batch) {
            batch.DrawString(font, text, new Vector2(location.X + 2, location.Y + 2), Color.DarkSlateGray);
            batch.DrawString(font, text, location, Color.White);
        }

        private void wrapText() {
            string[] split = text.Split(' ');
            foreach (string s in split) {
                Console.WriteLine(s);
            }
            setActive(false);
            //for (int i = 0; i < 
        }

        public override void draw(SpriteBatch batch) {
            batch.Draw(gradient, Vector2.Zero, Color.White);
            Vector2 size = font1.MeasureString(text);
            Vector2 loc = new Vector2(400.0F - (size.X / 2.0F), 260.0F - (size.Y / 2.0F));
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
