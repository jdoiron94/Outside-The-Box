using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace OutsideTheBox {

    /// <summary>
    /// Class which allows defining an npc's name and helpful hints
    /// </summary>

    public class NpcDefinition {

        private readonly string name;
        private readonly string[] hints;
        private readonly SpriteFont font;
        private readonly Vector2 origLoc;
        private readonly Texture2D thought;
        private readonly Random rand;

        private readonly int[] weights;

        private Vector2 location;

        private string text;
        private bool showing;
        private int ticks;

        public NpcDefinition(Texture2D thought, SpriteFont font, Vector2 location, string name, string[] hints, int[] weights) {
            this.thought = thought;
            this.font = font;
            this.location = location;
            this.name = name;
            this.hints = hints;
            this.weights = weights;
            this.origLoc = new Vector2(location.X, location.Y);
            this.rand = new Random();
        }

        /// <summary>
        /// Returns the npc's name
        /// </summary>
        /// <returns>Returns the npc's name</returns>
        public string getName() {
            return name;
        }

        /// <summary>
        /// Returns the hints from the definition
        /// </summary>
        /// <returns>Returns a string array of helpful hints</returns>
        public string[] getHints() {
            return hints;
        }

        /// <summary>
        /// Returns whether or not a thought bubble is showing
        /// </summary>
        /// <returns>Returns true if a thought bubble is showing; otherwise, false</returns>
        public bool isShowing() {
            return showing;
        }

        /// <summary>
        /// Resets the npc's thought
        /// </summary>
        public void resetThought() {
            location = origLoc;
            showing = false;
            ticks = 0;
        }

        /// <summary>
        /// Derives the x thought coordinate by x
        /// </summary>
        /// <param name="x">The x amount</param>
        public void deriveX(int x) {
            location.X += x;
        }

        /// <summary>
        /// Derives the y thought coordinate by y
        /// </summary>
        /// <param name="y">The y amount</param>
        public void deriveY(int y) {
            location.Y += y;
        }

        /// <summary>
        /// Sets the random hint
        /// </summary>
        public void update(bool generate) {
            if (generate) {
                //text = hints[rand.Next(0, hints.Length)];
                text = getRandom();
                showing = true;
            } else {
                if (ticks > 200) {
                    showing = false;
                    ticks = 0;
                    return;
                }
                ticks++;
            }
        }

        /// <summary>
        /// Returns a random hint from the hint array
        /// </summary>
        /// <returns>Returns a random hint from the hint array</returns>
        private string getRandom() {
            int totalWeight = 0;
            foreach (int w in weights) {
                totalWeight += w;
            }
            int random = rand.Next(0, totalWeight);
            int count = 0;
            for (int i = 0; i < weights.Length; i++) {
                count += weights[i];
                if (count >= random) {
                    return hints[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Draws the npc's thought
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void drawThought(SpriteBatch batch) {
            if (showing) {
                batch.Draw(thought, location, Color.White);
                Vector2 size = font.MeasureString(text);
                Vector2 loc = new Vector2(location.X + ((thought.Width - size.X) / 2.0F), location.Y + ((thought.Height - size.Y) / 2.0F));
                batch.DrawString(font, text, loc, Color.Black);
            }
        }
    }
}
