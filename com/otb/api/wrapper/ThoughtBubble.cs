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
<<<<<<< HEAD
                    key = true; 
                    return thought = "H7&64"; 
                case 2:
                    key = false; 
                    return thought = "The only good psychic is\na marginalized, improverished, and\noppressed psychic";
                case 3:
                    key = false; 
                    return thought = "I'm an armed guard";
                case 4:
                    key = false; 
                    return thought = "They will not get in my mind.\nNo sir";
                default:
                    key = false; 
                    return thought = "I do not hear Steve's thoughts\nI DO NOT!";
=======
                    return thought = "Poopy"; 
                case 2:
                    return thought = "Ayylmao";
                case 3:
                    return thought = "I just want to pew pew :C";
                case 4:
                    return thought = "lemme tell you real quick about poop";
                default:
                    return thought = "I am a guard that's me yes";
>>>>>>> origin/master
            }
        }

        /// <summary>
        /// Handles drawing of the thought bubble
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            Vector2 fontLocation = getLocation();
<<<<<<< HEAD
            fontLocation.X = fontLocation.X + 50;
            fontLocation.Y = fontLocation.Y + 25; 
=======
            fontLocation.X = fontLocation.X + 75;
            fontLocation.Y = fontLocation.Y + 50; 
>>>>>>> origin/master
            if (revealed) {
                batch.Draw(getTexture(), getLocation(), Color.White);
                batch.DrawString(font, thought, fontLocation, Color.Black);

            }
        }
    }
}
