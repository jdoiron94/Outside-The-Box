using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class Laser : Pit {

        private bool Activated;
        private bool defaultValue; 

        public Laser(Texture2D texture, Vector2 Location, SoundEffectInstance effect, int height, int width, bool Activated, bool? defaultValue = null) :
            base(texture, Location, effect, width, height) {
            this.Activated = Activated;
            if (defaultValue == null)
                defaultValue = true;
            else
                this.defaultValue = (bool) defaultValue;
        }

        public bool isActivated() {
            return Activated;
        }

        public void setActivated(bool value) {
            if (!defaultValue)
                Activated = value;
            else
                Activated = !value; 
        }

        public override void draw(SpriteBatch batch) {
            if (Activated) {
                batch.Draw(getTexture(), getSize(), Color.White);
            }
        }
    }
}
