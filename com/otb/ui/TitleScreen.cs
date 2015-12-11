using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles the title screen's interactions
    /// </summary>

    public class TitleScreen : Screen {

        private readonly Texture2D background;
        private readonly Texture2D controls;
        private readonly Texture2D about;
        private readonly Texture2D cursor;
        private readonly SpriteFont font;
        private readonly SoundEffectInstance effect;

        private readonly Rectangle[] backgroundBounds;
        private readonly Rectangle controlBounds;

        private Texture2D texture;

        private int index;
        private MouseState prevMouse;
        private MouseState curMouse;
        private KeyboardState prevKey;
        private KeyboardState curKey;

        private string[] options = { "Start", "Controls", "About" };
        private Vector2[] locations = { new Vector2(650.0F, 400.0F), new Vector2(650.0F, 430.0F), new Vector2(650.0F, 460.0F) };

        public TitleScreen(Texture2D background, Texture2D controls, Texture2D about, Texture2D cursor, SpriteFont font, SoundEffectInstance effect, string name, bool active) :
            base(name, active) {
            this.background = background;
            this.controls = controls;
            this.about = about;
            this.cursor = cursor;
            this.font = font;
            this.effect = effect;
            this.texture = background;
            this.index = 1;
            this.backgroundBounds = new Rectangle[] { new Rectangle(643, 397, 55, 25), new Rectangle(643, 429, 81, 25), new Rectangle(643, 459, 64, 25) };
            this.controlBounds = new Rectangle(717, 474, 52, 25);
        }

        /// <summary>
        /// Plays the title screen's sound effect
        /// </summary>
        public void playEffect() {
            if (effect != null && effect.State != SoundState.Playing) {
                effect.Play();
            }
        }

        /// <summary>
        /// Adds a drop shadow to the specified text
        /// </summary>
        /// <param name="text">The text to shadow</param>
        /// <param name="location">The location to draw the text</param>
        /// <param name="batch">The SpriteBatch to draw with</param>
        private void shadowText(string text, Vector2 location, SpriteBatch batch) {
            batch.DrawString(font, text, new Vector2(location.X + 2.0F, location.Y + 2.0F), Color.DarkSlateGray);
            batch.DrawString(font, text, location, Color.White);
        }

        /// <summary>
        /// Handles drawing of the title screen
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public override void draw(SpriteBatch batch) {
            batch.Draw(texture, Vector2.Zero, Color.White);
            if (texture == background) {
                for (int i = 0; i < 3; i++) {
                    if (i == index - 1) {
                        shadowText(options[i], locations[i], batch);
                        continue;
                    }
                    batch.DrawString(font, options[i], locations[i], Color.GhostWhite);
                }
            } else {
                if (index == 0) {
                    shadowText("Back", new Vector2(725.0F, 475.0F), batch);
                } else {
                    batch.DrawString(font, "Back", new Vector2(725.0F, 475.0F), Color.GhostWhite);
                }
            }
            batch.Draw(cursor, new Vector2(curMouse.Position.X, curMouse.Position.Y), Color.White);
        }

        /// <summary>
        /// Handles updating of the title screen
        /// </summary>
        /// <param name="time">The GameTime to respect</param>
        public override void update(GameTime time) {
            prevMouse = curMouse;
            curMouse = Mouse.GetState();
            prevKey = curKey;
            curKey = Keyboard.GetState();
            if (texture == background) {
                if (!prevMouse.Position.Equals(curMouse.Position)) {
                    for (int i = 0; i < backgroundBounds.Length; i++) {
                        if (backgroundBounds[i].Contains(prevMouse.Position)) {
                            index = i + 1;
                            break;
                        }
                    }
                }
                if (prevKey.IsKeyDown(Keys.Up) && curKey.IsKeyUp(Keys.Up)) {
                    index = index == 1 ? 3 : index - 1;
                } else if (prevKey.IsKeyDown(Keys.Down) && curKey.IsKeyUp(Keys.Down)) {
                    index = index == 3 ? 1 : index + 1;
                } else if (curKey.IsKeyDown(Keys.Enter)) {
                    if (index == 1) {
                        setActive(false);
                        playEffect();
                    } else if (index == 2) {
                        texture = controls;
                        playEffect();
                    } else {
                        texture = about;
                        playEffect();
                    }
                }
                if (prevMouse.LeftButton == ButtonState.Pressed && curMouse.LeftButton == ButtonState.Released) {
                    for (int i = 0; i < backgroundBounds.Length; i++) {
                        if (backgroundBounds[i].Contains(prevMouse.Position)) {
                            if (i == 0) {
                                setActive(false);
                                playEffect();
                            } else if (i == 1) {
                                texture = controls;
                                playEffect();
                            } else {
                                texture = about;
                                playEffect();
                            }
                            break;
                        }
                    }
                }
            } else if (texture == controls) {
                if (!prevMouse.Position.Equals(curMouse.Position)) {
                    index = controlBounds.Contains(curMouse.Position) ? 0 : -1;
                }
                if (prevMouse.LeftButton == ButtonState.Pressed && curMouse.LeftButton == ButtonState.Released) {
                    if (controlBounds.Contains(curMouse.Position)) {
                        texture = background;
                        playEffect();
                    }
                }
            } else {
                if (!prevMouse.Position.Equals(curMouse.Position)) {
                    index = controlBounds.Contains(curMouse.Position) ? 0 : -1;
                }
                if (prevMouse.LeftButton == ButtonState.Pressed && curMouse.LeftButton == ButtonState.Released) {
                    if (controlBounds.Contains(curMouse.Position)) {
                        texture = background;
                        playEffect();
                    }
                }
            }
        }
    }
}
