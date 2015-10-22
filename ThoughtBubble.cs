using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class ThoughtBubble : GameObject {

        private Npc npc;

        private bool revealed;
        private bool key;

        public ThoughtBubble(Texture2D texture, Vector2 Location, Npc npc, bool revealed, bool key) : base(texture, Location) {
            this.npc = npc;
            setThoughtLocation(npc.getLocation());
            revealed = false;
            key = false;
        }

        public void setThoughtLocation(Vector2 location) {
            float x = location.X + 25;
            float y = location.Y - 20;
            setLocation(new Vector2(x, y));
        }

        public bool isRevealed() {
            return revealed;
        }

        public void updateLocation() {
            setThoughtLocation(npc.getLocation());
        }


        public bool isKey() {
            return key;
        }

        public void reveal(bool r) {
            revealed = r;
        }

        public void setKey() {
            key = true;
        }

        public void draw(SpriteBatch batch) {
            if (revealed) {
                batch.Draw(getTexture(), getLocation(), Color.White);
            }
        }
    }
}
