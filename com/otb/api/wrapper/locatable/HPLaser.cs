using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles hitpoint lasers
    /// </summary>

    public class HPLaser : Laser {

        private int damage;

        public HPLaser(Texture2D texture, Vector2 location, SoundEffectInstance effect, int height, int width, int damage) :
            base(texture, location, effect, height, width) {
            this.damage = damage;
        }

        public HPLaser(Texture2D texture, Vector2 location, SoundEffectInstance effect, int height, int width, int damage, bool activated) :
            base(texture, location, effect, height, width, activated) {
            this.damage = damage;
        }

        public HPLaser(Texture2D texture, Vector2 location, SoundEffectInstance effect, int height, int width) :
            base(texture, location, effect, height, width) {
            damage = 10;
        }

        public HPLaser(Texture2D texture, Vector2 location, SoundEffectInstance effect, int height, int width, bool activated) :
            base(texture, location, effect, height, width, activated) {
            damage = 10;
        }

        /// <summary>
        /// Handles updating of the laser
        /// </summary>
        /// <param name="inputManager">The InputManager</param>
        public override void update(InputManager inputManager) {
            if (isActivated()) {
                inputManager.getPlayerManager().damagePlayer(damage);
            }
        }
    }
}
