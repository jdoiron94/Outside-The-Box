using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OutsideTheBox {

    public class PowerBar {

        private readonly AbilityIcon[] icons;

        public PowerBar(AbilityIcon[] icons) {
            this.icons = icons;
        }

        /// <summary>
        /// Returns the ability icons belonging to the power bar
        /// </summary>
        /// <returns>Returns the ability icons belonging to the power bar</returns>
        public AbilityIcon[] getIcons() {
            return icons;
        }

        /// <summary>
        /// Draws the display bar
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            foreach (AbilityIcon ai in icons) {
                ai.draw(batch);
            }
        }
    }
}
