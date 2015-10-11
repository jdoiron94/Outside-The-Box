using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Npc : Entity {

        private readonly NpcDefinition def;

        private AIPath path;

        public Npc(Texture2D texture, Vector2 location, Direction direction, NpcDefinition def, int maxHealth, int velocity) :
            base(texture, location, direction, maxHealth, velocity) {
            this.def = def;
        }

        public NpcDefinition getDefinition() {
            return def;
        }

        /*
         * Returns the entity's static AI path
         */
        public AIPath getPath() {
            return path;
        }

        public void setPath(AIPath path) {
            this.path = path;
        }

        public virtual void update(GameTime time) {
        }
    }
}
