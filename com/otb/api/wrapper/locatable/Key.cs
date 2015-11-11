﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class Key : Collectible {

        private bool unlocked;

        public Key(Texture2D Texture, Vector2 Location) :
            base(Texture, Location, true) {
            unlocked = false;
        }

        /// <summary>
        /// Handles unlocking of the key
        /// </summary>
        /// <param name="value">The boolean to be set</param>
        public void setUnlocked(bool value) {
            unlocked = value;
        }

        /// <summary>
        /// Returns whether or not the key is unlocked
        /// </summary>
        /// <returns>Returns true if the key is unlocked; otherwise, false</returns>
        public bool isUnlocked() {
            return unlocked;
        }
    }
}