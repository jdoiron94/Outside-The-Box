using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class ManaLaser : Laser {

        public ManaLaser(Texture2D texture, Vector2 location, SoundEffectInstance effect, int height, int width, bool Activated) :
            base(texture, location, effect, height, width, Activated) {

        }

        public override void update(InputManager inputManager) {
            if (isActivated()) {
                inputManager.getPlayerManager().depleteMana(50);
            }
        }
    }
}
