using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class KeyBox : GameObject {

        private Texture2D normBox;
        private Texture2D nullBox;
        private Texture2D key;
        private bool unlocked;
        private bool nullCheck;

        public KeyBox(Texture2D[] Textures, Vector2 Location) :
            base(Textures[0], Location) {
            normBox = Textures[0];
            nullBox = Textures[1];
            key = Textures[2];
            unlocked = false;
        }

        public bool isUnlocked() {
            return unlocked;
        }

        public bool isNull() {
            return nullCheck;
        }

        public void setUnlocked(bool value) {
            unlocked = value;
        }

        public void setNull(bool value) {
            nullCheck = value;
        }

        public void update(InputManager inputManager) {
            nullCheck = true;
            foreach (Door d in inputManager.getLevel().getDoors()) {
                if (!d.isUnlocked()) {
                    nullCheck = false;
                    unlocked = false;
                }
            }
        }

        public void draw(SpriteBatch batch) {
            if (!nullCheck) {
                batch.Draw(normBox, getLocation(), Color.White);
                if (unlocked == true) {
                    batch.Draw(key, getLocation(), Color.White);
                }
            } else {
                batch.Draw(nullBox, getLocation(), Color.White);
            }
        }
    }
}
