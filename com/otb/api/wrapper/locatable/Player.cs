using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles the player
    /// </summary>

    public class Player : Entity {

        public Player(Texture2D texture, Vector2 location, SoundEffectInstance effect, Direction direction, int maxHealth, int velocity) :
            base(texture, location, effect, direction, maxHealth, velocity) {
        }
    }
}
