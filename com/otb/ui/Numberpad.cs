using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace OutsideTheBox {

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
        private string actualPass;

        private readonly Rectangle[] numberBounds;

        public Numberpad(Texture2D numberpad, Texture2D cursor, SpriteFont font, string name, bool active) :
            base(name, active) {
            this.numberpad = numberpad;
            this.cursor = cursor;
            this.font = font;
            this.pass = "";
            this.actualPass = "1776";
            this.location = new Vector2(253.0F, 15.0F);
            this.numberBounds = new Rectangle[] { new Rectangle(358, 408, 83, 83), new Rectangle(258, 108, 83, 83), new Rectangle(358, 108, 83, 83), new Rectangle(458, 108, 83, 83),
                new Rectangle(258, 208, 83, 83), new Rectangle(358, 208, 83, 83), new Rectangle(458, 208, 83, 83),
                new Rectangle(258, 308, 83, 83), new Rectangle(358, 308, 83, 83), new Rectangle(458, 308, 83, 83), new Rectangle(458, 408, 83, 83)};
        }

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
                            Console.WriteLine("Mouse clicked number " + i);
                        } else if (i == 10) {
                            if (pass == actualPass) {
                                Console.WriteLine("SUCCESS");
                                setActive(false);
                            } else {
                                Console.WriteLine(pass + " != " + actualPass);
                                pass = "";
                            }
                        }
                    }
                }
            }
            if (prevKey.IsKeyDown(Keys.Escape) && curKey.IsKeyUp(Keys.Escape)) {
                setActive(false);
            }
        }

        public override void draw(SpriteBatch batch) {
            batch.Draw(numberpad, Vector2.Zero, Color.White);
            Vector2 size = font.MeasureString(pass);
            Vector2 loc = new Vector2(location.X + ((292.0F - size.X) / 2.0F), location.Y + ((63.0F - size.Y) / 2.0F));
            batch.DrawString(font, pass, loc, Color.GhostWhite);
            batch.Draw(cursor, new Vector2(curMouse.X, curMouse.Y), Color.White);
        }
    }
}
