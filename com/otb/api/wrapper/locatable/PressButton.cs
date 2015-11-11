using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OutsideTheBox.com.otb.api.wrapper;

namespace OutsideTheBox {

    public class PressButton : GameObject {

        private Texture2D textureOn;
        private Texture2D textureOff;
        private Texture2D textureDeactivated;

        private bool deactivated;
        private bool pushed;

        public PressButton(Texture2D[] texture, Vector2 location, bool deactivated, bool pushed) :
            base(texture[0], location) {
            textureOn = texture[0];
            textureOff = texture[1];
            textureDeactivated = texture[2];

            this.deactivated = deactivated;
            this.pushed = pushed;
        }

        public void setPushed(bool pushed) {
            this.pushed = pushed;
        }

        /// <summary>
        /// Returns if the game object is pushed
        /// </summary>
        /// <returns>Returns true if the game object is pushed; otherwise, false</returns>
        public bool isPushed() {
            return pushed;
        }

        public void setDeactivated(bool value) {
            deactivated = value;
        }

        /// <summary>
        /// Returns if the game object can be pushable
        /// </summary>
        /// <returns>Returns true if the game object is pushable; otherwise, false</returns>
        public bool isDeactivated() {
            return deactivated;
        }

        public virtual void update()
        {

        }

        /// <summary>
        /// Draws Press Button
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            if (deactivated) {
                batch.Draw(textureDeactivated, getLocation(), Color.White);
            } else {
                batch.Draw(pushed ? textureOn : textureOff, getLocation(), Color.White);
            }

        }
    }
}
