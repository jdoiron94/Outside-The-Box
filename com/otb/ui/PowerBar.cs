using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles the power bar
    /// </summary>

    public class PowerBar {

        private readonly AbilityIcon[] icons;

        public PowerBar(AbilityIcon[] icons) {
            this.icons = icons;
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
