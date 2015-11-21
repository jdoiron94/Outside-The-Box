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

        public Texture2D getIcon() {
            return icon;
        }

        public Vector2 getLocation() {
            return location;
        }

        public SpriteFont getFont() {
            return font;
        }

        public string getKey() {
            return key;
        }

        public void draw(SpriteBatch batch) {
            batch.Draw(icon, location, Color.White);
            Vector2 size = font.MeasureString(key);
            Vector2 loc = new Vector2(location.X + ((icon.Width - size.X) / 2.0F), location.Y + ((icon.Height - size.Y) / 2.0F));
            batch.DrawString(font, key, loc, Color.MonoGameOrange);
        }
    }
}
