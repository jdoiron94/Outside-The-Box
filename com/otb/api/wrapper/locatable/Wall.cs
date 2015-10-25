using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents a wall
    /// </summary>

    public class Wall : GameObject {

        public Wall(Texture2D texture, Projectile projectile, Vector2 location, Direction direction, bool liftable, bool next, int width, int height) :
            base(texture, projectile, location, direction, false, width, height) {
        }
    }
}
