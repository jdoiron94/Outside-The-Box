using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class Pit : GameObject {

        private Rectangle size;
        private Rectangle bounds;

        private readonly SoundEffectInstance effect;

        public Pit(Texture2D texture, Vector2 location, SoundEffectInstance effect, int width, int height) :
            base(texture, location) {
            this.effect = effect;
            size = new Rectangle((int) getLocation().X, (int) getLocation().Y, width, height);
            bounds = new Rectangle((int) getLocation().X, (int) getLocation().Y, width, height);
            setBounds(bounds);
            setDestinationBounds(bounds);
        }

        public SoundEffectInstance getEffect() {
            return effect;
        }

        public Rectangle getSize() {
            return size;
        }

        public Rectangle getPitBounds() {
            return bounds;
        }

        public void setPitBounds(bool orientation) {
            if (orientation) {
                bounds = new Rectangle((int) getLocation().X, (int) getLocation().Y, 1, bounds.Y);
            } else {
                bounds = new Rectangle((int) getLocation().X, (int) getLocation().Y, bounds.X, 1);
            }
        }

        public void playEffect() {
            if (effect != null && effect.State != SoundState.Playing) {
                effect.Play();
            }
        }

        public virtual void update(InputManager inputManager) {

        }

        public virtual void draw(SpriteBatch batch) {
            batch.Draw(getTexture(), size, Color.White);
        }
    }
}
