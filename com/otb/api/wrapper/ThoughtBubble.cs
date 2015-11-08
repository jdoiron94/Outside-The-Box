using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents thought bubbles from npcs
    /// </summary>

    public class ThoughtBubble : GameObject {

        private readonly Npc npc;
        private SpriteFont font;
        private bool revealed;
        private bool key;
        private string thought;


        public ThoughtBubble(Texture2D texture, SpriteFont font, Vector2 Location, Npc npc, bool revealed, bool key) :
            base(texture, Location) {
            this.npc = npc;
            setThoughtLocation(npc.getLocation());
            revealed = false;
            key = false;
            this.font = font;
            setRand();
        }

        /// <summary>
        /// Sets the thought bubble's location
        /// </summary>
        /// <param name="location">The location to set</param>
        public void setThoughtLocation(Vector2 location) {
            setLocation(new Vector2(location.X + 40F, location.Y - 100F));
        }

        /// <summary>
        /// Returns whether or not the thought bubble has been revealed
        /// </summary>
        /// <returns>Returns true if the thought bubble has been revealed; otherwise, false</returns>
        public bool isRevealed() {
            return revealed;
        }

        /// <summary>
        /// Updates the thought bubble's location relative to the owner's location
        /// </summary>
        public void updateLocation() {
            setThoughtLocation(npc.getLocation());
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool isKey() {
            return key;
        }

        /// <summary>
        /// Updates the revealed status
        /// </summary>
        /// <param name="r">The reveal status to set</param>
        public void reveal(bool r) {
            revealed = r;
        }

        /// <summary>
        /// Sets the thought bubble's key status
        /// </summary>
        public void setKey() {
            key = true;
        }

        /// <summary>
        /// Sets the random number for the thought
        /// </summary>
        public void setRand()
        {
            Random rand = new Random();
            setThought(rand.Next(4));
        }

        public void updateThought()
        {
            setRand(); 
        }

        /// <summary>
        /// Sets the thought bubble's speak
        /// </summary>
        public string setThought(int caseSwitch)
        {
            switch (caseSwitch)
            {
                case 1:
                    return thought = "Poopy"; 
                case 2:
                    return thought = "Ayylmao";
                case 3:
                    return thought = "I just want to pew pew :C";
                case 4:
                    return thought = "lemme tell you real quick about poop";
                default:
                    return thought = "I am a guard that's me yes";
            }
        }

        /// <summary>
        /// Handles drawing of the thought bubble
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            Vector2 fontLocation = getLocation();
            fontLocation.X = fontLocation.X + 75;
            fontLocation.Y = fontLocation.Y + 50; 
            if (revealed) {
                batch.Draw(getTexture(), getLocation(), Color.White);
                batch.DrawString(font, thought, fontLocation, Color.Black);

            }
        }
    }
}
