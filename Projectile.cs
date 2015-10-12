using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Projectile {

        /*
         * Class which holds all information necessary to represent a projectile.
         */

        private readonly Entity owner;
        private readonly Texture2D texture;
        private readonly Vector2 origin;

        private Vector2 position;
        private Direction direction;

        private readonly int velocity;
        private readonly int cooldown;
        private readonly float rotationSpeed;

        private float rotation;
        private bool active;
        
        public Projectile(Entity owner, Texture2D texture, int velocity, int cooldown, float rotationSpeed) {
            this.owner = owner;
            this.texture = texture;
            this.velocity = velocity;
            this.cooldown = cooldown;
            this.rotationSpeed = rotationSpeed;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            position = new Vector2(owner.getLocation().X, owner.getLocation().Y);
            direction = Direction.NONE;
            rotation = 0f;
            active = true;
        }

        public Projectile(Entity owner, Texture2D texture, int velocity, int cooldown) :
            this(owner, texture, velocity, cooldown, 0f) {
        }

        /*
         * Returns the owner of the projectile
         */
        public Entity getOwner() {
            return owner;
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
         * Sets the projectile's active status according to the parameter
         */
        public void setActive(bool active) {
            this.active = active;
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
        public bool isOnScreen(Game1 game) {
            return position.X >= -texture.Width && position.X <= game.getWidth() && position.Y >= -texture.Height && position.Y <= game.getHeight();
        }

        /*
         * Sets the projectile's direction and updates its location, rotation, and active status
         */
        public void update(Game1 game, Entity entity) {
            if (direction == Direction.NONE) {
                direction = entity.getDirection();
            }
            if (direction == Direction.NORTH) {
                deriveY(-velocity);
            } else if (direction == Direction.SOUTH) {
                deriveY(velocity);
            } else if (direction == Direction.WEST) {
                deriveX(-velocity);
            } else if (direction == Direction.EAST) {
                deriveX(velocity);
            }
            rotation = rotation + rotationSpeed;
            active = isOnScreen(game);
        }

        /*
         * Draws the projectile, given a SpriteBatch
         */
        public void draw(SpriteBatch batch) {
            batch.Draw(texture, Vector2.Add(position, origin), null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
