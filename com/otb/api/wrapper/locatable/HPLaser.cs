using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class HPLaser : Laser {

        public HPLaser(Texture2D texture, Vector2 location, SoundEffectInstance effect, int height, int width, bool activated) :
            base(texture, location, effect, height, width, activated) {
        }

        public override void update(InputManager inputManager) {
            if (isActivated()) {
                inputManager.getPlayerManager().damagePlayer(10);
            }
        }
    }
}
