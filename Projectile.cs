using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Projectile {

        private Texture2D texture;
        private Vector2 position;
        private Direction direction;

        private int velocity;
        private int cooldown;
        private bool active;
        
        // TODO: rotation support
        public Projectile(Texture2D texture, Vector2 position, int velocity, int cooldown) {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.cooldown = cooldown;
            direction = Direction.NONE;
            active = true;
        }

        public Texture2D getTexture() {
            return texture;
        }

        public Vector2 getPosition() {
            return position;
        }

        public int getVelocity() {
            return velocity;
        }

        public int getCooldown() {
            return cooldown;
        }

        public bool isActive() {
            return active;
        }

        public void deriveX(int x) {
            position.X += x;
        }

        public void deriveY(int y) {
            position.Y += y;
        }

        public bool isOnScreen(Entity entity) {
            return position.X >= 0 && position.X <= entity.getWindowBounds().Width && position.Y >= 0 && position.Y <= entity.getWindowBounds().Height;
        }

        public void update(Entity entity) {
            if (direction == Direction.NONE) {
                direction = entity.getDirection();
            }
            if (direction == Direction.NORTH) {
                deriveY(-1 * velocity);
            } else if (direction == Direction.SOUTH) {
                deriveY(velocity);
            } else if (direction == Direction.WEST) {
                deriveX(-1 * velocity);
            } else if (direction == Direction.EAST) {
                deriveX(velocity);
            }
            active = isOnScreen(entity);
        }

        public void draw(SpriteBatch batch) {
            batch.Draw(texture, position, Color.White);
        }
    }
}
