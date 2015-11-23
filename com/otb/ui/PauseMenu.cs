using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace OutsideTheBox {

    public class PauseMenu : Screen {

        private readonly Texture2D gradient;
        private readonly Texture2D cursor;
        private readonly SpriteFont xSmall;
        private readonly SpriteFont small;
        private readonly SpriteFont large;

        private readonly string pause;
        private readonly string exp;
        private readonly string hint;

        private readonly Rectangle[] textBounds;

        private List<Hint> hints;
        private MouseState prevMouse;
        private MouseState curMouse;
        private KeyboardState prevKey;
        private KeyboardState curKey;

        private bool viewingHint;
        private int index;
        private int level;
        private int experience;

        public PauseMenu(Texture2D gradient, Texture2D cursor, SpriteFont xSmall, SpriteFont small, SpriteFont large, string name, bool active) :
            base(name, active) {
            this.gradient = gradient;
            this.cursor = cursor;
            this.xSmall = xSmall;
            this.small = small;
            this.large = large;
            this.pause = "PAUSE";
            this.exp = "EXP: ";
            this.hint = "HINT";
            this.hints = new List<Hint>(5);
            this.viewingHint = false;
            this.textBounds = new Rectangle[] { new Rectangle(0, 415, 270, 105), new Rectangle(580, 415, 220, 105), new Rectangle(715, 0, 85, 30) };
            this.index = 1;
            this.level = 0;
            this.experience = 0;
        }

        public Texture2D getGradient() {
            return gradient;
        }

        public Texture2D getCursor() {
            return cursor;
        }

        public SpriteFont getExtraSmallFont() {
            return xSmall;
        }

        public SpriteFont getSmallFont() {
            return small;
        }

        public SpriteFont getLargeFont() {
            return large;
        }

        public int getLevel() {
            return level;
        }

        public int getExperience() {
            return experience;
        }

        public void addHint(Hint hint) {
            hints.Add(hint);
        }

        public void setLevel(int level) {
            this.level = level;
        }

        public void setExperience(int experience) {
            this.experience = experience;
        }

        private void shadowText(string text, SpriteFont font, Vector2 location, SpriteBatch batch) {
            batch.DrawString(font, text, new Vector2(location.X + 2.0F, location.Y + 2.0F), Color.DarkSlateGray);
            batch.DrawString(font, text, location, Color.White);
        }

        public override void draw(SpriteBatch batch) {
            batch.Draw(gradient, Vector2.Zero, Color.White);
            if (viewingHint) {
                Hint h = hints[level];
                h.draw(batch);
                return;
            }
            Vector2 pauseSize = large.MeasureString(pause);
            Vector2 pauseLoc = new Vector2(400.0F - (pauseSize.X / 2.0F), 260.0F - (pauseSize.Y / 2.0F));
            Vector2 expSize = small.MeasureString(exp);
            Vector2 expLoc = new Vector2(15.0F, 520.0F - expSize.Y - 10.0F);
            Vector2 hintSize = small.MeasureString(hint);
            Vector2 hintLoc = new Vector2(800.0F - hintSize.X - 15.0F, 520.0F - hintSize.Y - 10.0F);
            batch.DrawString(large, pause, pauseLoc, Color.GhostWhite);
            if (index == 1) {
                shadowText(exp + experience, small, expLoc, batch);
            } else {
                batch.DrawString(small, exp + experience, expLoc, Color.GhostWhite);
            }
            if (index == 2) {
                shadowText(hint, small, hintLoc, batch);
            } else {
                batch.DrawString(small, hint, hintLoc, Color.GhostWhite);
            }
            if (index == 3) {
                shadowText("Back", xSmall, new Vector2(725.0F, 5.0F), batch);
            } else {
                batch.DrawString(xSmall, "Back", new Vector2(725.0F, 5.0F), Color.GhostWhite);
            }
            batch.Draw(cursor, new Vector2(curMouse.Position.X, curMouse.Position.Y), Color.White);
        }

        public override void update(GameTime time) {
            prevMouse = curMouse;
            curMouse = Mouse.GetState();
            prevKey = curKey;
            curKey = Keyboard.GetState();
            if (viewingHint) {
                Hint hint = hints[level];
                if (hint.isShowing()) {
                    hint.update(time);
                    return;
                } else {
                    hint.setShowing(true);
                    viewingHint = false;
                }
            }
            bool hover = false;
            if (!prevMouse.Position.Equals(curMouse.Position)) {
                for (int i = 0; i < textBounds.Length; i++) {
                    if (textBounds[i].Contains(curMouse.Position)) {
                        index = i + 1;
                        hover = true;
                        break;
                    }
                }
                if (!hover) {
                    index = -1;
                }
            }
            if (prevMouse.LeftButton == ButtonState.Pressed && curMouse.LeftButton == ButtonState.Released) {
                if (textBounds[1].Contains(curMouse.Position)) {
                    viewingHint = true;
                } else if (textBounds[2].Contains(curMouse.Position)) {
                    setActive(false);
                }
            }
            if (prevKey.IsKeyDown(Keys.Left) && curKey.IsKeyUp(Keys.Left)) {
                index = index == 1 ? 2 : 1;
            } else if (prevKey.IsKeyDown(Keys.Right) && curKey.IsKeyUp(Keys.Right)) {
                index = index == 2 ? 1 : 2;
            } else if (prevKey.IsKeyDown(Keys.Enter) && curKey.IsKeyUp(Keys.Enter)) {
                if (index == 2) {
                    viewingHint = true;
                }
            } else if (prevKey.IsKeyDown(Keys.M) && curKey.IsKeyUp(Keys.M)) {
                Console.WriteLine("CLOSE BY KEY");
                setActive(false);
            }
        }
    }
}
