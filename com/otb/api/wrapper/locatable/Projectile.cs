using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents a projectile
    /// </summary>

    public class Projectile {

        private readonly Entity owner;
        private readonly Texture2D texture;
        private readonly Vector2 origin;

        private Vector2 location;
        private Direction direction;
        private Rectangle bounds;

        private readonly int velocity;
        private readonly int cooldown;
        private readonly float rotationSpeed;

        private float rotation;
        private bool active;

        private SoundEffect sound;

        public Projectile(Entity owner, Texture2D texture, int velocity, int cooldown, float rotationSpeed, SoundEffect sound) {
            this.owner = owner;
            this.texture = texture;
            this.velocity = velocity;
            this.cooldown = cooldown;
            this.rotationSpeed = rotationSpeed;
            this.sound = sound;
            origin = new Vector2(texture.Width / 2F, texture.Height / 2F);
            location = new Vector2(owner.getLocation().X + (owner.getTexture().Width - texture.Width) / 2F, owner.getLocation().Y + (owner.getTexture().Height - texture.Height) / 2F);
            direction = Direction.None;
            bounds = new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height);
            rotation = 0f;
            active = true;
        }

        public Projectile(Entity owner, Texture2D texture, int velocity, int cooldown, SoundEffect sound) :
            this(owner, texture, velocity, cooldown, 0f, sound) {
        }

        /// <summary>
        /// Returns the owner of the projectile
        /// </summary>
        /// <returns>Returns the owner of the projectile</returns>
        public Entity getOwner() {
            return owner;
        }

        /// <summary>
        /// Returns the projectile's texture
        /// </summary>
        /// <returns>Returns the projectile's texture</returns>
        public Texture2D getTexture() {
            return texture;
        }

        /// <summary>
        /// Returns the projectile's position
        /// </summary>
        /// <returns>Returns the projectile's position</returns>
        public Vector2 getLocation() {
            return location;
        }

        /// <summary>
        /// Returns the bounds for the projectile
        /// </summary>
        /// <returns>Returns the bounds for the projectile</returns>
        public Rectangle getBounds() {
            return bounds;
        }

        /// <summary>
        /// Returns the origin of the projectile
        /// </summary>
        /// <returns>Returns the projectile's origin</returns>
        public Vector2 getOrigin() {
            return origin;
        }

        /// <summary>
        /// Returns the projectile's velocity
        /// </summary>
        /// <returns>Returns the projectile's velocity</returns>
        public int getVelocity() {
            return velocity;
        }

        /// <summary>
        /// Returns the projectile's cooldown time
        /// </summary>
        /// <returns>Returns the projectile's cooldown time</returns>
        public int getCooldown() {
            return cooldown;
        }

        /// <summary>
        /// Returns the projectile's rotation speed
        /// </summary>
        /// <returns>Returns the projectile's rotation speed</returns>
        public float getRotationSpeed() {
            return rotationSpeed;
        }

        /// <summary>
        /// Returns the projectile's current rotation
        /// </summary>
        /// <returns>Returns the projectile's current rotation</returns>
        public float getRotation() {
            return rotation;
        }


        /// <summary>
        /// Returns the projectile's sound
        /// </summary>
        /// <returns>Returns the projectile's sound effect</returns>
        public SoundEffect getSound()
        {
            return sound;
        }

        /// <summary>
        /// Returns the projectile's active status
        /// </summary>
        /// <returns>Returns true if the projectile is currently ative; otherwise, false</returns>
        public bool isActive() {
            return active;
        }

        /// <summary>
        /// Sets the projectile's active status to the specified boolean
        /// </summary>
        /// <param name="active">The active status to set</param>
        public void setActive(bool active) {
            this.active = active;
        }

        /// <summary>
        /// Derives the projectile's x coordinate by the specified x amount
        /// </summary>
        /// <param name="x">The x amount to be derived by</param>
        public void deriveX(int x) {
            location.X += x;
            bounds.X += x;
        }

        /// <summary>
        /// Derives the projectile's y coordinate by the specified y amount
        /// </summary>
        /// <param name="y">The y amount to by derived by</param>
        public void deriveY(int y) {
            location.Y += y;
            bounds.Y += y;
        }
        
    /// <summary>
    /// Returns whether or not the projectile is currently on the screen
    /// </summary>
    /// <param name="game">The game instance to check viewport bounds from</param>
    /// <returns>Returns true if the projectile is currently on screen; otherwise, false</returns>
    public bool isOnScreen(Game1 game) {
            return location.X >= -texture.Width && location.X <= game.getWidth() && location.Y >= -texture.Height && location.Y <= game.getHeight();
        }

        /// <summary>
        /// Rotates the projectile's sprite, depending on the owning entity's direction
        /// </summary>
        private void rotate() {
            if (direction == Direction.North) {
                rotation = MathHelper.ToRadians(-90F);
            } else if (direction == Direction.South) {
                rotation = MathHelper.ToRadians(90F);
            } else if (direction == Direction.West) {
                rotation = MathHelper.ToRadians(-180F);
            }
        }

        /// <summary>
        /// Updates the projectile's direction, position, bounds, and active status
        /// </summary>
        /// <param name="game">The game instance</param>
        /// <param name="entity">The entity to inherit direction from</param>
        public void update(Game1 game, Entity entity) {
            if (direction == Direction.None) {
                direction = entity.getDirection();
                rotate();
            }
            if (direction == Direction.North) {
                deriveY(-velocity);
            } else if (direction == Direction.South) {
                deriveY(velocity);
            } else if (direction == Direction.West) {
                deriveX(-velocity);
            } else if (direction == Direction.East) {
                deriveX(velocity);
            }
            rotation += rotationSpeed;
            active = isOnScreen(game);
        }

        /// <summary>
        /// Draws the projectile
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(texture, Vector2.Add(location, origin), null, Color.White, rotation, origin, 1F, SpriteEffects.None, 0F);
        }
    }
}
