using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Projectile {

        /*
         * Class which holds all information necessary to represent a projectile.
         */

        private Texture2D texture;
        private Vector2 position;
        private Vector2 origin;
        private Direction direction;

        private int velocity;
        private int cooldown;
        private float rotationSpeed;
        private float rotation;
        private bool active;
        
        public Projectile(Texture2D texture, Vector2 position, int velocity, int cooldown, float rotationSpeed) {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.cooldown = cooldown;
            this.rotationSpeed = rotationSpeed;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            direction = Direction.NONE;
            rotation = 0f;
            active = true;
        }

        public Projectile(Texture2D texture, Vector2 position, int velocity, int cooldown) :
            this(texture, position, velocity, cooldown, 0f) {
        }

        /*
         * Returns the projectile's texture
         */
        public Texture2D getTexture() {
            return texture;
        }

        /*
         * Returns the projectile's current position
         */
        public Vector2 getPosition() {
            return position;
        }

        /*
         * Returns the projectile's origin
         */
        public Vector2 getOrigin() {
            return origin;
        }

        /*
         * Returns the projectile's velocity
         */
        public int getVelocity() {
            return velocity;
        }

        /*
         * Returns the projectile's cooldown time
         */
        public int getCooldown() {
            return cooldown;
        }

        /*
         * Returns the projectile's rotational speed
         */
        public float getRotationSpeed() {
            return rotationSpeed;
        }

        /*
         * Returns the projectile's current rotation
         */
        public float getRotation() {
            return rotation;
        }

        /*
         * Returns true if the projectile is active; otherwise, false
         */
        public bool isActive() {
            return active;
        }

        /*
         * Derives the projectile's location in terms of its x coordinate
         */
        public void deriveX(int x) {
            position.X += x;
        }

        /*
         * Derives the projectile's location in terms of its y coordinate
         */
        public void deriveY(int y) {
            position.Y += y;
        }

        /*
         * Returns true if the projectile is currently on the screen; otherwise, false
         */
        public bool isOnScreen(Entity entity) {
            return position.X >= -texture.Width && position.X <= entity.getWindowBounds().Width && position.Y >= -texture.Height && position.Y <= entity.getWindowBounds().Height;
        }

        /*
         * Sets the projectile's direction and updates its location, rotation, and active status
         */
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
            rotation = rotation + rotationSpeed;
            active = isOnScreen(entity);
        }

        /*
         * Draws the projectile, given a SpriteBatch
         */
        public void draw(SpriteBatch batch) {
            batch.Draw(texture, Vector2.Add(position, origin), null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
