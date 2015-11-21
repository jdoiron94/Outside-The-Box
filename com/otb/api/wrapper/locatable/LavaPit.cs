using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace OutsideTheBox {

    public class LavaPit : Pit {

        private int damage;

        public LavaPit(Texture2D texture, Vector2 location, SoundEffectInstance effect, int width, int height) :
            base(texture, location, effect, width, height) {
            damage = 2;
        }

        public int getDamage() {
            return damage;
        }

        public override void update(InputManager inputManager) {
            inputManager.getPlayerManager().damagePlayer(damage);
        }
    }
}
