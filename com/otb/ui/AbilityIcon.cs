using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles the ability icons in the powerbar
    /// </summary>

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
