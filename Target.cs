using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Target {

        private Texture2D texture;
        private InputManager inputManager;
        private bool active;
        private Player player;

        public Target(Texture2D texture) {
            this.texture = texture;
            this.active = false;
            //this.inputManager = inputManager;
        }

        public void setInputManager(InputManager inputManager) {
            this.inputManager = inputManager;
        }

        public bool isActive() {
            //return true;
            return active;
        }

        public void setActive(bool active) {
            this.active = active;
        }

        public void draw(SpriteBatch batch) {
            batch.Draw(texture, new Vector2(50, 50), Color.White);
        }
    }
}
