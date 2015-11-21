using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class PlayerLimitationField : Pit {

        public PlayerLimitationField(Texture2D texture, Vector2 location, SoundEffectInstance effect, int width, int height) :
            base(texture, location, effect, width, height) {
        }

        public override void update(InputManager inputManager) {
            inputManager.getPlayerManager().setHealthLimit(false);
            inputManager.getPlayerManager().setManaLimit(false);
        }
    }
}
