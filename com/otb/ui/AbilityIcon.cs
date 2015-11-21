using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class AbilityIcon {

        private readonly Texture2D icon;
        private readonly Vector2 location;
        private readonly SpriteFont font;
        private readonly string key;

        public AbilityIcon(Texture2D icon, Vector2 location, SpriteFont font, string key) {
            this.icon = icon;
            this.location = location;
            this.font = font;
            this.key = key;
        }

        /// <summary>
        /// Returns the icon for the ability
        /// </summary>
        /// <returns>Returns the icon for the ability</returns>
        public Texture2D getIcon() {
            return icon;
        }

        /// <summary>
        /// Returns the location of the icon
        /// </summary>
        /// <returns>Returns the location of the icon</returns>
        public Vector2 getLocation() {
            return location;
        }

        /// <summary>
        /// Returns the font used for the icon's text
        /// </summary>
        /// <returns>Returns the font used for the icon's text</returns>
        public SpriteFont getFont() {
            return font;
        }

        /// <summary>
        /// Returns the key used to activate the ability
        /// </summary>
        /// <returns>Returns the key used to activate the ability</returns>
        public string getKey() {
            return key;
        }

        /// <summary>
        /// Handles drawing of the ability icon
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(icon, location, Color.White);
            Vector2 size = font.MeasureString(key);
            Vector2 loc = new Vector2(location.X + ((icon.Width - size.X) / 2.0F), location.Y + ((icon.Height - size.Y) / 2.0F));
            batch.DrawString(font, key, loc, Color.MonoGameOrange);
        }
    }
}
