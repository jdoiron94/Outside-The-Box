using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace OutsideTheBox {

    public class PauseMenu : Screen {

        private Texture2D texture;
        private readonly Texture2D gradient;
        private readonly Texture2D controls;
        private readonly Texture2D cursor;
        private readonly SpriteFont small;
        private readonly SpriteFont large;

        private readonly string pause;
        private readonly string exp;
        private readonly string hint;

        private readonly Rectangle controlBounds;
        private readonly Rectangle[] textBounds;

        private List<Hint> hints;
        private MouseState prevMouse;
        private MouseState curMouse;
        private KeyboardState prevKey;
        private KeyboardState curKey;

        private bool viewingHint;
        private int index;
        private int controlIndex;
        private int level;
        private int experience;

        public PauseMenu(Texture2D gradient, Texture2D controls, Texture2D cursor, SpriteFont small, SpriteFont large, string name, bool active) :
            base(name, active) {
            this.texture = gradient;
            this.gradient = gradient;
            this.controls = controls;
            this.cursor = cursor;
            this.small = small;
            this.large = large;
            this.pause = "PAUSE";
            this.exp = "EXP: ";
            this.hint = "HINT";
            this.hints = new List<Hint>(5);
            this.viewingHint = false;
            this.controlBounds = new Rectangle(705, 475, 95, 45);
            this.textBounds = new Rectangle[] { new Rectangle(0, 460, 120, 60), new Rectangle(700, 465, 100, 55),
                new Rectangle(710, 0, 90, 50), new Rectangle(0, 0, 155, 65) };
            this.index = 1;
            this.level = 0;
            this.experience = 0;
        }

        /// <summary>
        /// Returns the player's experience within the pause menu
        /// </summary>
        /// <returns>Returns the player's exp</returns>
        public int getExperience() {
            return experience;
        }

        /// <summary>
        /// Adds a hint to the pause menu
        /// </summary>
        /// <param name="hint">The hint to add</param>
        public void addHint(Hint hint) {
            hints.Add(hint);
        }

        /// <summary>
        /// Sets the level to later show the correct hint
        /// </summary>
        /// <param name="level">The level to set</param>
        public void setLevel(int level) {
            this.level = level;
        }

        /// <summary>
        /// Sets the player's experience within the pause menu
        /// </summary>
        /// <param name="experience">The experience to set</param>
        public void setExperience(int experience) {
            this.experience = experience;
        }

        /// <summary>
        /// Adds a drop shadow to the specified text
        /// </summary>
        /// <param name="text">The text to shadow</param>
        /// <param name="font">The SpriteFont to draw with</param>
        /// <param name="location">The location to draw the text</param>
        /// <param name="batch">The SpriteBatch to draw with</param>
        private void shadowText(string text, SpriteFont font, Vector2 location, SpriteBatch batch) {
            batch.DrawString(font, text, new Vector2(location.X + 2.0F, location.Y + 2.0F), Color.DarkSlateGray);
            batch.DrawString(font, text, location, Color.White);
        }

        /// <summary>
        /// Handles drawing of the pause menu
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public override void draw(SpriteBatch batch) {
            batch.Draw(gradient, Vector2.Zero, Color.White);
            if (texture == controls) {
                batch.Draw(texture, Vector2.Zero, Color.White);
                if (controlIndex == 1) {
                    shadowText("Back", small, new Vector2(715.0F, 480.0F), batch);
                } else {
                    batch.DrawString(small, "Back", new Vector2(715.0F, 480.0F), Color.GhostWhite);
                }
                batch.Draw(cursor, new Vector2(curMouse.Position.X, curMouse.Position.Y), Color.White);
                return;
            }
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
                shadowText("Back", small, new Vector2(725.0F, 5.0F), batch);
            } else {
                batch.DrawString(small, "Back", new Vector2(725.0F, 5.0F), Color.GhostWhite);
            }
            if (index == 4) {
                shadowText("Controls", small, new Vector2(15.0F, 15.0F), batch);
            } else {
                batch.DrawString(small, "Controls", new Vector2(15.0F, 15.0F), Color.GhostWhite);
            }
            batch.Draw(cursor, new Vector2(curMouse.Position.X, curMouse.Position.Y), Color.White);
        }

        /// <summary>
        /// Handles updating the pause menu
        /// </summary>
        /// <param name="time">The GameTime to respect</param>
        public override void update(GameTime time) {
            prevMouse = curMouse;
            curMouse = Mouse.GetState();
            prevKey = curKey;
            curKey = Keyboard.GetState();
            if (texture == controls) {
                if (prevKey.IsKeyDown(Keys.M) && curKey.IsKeyUp(Keys.M)
                || (prevKey.IsKeyDown(Keys.Escape) && curKey.IsKeyUp(Keys.Escape))) {
                    controlIndex = -1;
                    texture = gradient;
                    return;
                }
                if (!prevMouse.Position.Equals(curMouse.Position)) {
                    if (controlBounds.Contains(curMouse.Position)) {
                        controlIndex = 1;
                    } else {
                        controlIndex = -1;
                    }
                }
                if (prevMouse.LeftButton == ButtonState.Pressed && curMouse.LeftButton == ButtonState.Released) {
                    if (controlIndex == 1) {
                        controlIndex = -1;
                        texture = gradient;
                    }
                }
                return;
            } else if (viewingHint) {
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
                } else if (textBounds[3].Contains(curMouse.Position)) {
                    texture = controls;
                }
            }
            if (prevKey.IsKeyDown(Keys.Left) && curKey.IsKeyUp(Keys.Left)) {
                index = index == 1 ? 2 : 1;
            } else if (prevKey.IsKeyDown(Keys.Right) && curKey.IsKeyUp(Keys.Right)) {
                index = index == 2 ? 1 : 2;
            } else if (prevKey.IsKeyDown(Keys.Enter) && curKey.IsKeyUp(Keys.Enter)) {
                if (index == 2) {
                    viewingHint = true;
                } else if (index == 4) {
                    texture = controls;
                }
            } else if (prevKey.IsKeyDown(Keys.M) && curKey.IsKeyUp(Keys.M)
                || (prevKey.IsKeyDown(Keys.Escape) && curKey.IsKeyUp(Keys.Escape))) {
                setActive(false);
            }
        }
    }
}
