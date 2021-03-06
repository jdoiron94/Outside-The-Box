﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles door object functionality
    /// </summary>

    public class Door : GameObject {

        private bool leads;
        private bool unlocked;

        private readonly bool origUnlocked;

        public Door(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, bool liftable, bool leads, int width, int height, bool unlocked) :
            base(texture, projectile, location, direction, liftable, width, height) {
            this.leads = leads;
            this.unlocked = unlocked;
            this.origUnlocked = unlocked;
        }

        /// <summary>
        /// Resets the door's unlocked status
        /// </summary>
        public void reset() {
            unlocked = origUnlocked;
        }

        /// <summary>
        /// Sets the door's next bool
        /// </summary>
        /// <param name="leads">Bool telling us if door leads to a new level</param>
        public void setLeads(bool leads) {
            this.leads = leads;
        }

        /// <summary>
        /// Returns whether or not there is a next level
        /// </summary>
        /// <returns>Returns true if the door leads to a new level; otherwise, false</returns>
        public bool continues() {
            return leads;
        }

        /// <summary>
        /// Returns whether or not the door is unlocked
        /// </summary>
        /// <returns>Returns true if the door is unlocked; otherwise, false</returns>
        public bool isUnlocked() {
            return unlocked;
        }

        /// <summary>
        /// Handles unlocking of the door
        /// </summary>
        /// <param name="unlocked">Bool to determine the door's unlocked property</param>
        public void setUnlocked(bool unlocked) {
            this.unlocked = unlocked;
        }
    }
}
