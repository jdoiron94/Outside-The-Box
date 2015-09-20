using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Projectile {

        private Texture2D texture;
        private Vector2 position;

        private int velocity;
        private int cooldown;
        
        public Projectile(Texture2D texture, Vector2 position, int velocity, int cooldown) {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.cooldown = cooldown;
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

        public void deriveX(int x) {
            position.X += x;
        }

        public void deriveY(int y) {
            position.Y += y;
        }
    }
}