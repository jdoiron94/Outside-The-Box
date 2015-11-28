using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class PlayerLimitationField : Pit {

        private readonly Texture2D[] frames;

        private int index;
        private int timer;
        private int current;
        private bool forward;

        public PlayerLimitationField(Texture2D[] frames, Vector2 location, SoundEffectInstance effect, int width, int height) :
            base(frames[0], location, effect, width, height) {
            this.frames = frames;
            this.index = 0;
            this.timer = 5;
            this.current = 0;
            this.forward = true;
        }

        public void updateFrame() {
            if (current >= timer) {
                if (forward) {
                    index++;
                    if (index == frames.Length - 1) {
                        forward = false;
                    }
                } else {
                    index--;
                    if (index == 0) {
                        forward = true;
                    }
                }
                current = 0;
            } else {
                current++;
            }
        }

        public override void update(InputManager inputManager) {
            inputManager.getPlayerManager().setHealthLimit(false);
            inputManager.getPlayerManager().setManaLimit(false);
        }

        public override void draw(SpriteBatch batch) {
            batch.Draw(frames[index], getSize(), Color.White);
        }
    }
}
